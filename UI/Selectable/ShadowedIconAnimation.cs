using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.UI.Selectable
{
	[System.Serializable]
	public class ShadowedIconAnimation : IButtonAnimations
	{
		[SerializeField] protected Graphic front;
		[SerializeField] private Graphic back;
		[SerializeField] private Vector2 offset;

		public ShadowedIconAnimation() { }
		public ShadowedIconAnimation(IButtonAnimations poly) 
		{
			if (poly is ShadowedIconAnimation _poly)
			{
				front = _poly.front;
				back = _poly.back;
				offset = _poly.offset;
			}
		}

		public virtual void DoStateTransition(ButtonState state, bool animate)
		{
			bool isPressed = state is ButtonState.Selected or ButtonState.GroupSelected
				or ButtonState.Pressed or ButtonState.Highlighted;

			//if (back) back.enabled = !isPressed;
			if (!front) return;

			front.rectTransform.localPosition = isPressed ? Vector3.zero : offset * back.rectTransform.rect.size;
		}
	}
}
