﻿using UnityEngine;

namespace UnityUtils.RectUtils
{
	public static class RectTransformEx 
	{
		public static Rect Wrap(this Rect rect, Transform transform)
		{
			if (transform is not RectTransform rectTransform)
				return rect;

			return rect.Wrap(rectTransform);
		}
		public static Rect Wrap(this Rect rect, RectTransform transform)
		{
			return rect.Wrap(transform.GetPositionedRect());
		}

		public static Rect GetPositionedRect(this RectTransform child)
		{
			return child.GetPositionedRect(child.rect);
		}
		public static Rect GetPositionedRect(this Transform child, Rect target)
		{
			Vector2 pos = child.localPosition;
			return Rect.MinMaxRect(
				pos.x + target.xMin, pos.y + target.yMin,
				pos.x + target.xMax, pos.y + target.yMax
				);
		}

		public static Vector2 GetSize(this RectTransform transform)
		{
			return transform.rect.size;
		}

		public static float GetSizeWithCurrentAnchors(this RectTransform transform, RectTransform.Axis axis, float size)
		{
			if (transform.parent is not RectTransform parent)
				return 0;

			return size - parent.GetSize()[(int)axis] * (transform.anchorMax[(int)axis] - transform.anchorMin[(int)axis]);
		}

		public static Vector2 GetDeltaWithAnchors(this RectTransform transform, Rect rect, bool getWidth = true, bool getHeight = true)
		{
			Vector2 delta = transform.sizeDelta;
			float width = getWidth ? transform.GetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width) : delta.x;
			float height = getHeight ? transform.GetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height) : delta.y;
			return new Vector2(width, height);
		}
		public static void SetRect(this RectTransform transform, Rect rect, bool setWidth = true, bool setHeight = true)
		{
			if (setWidth)
			{
				transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width);
			}

			if (setHeight)
			{
				transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height);
			}
		}
	}
}
