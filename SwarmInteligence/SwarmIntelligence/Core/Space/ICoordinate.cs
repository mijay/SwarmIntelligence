using System;
using Common;
using SwarmIntelligence.Core.Playground;

namespace SwarmIntelligence.Core.Space
{
	/// <summary>
	/// Interface for representing coordinates on the <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/>.
	/// </summary>
	/// <remarks>
	///	Should be implemented in the following way:
	/// <code>
	/// public struct Coordinate: ICoordinate&lt;Coordinate&gt;
	/// {
	///	 …
	/// }
	/// And should override <see cref="object.GetHashCode"/> correctly.
	/// </code>
	/// </remarks>
	public interface ICoordinate<TCoordinate>: IEquatable<TCoordinate>, ICloneable<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		int GetHashCode();
	}
}