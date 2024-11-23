using UnityEngine;
using UnityUtils.Common.Layout;

namespace UnityUtils.Layouts.RectLayout
{
	public class RectLayoutBehaviour : LayoutController, IRectLayoutElement
	{
		public bool IsEnabled => enabled;

		[SerializeField] private Transform sizeSource;
		[SerializeField] private RectLayoutComponent settings;

		public override void ReloadLayout(bool animate)
		{
			base.ReloadLayout(animate);
			settings?.Reload(sizeSource ? sizeSource : transform, animate);
		}

		public Rect GetRectLayout(Rect offset, Rect source, bool animate)
		{
			if (!isActiveAndEnabled)
				return offset;

			return settings.GetRectLayout(offset, source, animate);
		}
	}
}
