using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Collections;

namespace UnityUtils.DynamicScrollers
{
	public partial class DynamicScroller
	{
		[Serializable]
		public class Cells
		{
			public IScrollerCell this[int i]
			{
				get => i < 0 || i >= activeCells.Count ? null : activeCells[i];
				set
				{
					if (i >= Count)
					{
						activeCells.Add(value);
						return;
					}

					IScrollerCell oldCell = this[i];
					if (oldCell == value)
						return;

					activeCells[i] = value;
					CacheCell(oldCell);
				}
			}

			public event CellDelegate OnCellCreated;

			[SerializeField]
			private GameObject[] cellPrefabs;

			[SerializeField]
			private RectTransform cellParent;

			[SerializeField]
			private RectTransform cellCache;

			internal int PrefabCount => cellPrefabs.Length;
			private readonly Dictionary<Type, GameObject> mappedPrefabs = new();

			private readonly List<IScrollerCell> activeCells = new();
			private readonly Dictionary<Type, List<IScrollerCell>> cachedCells = new();

			public int Count => activeCells.Count;

			public bool TryGetPrefab(IScrollerCellData data, out GameObject prefab)
			{
				Type type = data.CellType;
				if (!mappedPrefabs.TryGetValue(type, out prefab))
				{
					prefab = cellPrefabs.FirstOrDefault(p => p.GetComponent<IScrollerCell>().CellType == type);
					if (!prefab) return false;
					mappedPrefabs[type] = prefab;
				}

				return true;
			}

			public bool TryRecycleOrCreate(IScrollerCellData data, out IScrollerCell cell)
			{
				return TryRecycle(data, out cell) || TryCreate(data, out cell);
			}

			public bool CacheCellAt(int index, out IScrollerCell cell)
			{
				if (index < 0 || index >= activeCells.Count)
				{
					cell = null;
					return false;
				}

				cell = activeCells.Pop(index);

				CacheCell(cell);
				return true;
			}
			private void CacheCell(IScrollerCell cell)
			{
				Type type = cell.CellType;
				if (!cachedCells.TryGetValue(type, out List<IScrollerCell> cache))
					cache = cachedCells[type] = new List<IScrollerCell>();

				cache.Add(cell);
				if (cellCache) cell.Transform.SetParent(cellCache);
				cell.Transform.gameObject.SetActive(false);
			}

			private bool TryRecycle(IScrollerCellData data, out IScrollerCell cell)
			{
				if (!cachedCells.TryGetValue(data.CellType, out List<IScrollerCell> cache) || cache.Count == 0)
				{
					cell = null;
					return false;
				}

				cell = cache.Pop();
				if (cellCache) cell.Transform.SetParent(cellParent);
				cell.Transform.gameObject.SetActive(true);
				return true;
			}

			private bool TryCreate(IScrollerCellData data, out IScrollerCell cell)
			{
				if (!TryGetPrefab(data, out GameObject prefab))
				{
					cell = null;
					return false;
				}

				GameObject inst = Instantiate(prefab, cellParent);
				if (!inst.TryGetComponent(out cell))
				{
					Destroy(inst);
					return false;
				}

				OnCellCreated?.Invoke(cell);
				return true;
			}
		}

		public IScrollerCell GetCellAt(int index) => cells[index];
		public IScrollerCell FindCellForDataAt(int index)
		{
			for (int i = 0; i < cells.Count; i++)
			{
				IScrollerCell cell = cells[i];
				if (cell.DataIndex == index)
					return cell;
			}

			return null;
		}
	}
}
