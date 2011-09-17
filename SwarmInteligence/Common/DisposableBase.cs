using System;

namespace Common
{
	public abstract class DisposableBase: IDisposable
	{
		protected bool Disposed { get; private set; }

		protected virtual void DisposeManaged()
		{
		}

		protected virtual void DisposeUnmanaged()
		{
		}

		~DisposableBase()
		{
			if(Disposed)
				return;
			DisposeUnmanaged();
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			if(Disposed)
				return;
			Disposed = true;
			DisposeManaged();
			DisposeUnmanaged();
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}