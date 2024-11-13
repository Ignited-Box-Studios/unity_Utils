using UnityEngine;

namespace UnityUtils.Layouts.RectLayout
{
	public class RelativeLayoutElement : RectLayoutElement
	{
		[SerializeField] private RectTransform relative;
		[SerializeField] private float heightRatio;
		[SerializeField] private float widthRatio;

		protected override void OnSetEnabled(bool enabled)
		{
			gameObject.SetActive(enabled);
		}


		protected override Rect GetHorizontalRectLayout(Rect offset, RectTransform parent)
		{
			Rect relativeRect = relative.rect;
			return new Rect(offset.xMax, 0,
					relativeRect.width * widthRatio,
					relativeRect.height * heightRatio
				);
		}
		protected override Rect GetVerticalRectLayout(Rect offset, RectTransform parent)
		{
			Rect relativeRect = relative.rect;
			return new Rect(0, offset.yMax,
					relativeRect.width * widthRatio,
					relativeRect.height * heightRatio
				);
		}
	}
}
