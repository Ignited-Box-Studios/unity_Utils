using UnityEngine;
using Utilities.Numbers;

namespace UnityUtils.Layouts.RectLayout
{
	public class RatioLayoutElement : RectLayoutElement
	{
		[SerializeField] private float ratio;
		[SerializeField] private bool useCurrentSize;

		private Rect Rect => (transform as RectTransform).rect;

		protected override void OnSetEnabled(bool enabled)
		{
			gameObject.SetActive(enabled);
		}

		protected override Rect GetHorizontalRectLayout(Rect offset, RectTransform parent)
		{
			Rect parentRect = parent.rect;
			return new Rect(offset.xMax, 0,
					ratio.GetWidthFromHeight(parentRect.height), 
					useCurrentSize ? Rect.height : parentRect.height
				);
		}
		protected override Rect GetVerticalRectLayout(Rect offset, RectTransform parent)
		{
			Rect parentRect = parent.rect;
			
			return new Rect(0, offset.yMax,
					useCurrentSize ? Rect.width : parentRect.width,
					ratio.GetHeightFromWidth(parentRect.width)
				);
		}
	}
}
