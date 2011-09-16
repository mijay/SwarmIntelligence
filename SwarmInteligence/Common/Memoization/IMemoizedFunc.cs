namespace Common.Memoization
{
	public interface IMemoizedFunc<in TArg, out TVal>
	{
		TVal Get(TArg arg);
		void Refresh(TArg arg);
	}

	public interface IMemoizedFunc<out TVal>
	{
		TVal Get();
		void Refresh();
	}
}