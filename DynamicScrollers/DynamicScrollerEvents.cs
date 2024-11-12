using UnityEngine;

namespace UnityUtils.DynamicScrollers
{
	public delegate void CellDelegate(IScrollerCell cell);

	public partial class DynamicScroller
	{
		public event CellDelegate OnCellCreated
		{
			add => cells.OnCellCreated += value;
			remove => cells.OnCellCreated -= value;
		}
		public event CellDelegate OnCellInitialized;
		public event CellDelegate OnCellCleared;

		protected virtual void InitializeCell(IScrollerCell cell, int cellIndex, int dataIndex)
		{
			cell.DataIndex = dataIndex;
			cell.CellIndex = cellIndex;
			cell.SetData(_data[dataIndex]);
			cell.Transform.SetSiblingIndex(cellIndex);
			cells[cellIndex] = cell;

			if (contentComponents.Sizing == ContentComponents.SizingType.Additive)
			{
				Vector2 cellSize = cell.GetSize(content.rect, ScrollAxis);
				AddViewportSize(cellSize, cellIndex);
			}

			OnCellInitialized?.Invoke(cell);
		}
		protected virtual void ClearCell(IScrollerCell cell)
		{
			cell.Clear();
			OnCellCleared?.Invoke(cell);
		}
	}
}
