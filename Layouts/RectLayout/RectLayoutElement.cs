using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.UI.Selectable;
using Utils.Logger;

namespace UnityUtils.Layouts.RectLayout
{
	public abstract class RectLayoutElement : MonoBehaviour, IRectLayoutElement
	{
		[SerializeField] private RectTransform.Axis axis;
		[SerializeField] private Vector2Int direction;

		[Header("Animation")]
		[SerializeField] private float duration; 
		private float resizeTime;
		private Rect target;

		protected virtual void OnEnable() { }
		protected virtual void OnDisable() { }

		public void Toggle(Button button)
		{
			OnSetEnabled(enabled = !button.IsSelected);
		}

		protected virtual void OnSetEnabled(bool enabled) { }

		public Rect GetRectLayout(Rect offset, RectTransform parent, bool animate)
		{
			target = axis switch
			{
				RectTransform.Axis.Horizontal => GetHorizontalRectLayout(offset, parent),
				RectTransform.Axis.Vertical => GetVerticalRectLayout(offset, parent),
				_ => throw new ArgumentOutOfRangeException(nameof(axis)),
			};

			if (animate)
			{
				AnimateAsync().LogException();
			}
			else
			{
				Set(transform as RectTransform);
			}

			return target;
		}

		protected abstract Rect GetVerticalRectLayout(Rect offset, RectTransform parent);
		protected abstract Rect GetHorizontalRectLayout(Rect offset, RectTransform parent);

		public async Task AnimateAsync()
		{
			//Already running
			if (resizeTime > 0)
				return;

			RectTransform transform = this.transform as RectTransform;

			Vector2 startPos = transform.position;
			Vector2 startSize = axis switch
			{
				RectTransform.Axis.Vertical => new Vector2(target.width, 0),
				RectTransform.Axis.Horizontal => new Vector2(0, target.height),
				_ => Vector2.zero,
			};

			while (resizeTime < duration)
			{
				float norm = resizeTime / duration;
				if (!transform)
					break;

				transform.localPosition = Vector2.Lerp(startPos, target.position * direction, norm);
				transform.sizeDelta = Vector2.Lerp(startSize, target.size, norm);

				await Task.Yield();
				resizeTime += Time.deltaTime;
			}

			Set(transform);
			resizeTime = 0;
		}

		private void Set(RectTransform transform)
		{
			transform.localPosition = target.position * direction;
			transform.sizeDelta = target.size;
		}
	}
}
