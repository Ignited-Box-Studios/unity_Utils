using UnityEngine;
using UnityUtils.Effects.VisualEffects;

namespace UnityUtils.Effects.Timeline.MaterialController.Types
{
	public class CurvedFloatProperty : IMaterialPropertyHandler
	{
		[SerializeField] private AnimationCurve curve;

		public void OnUpdate(Material material, MaterialProperty property, float weigth)
		{
			property.Set(material, curve.Evaluate(weigth));
		}
	}
}
