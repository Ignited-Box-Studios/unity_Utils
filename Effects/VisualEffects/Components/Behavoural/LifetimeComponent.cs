﻿using System;
using System.Collections;
using UnityEngine;

namespace UnityUtils.Effects.VisualEffects
{
	[Serializable]
	public class LifetimeComponent : IVisualEffectComponent
	{
		public enum Action
		{
			None,
			Destroy,
			Shrink,
			Stop
		}

		public string Name => nameof(LifetimeComponent);

		[SerializeField] private VisualEffectBehaviour subject;
		[SerializeField] private float lifetime;
		[SerializeField] private Action action;

		private Coroutine lifetimeCoroutine;

		public T GetValue<T>(int id) => default;
		public void SetValue<T>(int id, T value, bool isOptional = false) { }

		public void Play()
		{
			if (lifetimeCoroutine != null || !subject || !subject.isActiveAndEnabled)
				return;

			lifetimeCoroutine = subject.StartCoroutine(CountdownLifetime());
		}

		public void Stop()
		{
			if (lifetimeCoroutine != null)
				subject.StopCoroutine(lifetimeCoroutine);
		}

		private IEnumerator CountdownLifetime()
		{
			return action switch
			{
				Action.Shrink => TweenScale(),
				Action.Destroy or Action.Stop => DelayedAction(),
				_ => NoneAction(),
			};
		}
		private IEnumerator NoneAction()
		{
			yield return new WaitForEndOfFrame();
		}
		private IEnumerator DelayedAction()
		{
			yield return new WaitForSeconds(lifetime);
			subject.Stop();
			switch (action)
			{
				case Action.Destroy:
					subject.Destroy();
					break;
			}
		}
		private IEnumerator TweenScale()
		{
			yield return new WaitForSeconds(lifetime);

			Vector3 target = Vector3.zero;
			Vector3 start = subject.transform.localScale;
			WaitForEndOfFrame frame = new WaitForEndOfFrame();

			const float duration = 0.2f;
			float time = 0;

			while (time < duration)
			{
				float norm = time / duration;
				subject.transform.localScale = Vector3.Lerp(start, target, norm);
				time += Time.deltaTime;
				yield return frame;
			}
		}
	}
}
