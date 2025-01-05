﻿using TMPro;
using UnityEngine;

namespace UnityUtils.Layouts.RectLayout
{
	public class TextLayoutElement : RectLayoutElement
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private float fit = 0.9f;
		private bool HasContent => enabled && !string.IsNullOrEmpty(label.text);

		protected override void OnSetEnabled(bool enabled)
		{
			label.enabled = enabled;
		}

		protected override Rect GetHorizontalRectLayout(Rect offset, Rect source)
		{
			float height = source.height * fit;
			Vector2 size = HasContent ? label.GetPreferredValues(0, height) : Vector2.zero;
			return new Rect(offset.xMax, 0, size.x, height);
		}

		protected override Rect GetVerticalRectLayout(Rect offset, Rect source)
		{
			float width = source.width * fit;
			Vector2 size = HasContent ? label.GetPreferredValues(width, 0) : Vector2.zero;
			return new Rect(0, offset.yMax, width, size.y);
		}
	}
}
