namespace SwarmIntelligence.Core.Interfaces
{
    public interface IMapping<in TKey, out TData>
    {
        TData this[TKey key] { get; }
    }
}