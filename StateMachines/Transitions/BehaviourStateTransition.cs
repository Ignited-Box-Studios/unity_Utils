using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.StateMachines.Transitions
{
	public interface IBehaviourStateTransition
	{
		Task Preload(MonoBehaviour behaviour);
		Task Enter(MonoBehaviour behaviour);
		Task Exit(MonoBehaviour behaviour);
	}
}
