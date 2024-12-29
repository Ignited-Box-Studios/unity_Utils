using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.Animations.AnimationEvents;
using UnityUtils.PropertyAttributes;
using Utilities.Collections;

namespace UnityUtils.Effects.VisualEffects
{
	public class VisualEffectBehaviour : MonoBehaviour, IVisualEffect, IEnumerable<IVisualEffectComponent>
	{
		public bool IsPlaying { get; private set; }

		public Transform Root => transform;

		[SerializeReference, Polymorphic]
		protected IVisualEffectComponent[] components;

		[SerializeField]
		protected EffectComponent[] behaviours;

		[field: SerializeReference, Polymorphic(true)]
		public IVisualEffectEventHandler EventsHandler { get; set; }

		private void Awake()
		{
			EventsHandler?.Add<IVisualEffectParameters>(VisualEffectEvents.UpdateParameters, UpdateParameters);
		}

		private void OnDestroy()
		{
			EventsHandler?.Remove<IVisualEffectParameters>(VisualEffectEvents.UpdateParameters, UpdateParameters);
		}

		[ContextMenu("Play")]
		public virtual void Play()
		{
			foreach (IVisualEffectComponent component in this)
				component.Play();

			IsPlaying = true;
		}
		[ContextMenu("Stop")]
		public virtual void Stop()
		{
			IsPlaying = false;
			foreach (IVisualEffectComponent component in this)
				component.Stop();
		}

		public void UpdateParameters(IVisualEffectParameters parameters)
		{
			parameters.Apply(this);
		}

		public async void OnEnd(Action action)
		{
			if (action == null)
				return;

			while (IsPlaying)
				await Task.Yield();

			action();
		}

		public async Task AwaitEnd()
		{
			while (IsPlaying)
				await Task.Yield();
		}

		public T GetValue<T>(string component, int id)
		{
			IVisualEffectComponent comp = this.FirstOrDefault(c => c.Name == component);
			return comp != null ? comp.GetValue<T>(id) : default;
		}
		public void SetValue<T>(string component, int id, T value)
		{
			IVisualEffectComponent comp = this.FirstOrDefault(c => c.Name == component);
			comp?.SetValue(id, value);
		}
		public void SetAll<T>(int id, T value, bool isOptional = false)
		{
			foreach (IVisualEffectComponent component in this)
				component.SetValue(id, value, isOptional);
		}

		public void Dispose()
		{
			foreach (IVisualEffectComponent component in this)
				component.Dispose();
		}
		public virtual void Destroy()
		{
			Dispose();
			if (this && gameObject)
				Destroy(gameObject);
		}

		public IEnumerator<IVisualEffectComponent> GetEnumerator()
		{
			for (int i = 0; i < components.Length; i++)
				yield return components[i];

			for (int i = 0; i < behaviours.Length; i++)
				yield return behaviours[i];
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
