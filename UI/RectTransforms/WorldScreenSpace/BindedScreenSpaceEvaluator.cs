using System;
using UnityEngine;
using UnityUtils.GameObjects.Transforms;

namespace UnityUtils.UI.WorldScreenSpace
{
	[Serializable]
	public class BindedScreenSpaceEvaluator : IScreenSpaceEvaluator
	{
		public bool IsTargetValid => target != null && target && target.gameObject.activeSelf;
		public Transform target;
		public Vector3 offset;

		public void Update(Camera camera, RectTransform transform)
		{
			Vector3 worldPos = target.transform.position + offset;
			Vector2 pos = camera.WorldToScreenPosition(worldPos, out Vector2 _, out float distance);
			bool inView = distance > 0;
			if (inView)
			{
				transform.position = pos;
				transform.localScale = Vector3.one;
			}
			else
			{
				transform.localScale = Vector3.zero;
			}
		}
	}
}
