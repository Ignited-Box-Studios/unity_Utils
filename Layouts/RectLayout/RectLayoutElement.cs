using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.RectUtils;
using UnityUtils.UI.Selectable;
using Utils.Logger;

namespace UnityUtils.Layouts.RectLayout
{
	public abstract class RectLayoutElement : MonoBehaviour, IAnimatedRectLayoutElement
	{
		public RectTransform Transform => transform as RectTransform;
		[field: SerializeField] public RectTransform.Axis Axis { get; private set; }
		[field: SerializeField] public Vector2Int OffsetDirection { get; private set; }

		[Header("Animation")]
		[field: SerializeField] public float AnimationDuration { get; private set; }
		public float AnimationTime { get; private set; }
		public Rect TargetRect { get; private set; }

		protected virtual void OnEnable() { }
		protected virtual void OnDisable() { }

		public void Toggle(Button button)
		{
			OnSetEnabled(enabled = !button.IsOn);
		}

		protected virtual void OnSetEnabled(bool enabled) { }

		public Rect GetRectLayout(Rect offset, RectTransform parent, bool animate)
		{
			if (!isActiveAndEnabled)
				return offset;

			TargetRect = Axis switch
			{
				RectTransform.Axis.Horizontal => GetHorizontalRectLayout(offset, parent),
				RectTransform.Axis.Vertical => GetVerticalRectLayout(offset, parent),
				_ => throw new ArgumentOutOfRangeException(nameof(Axis)),
			};

			if (animate)
			{
				this.AnimateResizeAsync().LogException();
			}
			else
			{
				SetRect(TargetRect);
			}

			return TargetRect;
		}

		protected abstract Rect GetVerticalRectLayout(Rect offset, RectTransform parent);
		protected abstract Rect GetHorizontalRectLayout(Rect offset, RectTransform parent);

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
			RectTransform transform = Transform;
			transform.localPosition = rect.position * OffsetDirection;
			transform.SetRect(rect);
		}
	}
}
