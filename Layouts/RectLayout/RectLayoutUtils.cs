using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.RectUtils;

namespace UnityUtils.Layouts.RectLayout
{
	public static class RectLayoutUtils
	{
		public static async Task AnimateResizeAsync(this IAnimatedRectLayoutElement element)
		{
			if (System.Math.Abs(element.AnimationDuration) > 0)
			{
				element.SetRect(element.TargetRect);
				return;
			}
			
			if (element.AnimationTime > 0)
				return;

			RectTransform transform = element.Transform;
			RectTransform.Axis axis = element.Axis;

			Vector2 startPos = transform.position;
			Vector2 startSize = transform.sizeDelta;

			while (element.Next())
			{
				float norm = element.AnimationTime / element.AnimationDuration;
				if (!transform)
					break;

				Vector2 pos = Vector2.Lerp(startPos, element.TargetRect.position, norm); 
				element.SetRect(new Rect(pos, Vector2.Lerp(startSize, element.TargetRect.size, norm)));
				await Task.Yield();
			}

			element.SetRect(element.TargetRect);
		}
	}
}
