using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.ValueTweener
{
	public interface IValueTween
	{
		Coroutine StartCoroutine(MonoBehaviour behaviour);
		Task StartAsync();
	}
}
