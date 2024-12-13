using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtils.ValueTweener
{
	public abstract class BaseValueTween<TValue> : IValueTween
	{
		[SerializeField] private TValue start;
		[SerializeField] private TValue end;
		[SerializeField] private float duration;
		[SerializeField] private AnimationCurve easing;

		[SerializeField] UnityEvent<TValue> setValue;

		protected BaseValueTween() { }

		public Coroutine StartCoroutine(MonoBehaviour behaviour)
		{
			return behaviour.StartCoroutine(Coroutine());
		}
		private IEnumerator Coroutine()
		{
			WaitForEndOfFrame frame = new WaitForEndOfFrame();
			float time = 0;
			while (time < duration && Validate())
			{
				Step(time);
				yield return frame;
				time = Tick(time);
			}

			Step(duration);
		}
		
		public async Task StartAsync()
		{
			float time = 0;
			while (time < duration && Validate())
			{
				Step(time);
				await Task.Yield();
				time = Tick(time);
			}

			Step(duration);
		}

		protected virtual bool Validate()
		{
			int count = setValue.GetPersistentEventCount();
			for (int i = 0; i < count; i++)
			{
				if (!setValue.GetPersistentTarget(i))
					return false;
			}

			return true;
		}
		protected virtual void Step(float time)
		{
			Set(Lerp(start, end, Ease(time / duration)));
		}
		protected virtual float Ease(float norm)
		{
			return easing.Evaluate(norm);
		}
		protected abstract TValue Lerp(TValue start, TValue end, float norm);
		protected virtual void Set(TValue value)
		{
			setValue.Invoke(value);
		}
		protected virtual float Tick(float time)
		{
			return time += Time.deltaTime;
		}
	}
}
