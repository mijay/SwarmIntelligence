using System;
using System.Diagnostics;

namespace Common
{
	public static class Requires
	{
		[Conditional("DEBUG")]
		public static void NotNull(params object[] objects)
		{
			for(int i = 0; i < objects.Length; i++)
				if(objects[i] == null)
					throw new ArgumentNullException(string.Format("Argument at position {0} is null", i));
		}

		[Conditional("DEBUG")]
		public static void NotNull<TException>(object @object)
			where TException: Exception, new()
		{
			if(@object == null)
				throw new TException();
		}

		[Conditional("DEBUG")]
		public static void True(bool condition)
		{
			if(!condition)
				throw new ArgumentException("Argument does not satisfy condition");
		}

		[Conditional("DEBUG")]
		public static void True<TException>(bool condition)
			where TException: Exception, new()
		{
			if(!condition)
				throw new TException();
		}
	}
}