using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.PropertyAttributes;
using Axis = UnityEngine.RectTransform.Axis;

namespace UnityUtils.DynamicScrollers
{
	public partial class DynamicScroller : ScrollRect
	{
		public static readonly string[] serializedFields =
		{
			nameof(cells),
			nameof(contentComponents),
			nameof(_data),
		};

		[SerializeField] private Cells cells;
		[SerializeField] private ContentComponents contentComponents;

		private IList<IScrollerCellData> _data;
		public IList<IScrollerCellData> Data
		{
			get => _data;
			set
			{
				if (_data == value) return;

				_data = value;
				ReloadCells();
			}
		}

		public Axis ScrollAxis => vertical ? Axis.Vertical : Axis.Horizontal;

		protected override void OnEnable()
		{
			base.OnEnable();
			ReloadCells();
		}

		public void ReloadCells()
		{
			ResetContentSize();

			int cellIndex = 0;
			int count = _data?.Count ?? 0;
			for (int dataIndex = 0; dataIndex < count; dataIndex++)
			{
				if (ReloadAt(cellIndex, dataIndex))
					cellIndex++;
			}

			for (int i = cells.Count - 1; i >= cellIndex; i--)
			{
				if (!cells.CacheCellAt(i, out IScrollerCell cell)) 
					continue;

				ClearCell(cell);
			}

			if (count > 0 && contentComponents.Sizing == ContentComponents.SizingType.OnReload && contentComponents.Layout != null)
			{
				Vector2 size = contentComponents.Layout.GetContentSize(ScrollAxis, viewport);
				SetContentSize(size);
			}
		}

		public bool ReloadDataAt(int dataIndex, IScrollerCellData data)
		{
			IScrollerCell cell = FindCellForDataAt(dataIndex);
			if (cell == null)
				return false;

			Data[dataIndex] = data;
			cell.SetData(data);
			return true;
		}
		private bool ReloadAt(int cellIndex, int dataIndex)
		{
			IScrollerCellData data = _data[dataIndex];
			IScrollerCell cell = cells[cellIndex];

			if (cell?.CellType == data.CellType)
			{
				ClearCell(cell);
				cell.SetData(data);
				InitializeCell(cell, cellIndex, dataIndex);
				return true;
			}

			if (cells.TryRecycleOrCreate(data, out cell))
			{
				InitializeCell(cell, cellIndex, dataIndex);
				return true;
			}

			return false;
		}

		private void ResetContentSize()
		{
			content.sizeDelta = Vector2.zero;
			Axis scrollAxis = ScrollAxis;
			content.sizeDelta = contentComponents.StartPadding(scrollAxis) + contentComponents.EndPadding(scrollAxis);
		}

		private void SetContentSize(Vector2 size)
		{
			content.sizeDelta = size;
		}
		private void AddViewportSize(Vector2 cellSize, int cellIndex)
		{
			Vector2 padding = cellIndex == 0 ? Vector2.zero : contentComponents.Spacing;
			switch (ScrollAxis)
			{
				case Axis.Horizontal:
					content.sizeDelta += new Vector2(cellSize.x + padding.x, 0);
					break;
				case Axis.Vertical:
					content.sizeDelta += new Vector2(0, cellSize.y + padding.x);
					break;
			}
		}
	}
}
