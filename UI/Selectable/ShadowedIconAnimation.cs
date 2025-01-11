using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Storages.EnumPairLists;

namespace UnityUtils.UI.Selectable
{
	[System.Serializable]
	public class ShadowedIconAnimation : IButtonAnimations
	{
		[SerializeField] protected Graphic front;
		[SerializeField] private Graphic back;
		[SerializeField] private Vector2 offset;
		[SerializeField] private EnumPair<ButtonState, bool> isDown;

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
			try
			{
				bool isPressed = isDown[state];
				if (!front) return;
				front.rectTransform.localPosition = isPressed ? Vector3.zero : offset * back.rectTransform.rect.size;
			}
			catch (System.Exception e)
			{
				Debug.LogException(e, front.transform.parent.parent);
			}
		}
	}
}
