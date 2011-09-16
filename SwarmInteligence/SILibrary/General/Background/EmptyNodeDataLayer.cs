using SwarmIntelligence.Core.Space;

namespace SILibrary.General.Background
{
	public class EmptyNodeDataLayer<C>: DelegateNodeDataLayer<C, EmptyData>
		where C: ICoordinate<C>
	{
		public EmptyNodeDataLayer(Topology<C> topology)
			: base(topology, delegate { return EmptyData.Instance; }) {}
	}
}