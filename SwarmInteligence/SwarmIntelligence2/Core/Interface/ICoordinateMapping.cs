namespace SwarmIntelligence2.Core.Interface
{
    public interface ICoordinateMapping<C, out TData>
        where C: struct, ICoordinate<C>
    {
        TData this[C coord] { get; }
    }
}