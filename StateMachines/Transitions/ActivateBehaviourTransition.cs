using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.StateMachines.Transitions
{
	public class ActivateBehaviourTransition : IBehaviourStateTransition
	{
		public Task Preload(MonoBehaviour behaviour) => Exit(behaviour);

		public Task Enter(MonoBehaviour behaviour)
		{
			if (behaviour)
				behaviour.gameObject.SetActive(true);

			return Task.CompletedTask;
		}

		public Task Exit(MonoBehaviour behaviour)
		{
			if (behaviour)
				behaviour.gameObject.SetActive(false);

			return Task.CompletedTask;
		}
	}
}
