using System.Collections.Generic;
using System.Linq;
using Common.Collections.Concurrent;
using Common.Collections.Extensions;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Graph
{
	public class GraphMapping<TValue>: MappingBase<GraphCoordinate, TValue>
		where TValue: class
	{
		private readonly ConcurrentList<TValue> values = new ConcurrentList<TValue>();

		public GraphMapping(IValueProvider<GraphCoordinate, TValue> valueProvider, ILog log)
			: base(valueProvider, log)
		{
		}

		#region Overrides of MappingBase<GraphCoordinate,TValue>

		public override bool TryGet(GraphCoordinate key, out TValue value)
		{
			value = values[key.vertex];
			return value != null;
		}

		public override IEnumerator<KeyValuePair<GraphCoordinate, TValue>> GetEnumerator()
		{
			return values
				.Select((value, index) => value == null
				                          	? (KeyValuePair<GraphCoordinate, TValue>?) null
				                          	: new KeyValuePair<GraphCoordinate, TValue>(new GraphCoordinate(index), value))
				.NotNull()
				.GetEnumerator();
		}

		protected override bool TryRemove(GraphCoordinate key, out TValue value)
		{
			TValue tmpValue = values[key.vertex];
			if(tmpValue == null) {
				value = null;
				return false;
			}

			do {
				value = tmpValue;
				tmpValue = values.CompareExchange(key.vertex, null, value);
			} while(tmpValue != value);
			return value != null;
		}

		protected override TValue GetOrAdd(GraphCoordinate key, TValue value)
		{
			return values.CompareExchange(key.vertex, value, null);
		}

		#endregion
	}
}