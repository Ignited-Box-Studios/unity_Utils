using UnityEditor;
using UnityEngine;

namespace UnityUtils.GameObjects.Transforms
{
	public class TransformContextOptions
	{
		private const string debugMenu = "CONTEXT/Transform/Debug/";

		[MenuItem(debugMenu + nameof(DrawVectors), validate = true)]
		[MenuItem(debugMenu + nameof(LogWorldValues), validate = true)]
		private static bool IsTransform()
		{
			return Selection.activeTransform != null;
		}

		[MenuItem(debugMenu + nameof(DrawVectors))]
		public static bool DrawVectors()
		{
			Transform target = Selection.activeTransform;
			Debug.DrawRay(target.position, target.up, Color.green, 10);
			Debug.DrawRay(target.position, target.right, Color.red, 10);
			Debug.DrawRay(target.position, target.forward, Color.blue, 10);

			return true;
		}

		[MenuItem(debugMenu + nameof(LogWorldValues))]
		public static bool LogWorldValues()
		{
			Transform target = Selection.activeTransform;
			Debug.Log($"Position: {target.position}\nRotation: {target.rotation.eulerAngles}\nScale: {target.lossyScale}");

			return true;
		}
	}
}
