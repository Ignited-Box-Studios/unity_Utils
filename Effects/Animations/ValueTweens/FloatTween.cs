using UnityEngine;

namespace UnityUtils.ValueTweener
{
	public class FloatTween : BaseValueTween<float>
	{
		protected override float Lerp(float start, float end, float norm)
		{
			return Mathf.Lerp(start, end, norm);
		}
	}
}
