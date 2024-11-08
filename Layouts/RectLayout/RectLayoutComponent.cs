using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.Common.Layout;
using UnityUtils.RectUtils;
using Utils.Logger;

namespace UnityUtils.Layouts.RectLayout
{
	[Serializable]
	public class RectLayoutComponent : ILayoutComponent, IRectLayoutElement
	{
		[SerializeField]
		private RectTransform transform;

		[Header("Wrap Content")]
		[SerializeField]private bool doWrapWidth;
		[SerializeField]private bool doWrapHeight;
		[SerializeField]private float resizeDuration;
		private float resizeTime;
		private Vector2 resizeTarget;

		private bool DoWrap => doWrapWidth || doWrapHeight;

		public void Reload(Transform transformComponent, bool animate)
		{
			if (transformComponent is not RectTransform transform)
				return;

			ReloadSize(default, transform, animate);
		}

		private Rect ReloadSize(Rect rect, RectTransform parent, bool animate)
		{
			int count = transform.childCount;
			for (int i = 0; i < count; i++)
			{
				Transform child = transform.GetChild(i);
				if (child.TryGetComponent(out IRectLayoutElement element))
				{
					rect = element.GetRectLayout(rect, parent, animate);
				}
				else if (child is RectTransform rectChild)
				{
					rect = rect.Wrap(rectChild);
				}
			}

			if (DoWrap)
				Wrap(rect.Wrap(default(Rect)));

			return rect;
		}

		public Rect GetRectLayout(Rect offset, RectTransform parent, bool animate)
		{
			return ReloadSize(offset, parent, animate);
		}

		private void Wrap(Rect wrap)
		{
			resizeTarget = transform.GetDeltaWithAnchors(wrap, doWrapWidth, doWrapHeight);
			ResizeAsync().LogException();
		}

		private async Task ResizeAsync()
		{
			//Already running
			if (resizeTime > 0)
				return;

			Vector2 start = transform.sizeDelta;

			while (resizeTime < resizeDuration)
			{
				float norm = resizeTime / resizeDuration;
				if (!transform)
					break;

				transform.sizeDelta = Vector2.Lerp(start, resizeTarget, norm);

				await Task.Yield();
				resizeTime += Time.deltaTime;
			}

			transform.sizeDelta = resizeTarget;
			resizeTime = 0;
		}
	}
}
