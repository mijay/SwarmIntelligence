using System;
using SwarmIntelligence.Core;

namespace SILibrary.General.Background
{
	public class EmptyDataLayer<TKey>: DataLayer<TKey, EmptyData>
	{
		private readonly EmptyData emptyData;

		public EmptyDataLayer()
		{
			emptyData = new EmptyData();
		}

		#region Overrides of DataLayer<TKey,EmptyData>

		public override EmptyData Get(TKey key)
		{
			return emptyData;
		}

		public override void Set(TKey key, EmptyData data)
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}