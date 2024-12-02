using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtils.Effects.VisualEffects
{
	[Serializable]
	public class UnityEventComponent : IVisualEffectComponent
	{
		[SerializeField] private UnityEvent play;
		[SerializeField] private UnityEvent stop;

		public string Name => nameof(UnityEventComponent);

		public T GetValue<T>(int id)
		{
			return default;
		}

		public void Play()
		{
			play.Invoke();
		}
		public void Stop() 
		{
			stop.Invoke();
		}

		public void SetValue<T>(int id, T value, bool isOptional = false) { }
	}
}
