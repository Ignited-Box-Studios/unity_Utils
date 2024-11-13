using UnityEngine;
using UnityUtils.Common.Layout;

namespace UnityUtils.Layouts.RectLayout
{
	public class RectLayoutBehaviour : LayoutController, IRectLayoutElement
	{
		public bool IsEnabled => enabled;

		[SerializeField] private RectLayoutComponent settings;

		public override void ReloadLayout(bool animate)
		{
			base.ReloadLayout(animate);
			settings?.Reload(transform, animate);
		}

		public Rect GetRectLayout(Rect offset, RectTransform parent, bool animate)
		{
			if (!isActiveAndEnabled)
				return offset;

			return settings.GetRectLayout(offset, parent, animate);
		}
	}
}
