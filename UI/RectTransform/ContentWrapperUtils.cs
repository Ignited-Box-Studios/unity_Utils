using UnityEngine;
using UnityUtils.RectUtils;

namespace UnityUtils.UI.RectTransforms
{
	public static class ContentWrapperUtils
	{
		public static void ResizeAroundChildren(RectTransform transform, bool wrapWidth, bool wrapHeight)
		{
			Rect reach = default;
			int count = transform.childCount;

			for (int i = 0; i < count; i++)
			{
				Transform child = transform.GetChild(i);
				if (child.gameObject.activeInHierarchy && child is RectTransform childTransform)
					reach = reach.Wrap(childTransform.GetPositionedRect());
			}

			transform.SetRect(reach, wrapWidth, wrapHeight);
		}
	}
}
