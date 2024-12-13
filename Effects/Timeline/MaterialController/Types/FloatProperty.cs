using UnityEngine;
using UnityUtils.Effects.VisualEffects;

namespace UnityUtils.Effects.Timeline.MaterialController.Types
{
	public class FloatProperty : IMaterialPropertyHandler
	{
		[SerializeField] private float value;

		public void OnUpdate(Material material, MaterialProperty property, float weigth)
		{
			property.Set(material, value * weigth);
		}
	}
}
