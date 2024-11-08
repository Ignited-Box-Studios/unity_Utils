using UnityEngine;

namespace UnityUtils.UI.RectTransforms
{
	[RequireComponent(typeof(RectTransform)), ExecuteInEditMode]
	public class ContentWrapper : MonoBehaviour
	{
		[SerializeField] private bool wrapWidth;
		[SerializeField] private bool wrapHeight;
		[SerializeField] private bool shouldUpdate;

		private void Update()
		{
			if (shouldUpdate)
				Resize();
		}

		private void OnTransformChildrenChanged()
		{
			shouldUpdate = true;
		}

		public void Resize()
		{
			shouldUpdate = false;

			if (wrapWidth || wrapHeight)
			{
				RectTransform rect = transform as RectTransform;
				ContentWrapperUtils.ResizeAroundChildren(rect, wrapWidth, wrapHeight);
			}
		}
	}
}
