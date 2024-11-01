#if UNITY_EDITOR
using UnityEditor.AssetImporters;

namespace UnityUtils.Serialization.WritableAssetFile
{
	public class WritableImporter : ScriptedImporter
	{
		public override void OnImportAsset(AssetImportContext ctx)
		{
			throw new System.NotImplementedException();
		}
	}
}
#endif