using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.Common.Layout;
using UnityUtils.RectUtils;
using Utils.Logger;

namespace UnityUtils.Layouts.RectLayout
{
	[Serializable]
	public class RectLayoutComponent : ILayoutComponent, IAnimatedRectLayoutElement
	{
		public bool IsEnabled => Transform.gameObject.activeSelf;

		[field: SerializeField] public RectTransform Transform { get; private set; }
		[field: SerializeField] public RectTransform.Axis Axis { get; private set; }
		[SerializeField] public Vector2 spacing;

		[Header("Wrap Content")]
		[SerializeField]private bool doWrapWidth;
		[SerializeField]private bool doWrapHeight;
		[field: SerializeField] public float AnimationDuration { get; private set; }
		public float AnimationTime { get; private set; }
		public Rect TargetRect { get; private set; }

		private bool DoWrap => doWrapWidth || doWrapHeight;

		public void Reload(Transform sizeSource, bool animate)
		{
			if (sizeSource is not RectTransform parent)
				return;

			ReloadSize(default, parent.rect, animate);
		}
		private Rect ReloadSize(Rect rect, Rect source, bool animate)
		{
			int count = Transform.childCount;
			for (int i = 0; i < count; i++)
			{
				Transform child = Transform.GetChild(i);
				if (!child.gameObject.activeSelf)
					continue;

				if (child.TryGetComponent(out IRectLayoutElement element))
				{
					if (!element.IsEnabled)
						continue;

					rect = element.GetRectLayout(rect, source, animate);
				}
				else if (child is RectTransform rectChild)
				{
					rect = rect.Wrap(rectChild);
				}

				rect = rect.AddSize(spacing);
			}

			if (DoWrap)
				Wrap(rect.Wrap(default(Rect)), animate);

			return rect;
		}
		public Rect GetRectLayout(Rect offset, Rect source, bool animate)
		{
			return ReloadSize(offset, source, animate);
		}
		private void Wrap(Rect wrap, bool animate)
		{
			TargetRect = wrap;

			if (animate)
			{
				this.AnimateResizeAsync().LogException();
			}
			else
			{
				SetRect(TargetRect);
			}
		}
		public bool Next()
		{
			AnimationTime += Time.deltaTime; 
			bool hasNext = AnimationTime < AnimationDuration;
			if (!hasNext)
				AnimationTime = 0;

			return hasNext;
		}

		public void SetRect(Rect rect)
		{
			//Transform.localPosition = rect.position;
			Vector2 size = Transform.GetDeltaWithAnchors(rect, doWrapWidth, doWrapHeight);
			Transform.sizeDelta = size;
		}
	}
}
