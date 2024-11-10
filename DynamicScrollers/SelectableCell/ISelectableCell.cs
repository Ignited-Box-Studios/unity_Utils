namespace UnityUtils.DynamicScrollers
{
	public interface ISelectableCell : IScrollerCell
	{
		bool IsSelected { get; set; }
		void SetHandler(ISelectorHandler handler);
	}
}