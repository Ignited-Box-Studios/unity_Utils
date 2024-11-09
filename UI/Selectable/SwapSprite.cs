using UnityEngine;
using UnityUtils.PropertyAttributes;
using UnityUtils.UI.ImageComponents;

namespace UnityUtils.UI.Selectable
{
	public class SwapSprite : MonoBehaviour
	{
		[SerializeReference, Polymorphic]
		private IImageComponent image;

		[SerializeField] private Sprite selectedSprites;
		[SerializeField] private Sprite normalSprites;

		public void OnSelectedChanged(Button button)
		{
			image.OverrideSprite = button.IsOn ? selectedSprites : normalSprites;
		}
	}
}
