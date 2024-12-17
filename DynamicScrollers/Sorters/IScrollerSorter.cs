using System.Collections.Generic;

namespace UnityUtils.DynamicScrollers
{
	public interface IScrollerSorter
	{
		IEnumerable<int> Sort(IList<IScrollerCellData> data);
	}
}
