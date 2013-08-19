using System;

namespace Common
{
	public abstract class DisposableBase: IDisposable
	{
		private bool disposed;

		protected void CheckDisposedState()
		{
			if (disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}

		protected virtual void DisposeManaged()
		{
		}

		protected virtual void DisposeUnmanaged()
		{
		}

		~DisposableBase()
		{
			if(disposed)
				return;
			DisposeUnmanaged();
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			if(disposed)
				return;
			disposed = true;
			DisposeManaged();
			DisposeUnmanaged();
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}