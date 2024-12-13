using UnityEngine;

namespace UnityUtils.Effects.VisualEffects
{
	public class MaterialPropertyHandler : MonoBehaviour
	{
		[SerializeField] private MaterialProperty property;

		public void SetFloat(float value)
		{
			property.Set(value);
		}
	}
}
