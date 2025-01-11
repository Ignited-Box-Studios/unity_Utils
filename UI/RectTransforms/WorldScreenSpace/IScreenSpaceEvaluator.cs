using UnityEngine;

namespace UnityUtils.UI.WorldScreenSpace
{
	public interface IScreenSpaceEvaluator
	{
		void Update(Camera camera, RectTransform transform);
	}
}
