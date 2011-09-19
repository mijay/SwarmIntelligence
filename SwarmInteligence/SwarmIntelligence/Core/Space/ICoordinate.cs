using System;
using Common;
using SwarmIntelligence.Core.Playground;

namespace SwarmIntelligence.Core.Space
{
	//todo: сейчас интерфейс нигде реально не используеться... Наверно стоит вернуть where
	//todo: то что я снес контракты видится неверным, особенно теперь, когда я все перевел на интерфейсы
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