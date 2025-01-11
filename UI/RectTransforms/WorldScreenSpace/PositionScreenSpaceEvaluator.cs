using System;
using UnityEngine;
using UnityUtils.GameObjects.Transforms;
using Utilities.Numbers;

namespace UnityUtils.UI.WorldScreenSpace
{
	[Serializable]
	public class PositionScreenSpaceEvaluator : IScreenSpaceEvaluator
	{
		public Vector3 position;
		public float size;

		public void Update(Camera camera, RectTransform transform)
		{
			Vector2 pos = camera.WorldToScreenPosition(position, out Vector2 _, out float distance);
			transform.position = pos;
			transform.localScale = Vector3.one * size.SafeDivide(distance);
		}
	}
}
