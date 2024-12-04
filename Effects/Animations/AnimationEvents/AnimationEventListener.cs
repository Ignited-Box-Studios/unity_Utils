using UnityEngine;

namespace UnityUtils.Animations.AnimationEvents
{
	[RequireComponent(typeof(Animator))]
	public class AnimationEventListener : MonoBehaviour
	{
		private Animator animator;

		protected virtual void Awake()
		{
			if (!animator)
				animator = GetComponent<Animator>();
		}

		public virtual void OnAnimationEvent(AnimationEvent evnt)
		{
			if (evnt.objectReferenceParameter is ScriptableAnimationEvent behv)
			{
				behv.HandleEvent(this, animator, evnt);
			}
#if UNITY_EDITOR
			else if (evnt.objectReferenceParameter != null)
			{
				string evntObjTypeName = evnt.objectReferenceParameter.GetType().Name;
				Debug.LogWarning($"Object Reference {evntObjTypeName} is not an {nameof(ScriptableAnimationEvent)}");
			}
#endif
		}
	}
}
