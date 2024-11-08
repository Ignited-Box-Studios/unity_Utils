using UnityEngine;

namespace UnityUtils.Common.Layout
{
	[ExecuteInEditMode]
	public abstract class LayoutController : MonoBehaviour
	{
		[SerializeField] private bool isReloadRequested;

		[SerializeField] private bool reloadOnValidate = true;
		[SerializeField] private bool reloadOnEnable = true;
		[SerializeField] private bool reloadOnChildrenChange = true;
		[SerializeField] private bool reloadOnResize = true;

		protected virtual void OnValidate() 
		{
			if (reloadOnValidate)
				isReloadRequested = true;
		}
		protected virtual void OnEnable()
		{
			if (reloadOnEnable)
				isReloadRequested = true;
		}
		protected virtual void OnTransformChildrenChanged()
		{
			if (reloadOnChildrenChange)
				isReloadRequested = true;
		}

		protected virtual void OnRectTransformDimensionsChange()
		{
			if (reloadOnResize)
				isReloadRequested = true;
		}

		protected virtual void Update()
		{
			if (isReloadRequested)
				ReloadLayout(false);
		}

		public virtual void ReloadLayout(bool animate = false)
		{
			isReloadRequested = false;
		}
	}
}
