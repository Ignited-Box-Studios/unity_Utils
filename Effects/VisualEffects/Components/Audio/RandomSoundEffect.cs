using UnityEngine;
using Utilities.Extensions;

namespace UnityUtils.Effects.VisualEffects
{
	public class RandomSoundEffect : EffectComponent
	{
		[SerializeField]
		private AudioComponent component;

		[SerializeField]
		private AudioClip[] clips;

		private void Replay()
		{
			if (component.Clip == null)
				Play();
			else component.Replay();
		}
		public override void Play()
		{
			component.Clip = clips.RandomElement();
			component.Play();
		}
		public override void Stop()
		{
			component.Stop();
		}

		public override T GetValue<T>(int id) => component.GetValue<T>(id);
		public override void SetValue<T>(int id, T value, bool isOptinal = false)
		{
			Replay();
			component.SetValue(id, value, isOptinal);
		}
	}
}
