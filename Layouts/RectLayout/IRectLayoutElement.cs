using UnityEngine;

namespace UnityUtils.Layouts.RectLayout
{
	public interface IRectLayoutElement
	{
		bool IsEnabled { get; }
		Rect GetRectLayout(Rect offset, Rect source, bool animate);
	}
}
