using System;
using Common;
using SwarmIntelligence.Core.Playground;

namespace SwarmIntelligence.Core.Space
{
	/// <summary>
	/// Interface for representing coordinates on the <see cref="Map{TCoordinate,TNodeData,TEdgeData}"/>.
	/// Should be implemented in the following way:
	/// <code>
	/// public struct Coordinate: ICoordinate&lt;Coordinate&gt;
	/// {
	///	 …
	/// }
	/// </code>
	/// </summary>
	public interface ICoordinate<TCoordinate>: IEquatable<TCoordinate>, ICloneable<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate> {}
}