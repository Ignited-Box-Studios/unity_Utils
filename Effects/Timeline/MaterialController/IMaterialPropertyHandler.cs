using UnityEngine;
using UnityUtils.Effects.VisualEffects;

namespace UnityUtils.Effects.Timeline.MaterialController
{
	public interface IMaterialPropertyHandler
	{
		public void OnUpdate(Material material, MaterialProperty property, float weigth);
	}
}
