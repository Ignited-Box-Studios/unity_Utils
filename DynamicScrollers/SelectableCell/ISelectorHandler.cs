namespace UnityUtils.DynamicScrollers
{
	public interface ISelectorHandler
	{
		bool IsEnabled(int cellIndex, int dataIndex)
		{
			return true;
		}
		bool OnSelected(int cellIndex, int dataIndex);
	}
}