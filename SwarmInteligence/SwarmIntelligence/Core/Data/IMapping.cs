namespace SwarmIntelligence.Core.Data
{
    public interface IMapping<in TKey, out TData>
    {
        TData this[TKey key] { get; }
    }
}