using System.Threading.Tasks;
using UnityEngine;
using UnityUtils.PropertyAttributes;

namespace UnityUtils.ValueTweener
{
	[ExecuteInEditMode]
	public class ValueTween : MonoBehaviour, IValueTween
	{
		[SerializeReference, Polymorphic]
		private IValueTween tween;

		public float Value { get => 0; 
			set
			{
				Debug.Log(value);
			}
		}

#if UNITY_EDITOR
		[SerializeField] private bool startCoroutine;
		private Coroutine coroutine;

		[SerializeField] private bool startAsync;
		private Task task;

		private void Update()
		{
			if (startCoroutine)
			{
				startCoroutine = false;
				if (coroutine != null)
					StopCoroutine(coroutine);
				coroutine = StartCoroutine(this);
			}

			if (startAsync)
			{
				startAsync = false;
				if (task == null || task.IsCompleted)
					task = StartAsync();
			}
		}
#endif

		public void StartCoroutine() => tween.StartCoroutine(this);
		public Coroutine StartCoroutine(MonoBehaviour behaviour)
		{
			return tween.StartCoroutine(behaviour);
		}

		public Task StartAsync()
		{
			return tween.StartAsync();
		}
	}
}
