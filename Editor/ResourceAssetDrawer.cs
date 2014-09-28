using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;

[CustomPropertyDrawer(typeof(ResourceAsset),true)]
public class ResourceAssetDrawer : PropertyDrawer
{
	protected const string resourceDir = "/Resources/";
    

	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		EditorGUI.BeginProperty( pos, label, prop );
		
		System.Type assetType = typeof( Object );
		System.Type assetResourceType = typeof(ResourceAsset);
		
	
        
		if( assetResourceType.IsAssignableFrom( fieldInfo.FieldType ) ){
    
			MethodInfo assetTypeMethod = fieldInfo.FieldType.GetMethod("AssetType");
			if( assetTypeMethod != null && assetTypeMethod.IsStatic ){
				assetType = assetTypeMethod.Invoke(null,null) as System.Type;
            }
            else if( fieldInfo.FieldType.ContainsGenericParameters )
            {
            	System.Type[] fieldGenericParams = fieldInfo.FieldType.GetGenericArguments();
            	assetType = fieldGenericParams[0];
            }
        }
		
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
			/*
			 * If we land here then no asset is attached
			 */
			
		}
		
		EditorGUI.BeginChangeCheck ();
		
		Object asset = EditorGUI.ObjectField(pos,string.Format("{0}",label.text),existingAsset,assetType,false);
                                   
		
		if( EditorGUI.EndChangeCheck() ){
			if( asset == null ){
				pathProperty.stringValue = null;
				guidProperty.stringValue = null;
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

