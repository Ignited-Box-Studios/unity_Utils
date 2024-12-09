using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.StateMachines.Transitions
{
	public class FadeBehaviourTransition : IBehaviourStateTransition
	{
		private bool HasCanvasGroup => canvas;

		[SerializeField] private float fadeDuration;

		private CanvasGroup canvas;

		private Task Clear()
		{
			canvas = null;
			return Task.CompletedTask;
		}

		public Task Preload(MonoBehaviour behaviour)
		{
			canvas = behaviour.GetComponent<CanvasGroup>();
			canvas.alpha = 0;

			return Task.CompletedTask;
		}

		public Task Enter(MonoBehaviour behaviour)
		{
			if (!behaviour || !HasCanvasGroup)
				return Clear();

			return LerpAlphaAsync(canvas, 1);
		}

		public Task Exit(MonoBehaviour behaviour)
		{
			if (!behaviour || !HasCanvasGroup)
				return Clear();

			Task lerp = LerpAlphaAsync(canvas, 0);
			Clear();
			return lerp;
		}

		public async Task LerpAlphaAsync(CanvasGroup canvas, float alpha)
		{
			float time = 0;
			float startAlpha = canvas.alpha;
			while (canvas && canvas.gameObject && time < fadeDuration) 
			{
				canvas.alpha = Mathf.Lerp(startAlpha, alpha, time / fadeDuration);
				await Task.Yield();
				time += Time.deltaTime;
			}

			if (canvas)
				canvas.alpha = alpha;
		}
	}
}
