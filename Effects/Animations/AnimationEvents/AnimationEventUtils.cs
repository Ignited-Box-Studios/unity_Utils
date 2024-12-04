using System.Linq;
using UnityEngine;

namespace UnityUtils.Animations.AnimationEvents
{
	public static class AnimationEventUtils
	{
		public static AnimationEvent FindEventWithName(this AnimationClip clip, string name)
		{
			return clip.events.FirstOrDefault(e => e.stringParameter == name ||
				(e.objectReferenceParameter is ScriptableAnimationEvent evnt && evnt.HasSubEvent(name))
			);
		}
	}
}
