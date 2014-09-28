using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

[CustomPropertyDrawer(typeof(ResourceRef),true)]
public class ResourceRefDrawer : PropertyDrawer
{
	protected const string resourceDir = "/Resources/";
	protected float topLineHeight;
	protected float bottomLineHeight = 50f;
	protected bool nonResourceAsset = false;
	
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		topLineHeight = base.GetPropertyHeight(property, label);
		if( nonResourceAsset ){
			return topLineHeight + bottomLineHeight;
		}
		else {
			return topLineHeight;
		}
	}

	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		bool clear = false;
		
		ResourceRef existingRef = prop.objectReferenceValue as ResourceRef;
		
		Debug.LogWarning(string.Format("ResourceRefDrawer.OnGUI start, nonResourceAsset={0}, obj={1}, type={2},eventType={3}",nonResourceAsset,prop.objectReferenceValue,fieldInfo.FieldType.Name,Event.current.type
		                               ));
		
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
					if( nonResourceAsset ){
						nonResourceAsset = false;
						EditorUtility.SetDirty( prop.serializedObject.targetObject );						
					}
					
					if( existingValue == null ){
						Debug.LogWarning("existingValue was null "+assetPath+" ");
                    }
				}else {
					bool wasNonResourceAsset = nonResourceAsset;
					nonResourceAsset = true;
					if( nonResourceAsset && !wasNonResourceAsset ){
						EditorUtility.SetDirty( prop.serializedObject.targetObject );						
					}
					Debug.LogWarning(string.Format("assetPath {0} did not contain resources dir. GUID:{1}",assetPath,existingRef.GUID));
                }
            }
            
			        
		} else {						
			Debug.LogWarning("Creating new instance of "+this.fieldInfo.FieldType.Name);
			prop.objectReferenceValue = existingRef = ScriptableObject.CreateInstance<ResourceRef>();
		}
						
		EditorGUI.BeginChangeCheck ();

		Rect topBox = pos;
		Rect bottomBox = pos;
		
		if( nonResourceAsset ){
			topBox = new Rect {
				x = pos.x,
				y = pos.y,
				width = pos.width,
				height = pos.height - bottomLineHeight				
			};
			
			bottomBox = new Rect {
				x = pos.x,
				y = pos.y + topLineHeight,
				width = pos.width,
				height = pos.height - topLineHeight
			};
		}		
		Object obj = EditorGUI.ObjectField(topBox,label,existingValue,typeof(Object),false);
		
		if( nonResourceAsset ){
			EditorGUI.HelpBox( bottomBox, "Assets linked using ResourceRef, must reside within a resource directroy", MessageType.Warning );			
		}
		

		
		if( EditorGUI.EndChangeCheck() ){
			if( obj==null ){
				//Debug.LogWarning("Cleared value");
				prop.objectReferenceValue = existingRef = ScriptableObject.CreateInstance( this.fieldInfo.FieldType ) as ResourceRef;
				return;
			}
			string assetPath = AssetDatabase.GetAssetPath( obj );

			existingRef.GUID = AssetDatabase.AssetPathToGUID( assetPath );
			
			if(assetPath.Contains(resourceDir)){
				
				string resourcePath = assetPath.Substring( assetPath.IndexOf(resourceDir)+resourceDir.Length );
				resourcePath = resourcePath.Substring(0, resourcePath.Length - Path.GetExtension(assetPath).Length );
				
				existingRef.path = resourcePath;
				prop.objectReferenceValue = existingRef;
				Debug.Log (string.Format("Saving ResourceRef Path:{0}, GUID:{1}",existingRef.path,existingRef.GUID));
				
			}
			else {
				Debug.LogWarning(string.Format("could not save assetPath {0} did not contain resources dir. GUID:{1}",assetPath,existingRef.GUID));
				
				EditorUtility.SetDirty( prop.serializedObject.targetObject );
			}
			
			
			
		}		
		

		
		
		
	}
}
