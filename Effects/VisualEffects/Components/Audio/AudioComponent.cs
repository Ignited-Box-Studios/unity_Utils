using System;
using UnityEngine;
using UnityUtils.Effects.VisualEffects.ParameterFunctions;
using UnityUtils.PropertyAttributes;

namespace UnityUtils.Effects.VisualEffects
{
	public interface IAudioParameterFunctions : IParameterFunctions<AudioSource> { }

	[Serializable]
	public class AudioComponent : IVisualEffectComponent
	{
		[SerializeField]
		private AudioSource source;

		[SerializeReference, Polymorphic(true), HideInInspector]
		private IAudioParameterFunctions functions;

		[SerializeReference, Polymorphic(true)]
		private IAudioParameterFunctions audioFunctions;

		public string Name => source.clip.name;
		public AudioClip Clip 
		{
			get => source.clip;
			set => source.clip = value;
		}
		public bool IsPlaying => source.isPlaying;
		private IAudioParameterFunctions Functions => audioFunctions ?? functions;

		public void Replay()
		{
			source.time = 0;
		}
		public void Play()
		{
			source.enabled = true;
			if (source.gameObject.activeInHierarchy)
				source.Play();
		}
		public void Stop()
		{
			source.enabled = false;
		}

		public T GetValue<T>(int id)
		{
			return Functions == null ? default : Functions.GetValue<T>(source, id);
		}
		public void SetValue<T>(int id, T value, bool isOptional = false)
		{
			Functions?.SetValue(source, id, value, isOptional);
		}
	}
}
