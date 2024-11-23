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


		protected override Rect GetHorizontalRectLayout(Rect offset, Rect source)
		{
			return new Rect(offset.xMax, 0,
					source.width * widthRatio,
					source.height * heightRatio
				);
		}
		protected override Rect GetVerticalRectLayout(Rect offset, Rect source)
		{
			return new Rect(0, offset.yMax,
					source.width * widthRatio,
					source.height * heightRatio
				);
		}
	}
}
