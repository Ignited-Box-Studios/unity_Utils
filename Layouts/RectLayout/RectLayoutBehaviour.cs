using UnityEngine;
using UnityUtils.Common.Layout;

namespace UnityUtils.Layouts.RectLayout
{
	public class RectLayoutBehaviour : LayoutController, IRectLayoutElement
	{
		[SerializeField] private RectLayoutComponent settings;

		public override void ReloadLayout(bool animate)
		{
			base.ReloadLayout(animate);
			settings?.Reload(transform, animate);
		}

		public Rect GetRectLayout(Rect offset, RectTransform parent, bool animate)
		{
			return settings.GetRectLayout(offset, parent, animate);
		}
	}
}
