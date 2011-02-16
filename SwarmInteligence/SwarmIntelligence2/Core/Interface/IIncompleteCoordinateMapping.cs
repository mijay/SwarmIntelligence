namespace SwarmIntelligence2.Core.Interface
{
    public interface IIncompleteCoordinateMapping<C, out TData>: ICoordinateMapping<C, TData>
        where C: struct, ICoordinate<C>
    {
        bool HasData(C coord);
    }
}