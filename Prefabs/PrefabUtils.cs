﻿using UnityEngine;

namespace Assets.External.unity_utils.Prefabs
{
	public static class PrefabUtils
	{
		public static bool IsPrefab(this GameObject obj)
		{
			return !obj.scene.IsValid();
		}

		public static bool IsPrefab(this Component comp) => comp.gameObject.IsPrefab();
	}
}