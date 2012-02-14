namespace Common
{
	public static class MutableTuple
	{
		public static MutableTuple<T> Create<T>(T val)
		{
			return new MutableTuple<T> { Item1 = val };
		}
	}

	public class MutableTuple<T>
	{
		public T Item1 { get; set; }
	}
}