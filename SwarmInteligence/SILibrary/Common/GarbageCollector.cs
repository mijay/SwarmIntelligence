using System;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Common
{
	public class GarbageCollector<TKey, TValue>: IGarbageCollector<TKey, TValue>
	{
		#region Implementation of IGarbageCollector<TKey,TValue>

		public void Collect(MappingBase<TKey, TValue> mappingBase)
		{
			throw new NotImplementedException();
		}

		#endregion

		//public void Collect()
		//{
		//    Map
		//        .AsParallel()
		//        .Select(cell => cell.Base())
		//        .Where(cellBase => cellBase.IsEmpty)
		//        .ForAll(cellBase => Map.Free(cellBase.Coordinate));
		//}
	}
}