//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: TaskExtensions.cs
//
//--------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Tasks
{
	/// <summary>Extensions methods for Task.</summary>
	public static class TaskExtensions
	{
		#region Timeouts

		/// <summary>Creates a new Task that mirrors the supplied task but that will be canceled after the specified timeout.</summary>
		/// <param name="task">The task.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>The new Task that may time out.</returns>
		public static Task WithTimeout(this Task task, TimeSpan timeout)
		{
			var result = new TaskCompletionSource<object>(task.AsyncState);
			var timer = new Timer(state => ((TaskCompletionSource<object>) state).TrySetCanceled(), result, timeout, TimeSpan.FromMilliseconds(-1));
			task.ContinueWith(t => {
			                  	timer.Dispose();
			                  	result.TrySetFromTask(t);
			                  }, TaskContinuationOptions.ExecuteSynchronously);
			return result.Task;
		}

		/// <summary>Creates a new Task that mirrors the supplied task but that will be canceled after the specified timeout.</summary>
		/// <typeparam name="TResult">Specifies the type of data contained in the task.</typeparam>
		/// <param name="task">The task.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>The new Task that may time out.</returns>
		public static Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
		{
			var result = new TaskCompletionSource<TResult>(task.AsyncState);
			var timer = new Timer(state => ((TaskCompletionSource<TResult>) state).TrySetCanceled(), result, timeout, TimeSpan.FromMilliseconds(-1));
			task.ContinueWith(t => {
			                  	timer.Dispose();
			                  	result.TrySetFromTask(t);
			                  }, TaskContinuationOptions.ExecuteSynchronously);
			return result.Task;
		}

		#endregion

		#region Children

		/// <summary>
		/// Ensures that a parent task can't transition into a completed state
		/// until the specified task has also completed, even if it's not
		/// already a child task.
		/// </summary>
		/// <param name="task">The task to attach to the current task as a child.</param>
		public static void AttachToParent(this Task task)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			task.ContinueWith(t => t.Wait(), CancellationToken.None,
			                  TaskContinuationOptions.AttachedToParent |
			                  TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		#endregion

		#region Then

		/// <summary>Creates a task that represents the completion of a follow-up action when a task completes.</summary>
		/// <param name="task">The task.</param>
		/// <param name="next">The action to run when the task completes.</param>
		/// <returns>The task that represents the completion of both the task and the action.</returns>
		public static Task Then(this Task task, Action next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate {
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		try {
			                  			next();
			                  			tcs.TrySetResult(null);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a follow-up function when a task completes.</summary>
		/// <param name="task">The task.</param>
		/// <param name="next">The function to run when the task completes.</param>
		/// <returns>The task that represents the completion of both the task and the function.</returns>
		public static Task<TResult> Then<TResult>(this Task task, Func<TResult> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TResult>();
			task.ContinueWith(delegate {
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		try {
			                  			TResult result = next();
			                  			tcs.TrySetResult(result);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a follow-up action when a task completes.</summary>
		/// <param name="task">The task.</param>
		/// <param name="next">The action to run when the task completes.</param>
		/// <returns>The task that represents the completion of both the task and the action.</returns>
		public static Task Then<TResult>(this Task<TResult> task, Action<TResult> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate {
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		try {
			                  			next(task.Result);
			                  			tcs.TrySetResult(null);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a follow-up function when a task completes.</summary>
		/// <param name="task">The task.</param>
		/// <param name="next">The function to run when the task completes.</param>
		/// <returns>The task that represents the completion of both the task and the function.</returns>
		public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, TNewResult> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNewResult>();
			task.ContinueWith(delegate {
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		try {
			                  			TNewResult result = next(task.Result);
			                  			tcs.TrySetResult(result);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
		/// <param name="task">The first task.</param>
		/// <param name="next">The function that produces the second task.</param>
		/// <returns>The task that represents the completion of both the first and second task.</returns>
		public static Task Then(this Task task, Func<Task> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate {
			                  	// When the first task completes, if it faulted or was canceled, bail
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		// Otherwise, get the next task.  If it's null, bail.  If not,
			                  		// when it's done we'll have our result.
			                  		try {
			                  			next().ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
		/// <param name="task">The first task.</param>
		/// <param name="next">The function that produces the second task based on the result of the first task.</param>
		/// <returns>The task that represents the completion of both the first and second task.</returns>
		public static Task Then<T>(this Task<T> task, Func<T, Task> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate {
			                  	// When the first task completes, if it faulted or was canceled, bail
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		// Otherwise, get the next task.  If it's null, bail.  If not,
			                  		// when it's done we'll have our result.
			                  		try {
			                  			next(task.Result).ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
		/// <param name="task">The first task.</param>
		/// <param name="next">The function that produces the second task.</param>
		/// <returns>The task that represents the completion of both the first and second task.</returns>
		public static Task<TResult> Then<TResult>(this Task task, Func<Task<TResult>> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TResult>();
			task.ContinueWith(delegate {
			                  	// When the first task completes, if it faulted or was canceled, bail
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		// Otherwise, get the next task.  If it's null, bail.  If not,
			                  		// when it's done we'll have our result.
			                  		try {
			                  			next().ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		/// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
		/// <param name="task">The first task.</param>
		/// <param name="next">The function that produces the second task based on the result of the first.</param>
		/// <returns>The task that represents the completion of both the first and second task.</returns>
		public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, Task<TNewResult>> next)
		{
			if(task == null)
				throw new ArgumentNullException("task");
			if(next == null)
				throw new ArgumentNullException("next");

			var tcs = new TaskCompletionSource<TNewResult>();
			task.ContinueWith(delegate {
			                  	// When the first task completes, if it faulted or was canceled, bail
			                  	if(task.IsFaulted)
			                  		tcs.TrySetException(task.Exception.InnerExceptions);
			                  	else if(task.IsCanceled)
			                  		tcs.TrySetCanceled();
			                  	else
			                  		// Otherwise, get the next task.  If it's null, bail.  If not,
			                  		// when it's done we'll have our result.
			                  		try {
			                  			next(task.Result).ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default);
			                  		}
			                  		catch(Exception exc) {
			                  			tcs.TrySetException(exc);
			                  		}
			                  }, TaskScheduler.Default);
			return tcs.Task;
		}

		#endregion
	}
}