namespace Common
{
    public class MutablePair<TKey, TValue>
    {
        public TKey Key { set; get; }
        public TValue Value { set; get; }

        public MutablePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
