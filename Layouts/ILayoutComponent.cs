using UnityEngine;

namespace UnityUtils.Common.Layout
{
	public interface ILayoutComponent
	{
		void Reload(Transform transform, bool animate);
	}
}
