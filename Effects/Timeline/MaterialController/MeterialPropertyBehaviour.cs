using UnityEngine;
using UnityEngine.Playables;
using UnityUtils.Effects.VisualEffects;

namespace UnityUtils.Effects.Timeline.MaterialController
{
	public class MeterialPropertyBehaviour : PlayableBehaviour
	{
		private Material material;
		private MaterialPropertyAsset asset;
		private MaterialProperty property => asset.TargetProperty;
		private IMaterialPropertyHandler handler => asset.Value;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			base.ProcessFrame(playable, info, playerData);
			handler.OnUpdate(material, property, info.weight);
		}

		public void Apply(MaterialPropertyAsset asset, IExposedPropertyTable exposedPropertyTable)
		{
			material = asset.Material.Resolve(exposedPropertyTable);
			this.asset = asset;
		}
	}
}
