using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example : MonoBehaviour {

	public ResourceGameObject resourceAsset;
	public ResourceAsset<ExampleScriptableObject> scritableObjectExample;
	public ResourceAudioClip audioClipResource;
	public ResourceTexture textureResource;
	public List<ResourceGameObject> resourceList;
    
	void Start () 
	{
		
	}
	
	void Update () 
	{
		if( Input.GetMouseButtonDown(0) ){
			Instantiate( resourceAsset.Get<GameObject>(), transform.position, Quaternion.identity );            
		}
		
		
		Debug.Log ( Resources.FindObjectsOfTypeAll<GameObject>().Length );
	}
}
