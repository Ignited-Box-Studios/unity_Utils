using System;

namespace UnityUtils.DynamicScrollers
{
	public class DataPredicateFilter : IScrollerFilter
	{
		private readonly Predicate<IScrollerCellData> predicate;

		public DataPredicateFilter(Predicate<IScrollerCellData> predicate)
		{
			this.predicate = predicate;
		}

		public bool Include(int dataIndex, IScrollerCellData data)
		{
			return predicate(data);
		}
	}
}
