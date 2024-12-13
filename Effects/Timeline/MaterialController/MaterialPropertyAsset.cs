using UnityEngine;
using UnityEngine.Playables;
using UnityUtils.Effects.VisualEffects;
using UnityUtils.PropertyAttributes;

namespace UnityUtils.Effects.Timeline.MaterialController
{
	public class MaterialPropertyAsset : PlayableAsset
	{
		[field: SerializeField]
		public ExposedReference<Material> Material { get; private set; }

		[field: SerializeField]
		public MaterialProperty TargetProperty { get; private set; }

		[field: SerializeReference, Polymorphic]
		public IMaterialPropertyHandler Value { get; private set; }

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			ScriptPlayable<MeterialPropertyBehaviour> playable = ScriptPlayable<MeterialPropertyBehaviour>.Create(graph);

			MeterialPropertyBehaviour behaviour = playable.GetBehaviour();
			behaviour.Apply(this, graph.GetResolver());

			return playable;
		}
	}
}
