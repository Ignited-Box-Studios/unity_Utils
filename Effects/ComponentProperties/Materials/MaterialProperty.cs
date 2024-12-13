using System;
using UnityEngine;

namespace UnityUtils.Effects.VisualEffects
{
	[Serializable]
	public struct MaterialProperty
	{
		[SerializeField] private Material material;
		[SerializeField] private string propertyName;
		[SerializeField, HideInInspector] private int propertyHash;

		public readonly int Hash => propertyHash;

		public readonly void Set<T>(T value) => Set(material, value);
		public readonly void Set<T>(Material material, T value)
		{
			material.TrySetProperty(propertyHash, value, false);
		}
	}
}
