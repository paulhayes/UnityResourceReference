using UnityEditor;
using UnityEngine;
using System.IO;

[CustomPropertyDrawer(typeof(ResourceAsset))]
public class ResourceAssetDrawer : PropertyDrawer
{
	protected const string resourceDir = "/Resources/";
    

	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		EditorGUI.BeginProperty( pos, label, prop );
		
		SerializedProperty pathProperty = prop.FindPropertyRelative("path");
		SerializedProperty guidProperty = prop.FindPropertyRelative("GUID");
		
		Object existingAsset = null;
		
		string assetPath = AssetDatabase.GUIDToAssetPath( guidProperty.stringValue );
		if( assetPath == null ){
			Debug.LogWarning("Could not resolve asset path from GUID");
		}
		else if( assetPath.Contains(resourceDir) ){
			existingAsset = AssetDatabase.LoadAssetAtPath(assetPath,typeof(Object));
			if( existingAsset == null ){
				Debug.LogWarning("existingValue was null "+assetPath+" ");
			}
		} 
		else {
			Debug.LogWarning(string.Format("assetPath {0} did not contain resources dir. GUID:{1}",assetPath,guidProperty.stringValue));
		}
		
		EditorGUI.BeginChangeCheck ();
		
		Object asset = EditorGUI.ObjectField(pos,label,existingAsset,typeof(Object),false);
		
		if( EditorGUI.EndChangeCheck() ){
			if( asset == null ){
			}
			else {
				assetPath = AssetDatabase.GetAssetPath( asset );
				guidProperty.stringValue = AssetDatabase.AssetPathToGUID( assetPath );
                
                if(assetPath.Contains(resourceDir)){
					
					string resourcePath = assetPath.Substring( assetPath.IndexOf(resourceDir)+resourceDir.Length );
					pathProperty.stringValue = resourcePath.Substring(0, resourcePath.Length - Path.GetExtension(assetPath).Length );
					
					Debug.Log (string.Format("Saving ResourceRef Path:{0}, GUID:{1}",assetPath,guidProperty.stringValue));
					
				}
				else {
					Debug.LogWarning(string.Format("could not save assetPath {0} did not contain resources dir. GUID:{1}",assetPath,guidProperty.stringValue));
                    
				}
			}
		}
		
		EditorGUI.EndProperty();
	}
}

