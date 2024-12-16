namespace UnityUtils.DynamicScrollers
{
	public interface IScrollerFilter
	{
		bool Include(int index, IScrollerCellData data);
	}

	public interface IScrollerFilter<TData> : IScrollerFilter
		where TData : IScrollerCellData
	{
		bool IncludeTypeMismatch => false;

		bool IScrollerFilter.Include(int index, IScrollerCellData data)
		{
			return data is not TData tdata ? IncludeTypeMismatch : Include(index, tdata);
		}
		bool Include(int index, TData data);
	}
}
