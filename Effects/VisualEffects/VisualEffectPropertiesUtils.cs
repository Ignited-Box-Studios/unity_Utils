using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Utils.Delegates;

namespace UnityUtils.Effects.VisualEffects
{
	public static class VisualEffectPropertiesUtils
	{
		private static readonly Dictionary<Type, IPropertyDelegate> delegates = new()
		{
			[typeof(Gradient)] = new IdProperty<VisualEffect, Gradient>(
				(comp, id) => comp.HasGradient(id),
				(comp, id) => comp.GetGradient(id),
				(comp, id, value) => comp.SetGradient(id, value)
				),
			[typeof(float)] = new IdProperty<VisualEffect, float>(
				(comp, id) => comp.HasFloat(id),
				(comp, id) => comp.GetFloat(id),
				(comp, id, value) => comp.SetFloat(id, value)
				),
			[typeof(int)] = new IdProperty<VisualEffect, int>(
				(comp, id) => comp.HasInt(id),
				(comp, id) => comp.GetInt(id),
				(comp, id, value) => comp.SetInt(id, value)
				),
			[typeof(Vector2)] = new IdProperty<VisualEffect, Vector2>(
				(comp, id) => comp.HasVector2(id),
				(comp, id) => comp.GetVector2(id),
				(comp, id, value) => comp.SetVector2(id, value)
				),
			[typeof(Vector3)] = new IdProperty<VisualEffect, Vector3>(
				(comp, id) => comp.HasVector3(id),
				(comp, id) => comp.GetVector3(id),
				(comp, id, value) => comp.SetVector3(id, value)
				),
			[typeof(Vector4)] = new IdProperty<VisualEffect, Vector4>(
				(comp, id) => comp.HasVector4(id),
				(comp, id) => comp.GetVector4(id),
				(comp, id, value) => comp.SetVector4(id, value)
				),
			[typeof(SkinnedMeshRenderer)] = new IdProperty<VisualEffect, SkinnedMeshRenderer>(
				(comp, id) => comp.HasSkinnedMeshRenderer(id),
				(comp, id) => comp.GetSkinnedMeshRenderer(id),
				(comp, id, value) => comp.SetSkinnedMeshRenderer(id, value)
				),
		};

		public static T GetProperty<T>(this VisualEffect vfx, int id)
		{
			if (!vfx) return default;
			vfx.TryGetProperty(id, out T value);
			return value;
		}

		public static T GetProperty<T>(this VisualEffect vfx, string id)
		{
			if (!vfx) return default;
			vfx.TryGetProperty(id, out T value);
			return value;
		}

		public static bool TryGetProperty<T>(this VisualEffect vfx, string name, out T value)
			=> vfx.TryGetProperty(Shader.PropertyToID(name), out value);

		public static bool TryGetProperty<T>(this VisualEffect vfx, int id, out T value)
		{
			if (!vfx)
			{
				value = default;
				return false;
			}

			try
			{
				var prop = delegates.GetIdProperty<VisualEffect, T>();
				value = prop.Get(vfx, id);
				return true;
			}
			catch (MissingPropertyException mpe)
			{
				Debug.LogException(mpe);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}

			value = default;
			return false;
		}

		public static bool TrySetProperty<T>(this VisualEffect vfx, string name, T value, bool log)
			=> vfx.TrySetProperty(Shader.PropertyToID(name), value, log);

		public static bool TrySetProperty<T>(this VisualEffect vfx, int id, T value, bool log)
		{
			if (!vfx) return false;

			try
			{
				var prop = delegates.GetIdProperty<VisualEffect, T>();
				prop.Set(vfx, id, value);
				return true;
			}
			catch (MissingPropertyException mpe)
			{
				if (log) Debug.LogException(mpe);
			}
			catch (Exception e)
			{
				if (log) Debug.LogException(e);
			}

			return false;
		}
	}
}
