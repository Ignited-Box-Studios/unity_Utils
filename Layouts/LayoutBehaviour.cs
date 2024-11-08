using UnityEngine;
using UnityUtils.PropertyAttributes;

namespace UnityUtils.Common.Layout
{
	[ExecuteInEditMode]
	public class LayoutBehaviour : LayoutController
	{
		[SerializeReference, Polymorphic]
		private ILayoutComponent controller;

		public override void ReloadLayout(bool animate = false)
		{
			base.ReloadLayout(animate);
			controller?.Reload(transform, animate);
		}
	}
}
