namespace SwarmIntelligence2.Core.World.Data
{
    public interface IMapping<in TKey, out TData>
    {
        TData this[TKey key] { get; }
    }
}