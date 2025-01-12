﻿using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.DynamicScrollers
{
	public struct CellSizeLayoutContainer : ILayoutGroupContainer
	{
		public readonly Vector2 Spacing => Vector2.zero;
		public readonly RectOffset Padding => new RectOffset();
		public readonly Vector2Int GridSize => new Vector2Int(1 ,1);

		[SerializeField] private DynamicScroller scroller;

		public readonly Vector2 GetContentSize(RectTransform.Axis axis, RectTransform transform)
		{
			Vector2 axisSizing = axis switch
			{ 
				RectTransform.Axis.Horizontal => new Vector2(1, 0),
				RectTransform.Axis.Vertical => new Vector2(0, 1),
				_ => Vector2.zero
			};

			Rect rect = transform.rect;
			Vector2 size = axis switch
			{
				RectTransform.Axis.Horizontal => new Vector2(0, rect.size.y),
				RectTransform.Axis.Vertical => new Vector2(rect.size.x, 0),
				_ => Vector2.zero
			};

			IList<IScrollerCellData> data = scroller.Data;
			for (int i = 0; i < data.Count; i++)
			{
				IScrollerCell cell = scroller.GetCellAt(i);
				if (cell == null)
					continue;
				Vector2 cellSize = cell.GetSize(rect, axis);
				size += cellSize * axisSizing;
				cell.Transform.sizeDelta = size;
			}

			return size;
		}
	}
}
