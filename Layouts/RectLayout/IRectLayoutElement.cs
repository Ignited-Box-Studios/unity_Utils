using UnityEngine;

namespace UnityUtils.Layouts.RectLayout
{
	public interface IRectLayoutElement
	{
		Rect GetRectLayout(Rect offset, RectTransform parent, bool animate);
	}
}
