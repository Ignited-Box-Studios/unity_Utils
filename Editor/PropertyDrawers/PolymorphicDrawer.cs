﻿using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityUtils.Editor.SerializedProperties;
using UnityUtils.PropertyAttributes;
using UnityUtils.RectUtils;
using Utilities.Reflection;

namespace UnityUtils.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof(PolymorphicAttribute))]
	public class PolymorphicDrawer : ExtendedPropertyDrawer
	{
		protected override LabelDrawType LabelType => LabelDrawType.Foldout;

		private void Init(SerializedProperty prop, PolymorphicAttribute polyAttr)
		{
			string path = prop.propertyPath;

			if (polyAttr.IsInitialized)
			{
				return;
			}

			int listIndex = prop.GetIndex();
			object parent = prop.GetParent();
			polyAttr.SetFieldInfo(parent, fieldInfo, listIndex);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			PolymorphicAttribute polyAttr = attribute as PolymorphicAttribute;
			Init(property, polyAttr);

			if (polyAttr.SelectedType != null && polyAttr.SelectedType.TryGetAttribute(out ITooltipAttribute tooltip)) 
			{
				label = new GUIContent(label.text, tooltip.Tooltip);
			}

			int indent = EditorGUI.indentLevel;
			if (property.isArray)
				EditorGUI.indentLevel++;
			base.OnGUI(Indent(position), property, label);

			EditorGUI.indentLevel = indent;
		}

		protected override float DrawProperty(ref Rect position, SerializedProperty property, GUIContent label)
		{
			PolymorphicAttribute polyAttr = attribute as PolymorphicAttribute;
			bool drawChildren = !polyAttr.IgnoreChildren;
			float width = CalcLabelSize(property.displayName).x;
			PolyTypeDropdown(position.MoveX(width), polyAttr, property);

			if (drawChildren)
			{
				int indent = EditorGUI.indentLevel;
				EditorGUI.indentLevel++;
				position = Indent(position);

				position = position.MoveY(SpacedLineHeight).SetRemainderWidth(ViewWidth);
				position = DrawProperty(position, property);

				EditorGUI.indentLevel = indent;
				position = position.MoveY(-LineHeight);
			}

			EditorGUI.EndFoldoutHeaderGroup();
			return 0;
		}

		protected override bool DrawLabel(SerializedProperty property, GUIContent label, ref Rect position, int index, bool folded)
		{
			position = Indent(position);
			if (folded)
			{
				PolymorphicAttribute polyAttr = attribute as PolymorphicAttribute;
				Type type = polyAttr.GetFieldValue(index)?.GetType();
				if (type != null)
				{
					label = new GUIContent($"{label.text}: {type.Name}");
					position = position.SetWidth(CalcLabelSize(label.text).x);
				}
			}

			bool fold = base.DrawLabel(property, label, ref position, index, folded);
			position = Indent(position);
			return fold;
		}

		private Rect DrawProperty(Rect position, SerializedProperty property)
		{
			Type selectedType = property.boxedValue?.GetType();
			if (selectedType == null) return position;

			if (!selectedType.IsSubclassOf(typeof(UnityEngine.Object)))
			{
				DefaultGUI(ref position, property, property.depth + 1);
				return position;
			}
			
			position = position.MoveY(Spacing);

			object instance = fieldInfo.GetValue(property.GetParent());
			int index = property.GetIndex();

			UnityEngine.Object value = (instance is IList list ? list[index] : instance) as UnityEngine.Object;
			UnityEngine.Object newValue = EditorGUI.ObjectField(position, GUIContent.none, value, selectedType, true);

			if (newValue == value) return position;

			if (instance is IList nlist)
			{
				nlist[index] = newValue;
			}
			else
			{
				fieldInfo.SetValue(property.GetParent(), newValue); 
			}

			property.serializedObject.ApplyModifiedProperties();

			return position;
		}

		private Rect PolyTypeDropdown(Rect position, PolymorphicAttribute polyAttr, SerializedProperty property)
		{
			int listIndex = property.GetIndex();
			position = GetDropdownRect(position, property.depth);
			string option = polyAttr.Index == -1 ? "Null Reference" : polyAttr.Options[polyAttr.Index];
			float width = CalcLabelSize(option).x;
			Rect popupPos = position.SetWidth(width + 50);

			int index = EditorGUI.Popup(popupPos, polyAttr.Index, polyAttr.Options);

			if (index == polyAttr.Index) return position;
			
			//polyAttr.SetFieldInfo(property.GetParent(), fieldInfo, listIndex);
			if (polyAttr.ChangeIndex(index, listIndex, true, out var value))
			{
				property.boxedValue = value;
				property.serializedObject.ApplyModifiedProperties();
				property.serializedObject.Update();
			}

			return position;
		}

		private static Rect GetDropdownRect(Rect position, int depth)
		{
			Rect popupPos = position.MoveX(Spacing * 3);
			return popupPos.SetWidth(ViewWidth - popupPos.x);
		}
	}
}
