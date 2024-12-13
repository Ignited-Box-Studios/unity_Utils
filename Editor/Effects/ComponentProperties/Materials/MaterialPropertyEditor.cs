#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;
using Utilities.Collections;
using Utils.Collections;
using UnityUtils.Editor;
using UnityUtils.RectUtils;

namespace UnityUtils.Effects.VisualEffects
{
	[CustomPropertyDrawer(typeof(MaterialProperty))]
	public class MaterialPropertyDrawer : ExtendedPropertyDrawer
	{
		Material mat;
		UnityEditor.MaterialProperty[] props;
		string[] names;

		int selected;

		private void UpdateProperties(Material mat)
		{
			this.mat = mat;
			props = MaterialEditor.GetMaterialProperties(new Object[] { mat });
			names = props.Select(p => p.name).ToArray();
		}

		private void UpdateSelected(string name)
		{
			selected = names.IndexOf(name);
		}

		protected override float DrawProperty(ref Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty material = property.FindPropertyRelative("material");
			position = position.MoveY(LineHeight);
			EditorGUI.PropertyField(position, material);
			SerializedProperty propName = property.FindPropertyRelative("propertyName");
			SerializedProperty propHash = property.FindPropertyRelative("propertyHash");

			if (material.objectReferenceValue is not Material mat)
				return 0;

			if (this.mat != mat)
			{
				UpdateProperties(mat);
				UpdateSelected(propName.stringValue);
			}

			position = position.MoveY(LineHeight);
			int selected = EditorGUI.Popup(position, this.selected, names);
			if (selected != this.selected)
			{
				this.selected = selected;
				string name = names[this.selected];
				propName.stringValue = name;
				propHash.intValue = Shader.PropertyToID(name);
				property.serializedObject.ApplyModifiedProperties();
			}

			return 0;
		}
	}
}
#endif