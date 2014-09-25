using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

[CustomPropertyDrawer(typeof(ResourceRef),true)]
public class ResourceRefDrawer : PropertyDrawer
{
	protected const string resourceDir = "/Resources/";

	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
	
		//Debug.LogWarning("ResourceRefDrawer.OnGUI start");
		ResourceRef existingRef = prop.objectReferenceValue as ResourceRef;
				
		Object existingValue = null;
		
		if( existingRef != null ){
			if( existingRef.GUID == null ){
				//Debug.LogWarning("existingRef.GUID was null");
            }
            else{
				string assetPath = AssetDatabase.GUIDToAssetPath( existingRef.GUID );
				if( assetPath == null ){
					Debug.LogWarning("Could not resolve asset path from GUID");
				}
				else if( assetPath.Contains(resourceDir) ){
					existingValue = AssetDatabase.LoadAssetAtPath(assetPath,existingRef.ResourceType());
					
					if( existingValue == null ){
						Debug.LogWarning("existingValue was null "+assetPath+" ");
                    }
					
				}else {
					Debug.LogWarning(string.Format("assetPath {0} did not contain resources dir. GUID:{1}",assetPath,existingRef.GUID));
                }    
            }
            
			        
		}else {
			//Debug.LogWarning("Resetting: Creating new "+this.fieldInfo.FieldType.Name);
			prop.objectReferenceValue = existingRef = ScriptableObject.CreateInstance( this.fieldInfo.FieldType ) as ResourceRef;
        }
		
        
		EditorGUI.BeginChangeCheck ();
		//Object obj = null;
		
		Object obj = EditorGUI.ObjectField(pos,label,existingValue,existingRef.ResourceType(),false);	
		
		if( EditorGUI.EndChangeCheck() ){
			if( obj==null ){
				//Debug.LogWarning("Cleared value");
				prop.objectReferenceValue = existingRef = ScriptableObject.CreateInstance( this.fieldInfo.FieldType ) as ResourceRef;
				return;
			}
			string assetPath = AssetDatabase.GetAssetPath( obj );
			if(assetPath.Contains(resourceDir)){
				
				existingRef.GUID = AssetDatabase.AssetPathToGUID( assetPath );
				string resourcePath = assetPath.Substring( assetPath.IndexOf(resourceDir)+resourceDir.Length );
				resourcePath = resourcePath.Substring(0, resourcePath.Length - Path.GetExtension(assetPath).Length );
				
				existingRef.path = resourcePath;
				prop.objectReferenceValue = existingRef;
				Debug.Log (string.Format("Saving ResourceRef Path:{0}, GUID:{1}",existingRef.path,existingRef.GUID));
			}
			
		}
		/*
		if( existingRef != null )
		{
			Debug.Log (string.Format("ResourceRef Path:{0}, GUID:{1}",existingRef.path,existingRef.GUID));
		}
		*/
		//Debug.LogWarning("ResourceRefDrawer.OnGUI end");
        
	}
}
