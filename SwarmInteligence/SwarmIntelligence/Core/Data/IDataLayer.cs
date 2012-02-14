using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
	public interface IDataLayer<TCoordinate, TLayerCoordinate, TData>: IMapping<TLayerCoordinate, TData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// <see cref="Topology{TCoordinate}"/> of the current space.
		/// </summary>
		[Pure]
		Topology<TCoordinate> Topology { get; }

		/// <summary>
		/// Returns the <typeparamref name="TLayerCoordinate"/> associated with <paramref name="key"/>.
		/// If this value does not exist - force its creation.
		/// </summary>
		[Pure]
		TData Get(TLayerCoordinate key);
	}
}