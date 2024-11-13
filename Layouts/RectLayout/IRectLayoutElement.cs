using UnityEngine;

namespace UnityUtils.Layouts.RectLayout
{
	public interface IRectLayoutElement
	{
		bool IsEnabled { get; }
		Rect GetRectLayout(Rect offset, RectTransform parent, bool animate);
	}
}
