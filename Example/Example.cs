using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example : MonoBehaviour {

	public ResourceAudioClip audioClipResource;
	public ResourceTexture textureResource;
	public ResourceSprite spriteResource;
	public ResourceRuntimeAnimatorController animatorControllerResource;
	public ResourceTextAsset textResource;
    
    private int memoryUsage;
    private int assetsCounted;
	private float memoryUpdateInterval = 0.1f;
    
    private string memoryInventory;
    private Vector2 scrollPosition;
	void Start () 
	{
		InvokeRepeating("RefreshMemoryUsage",0,memoryUpdateInterval);
	}
	
	void Update () 
	{
		
	}
	
	void RefreshMemoryUsage()
	{
		memoryInventory = "";
		memoryUsage = 0;
		assetsCounted = 0;
		#if ENABLE_PROFILER
		MemoryOf<Texture>();
		MemoryOf<AudioClip>();
		//Can't use Resources.FindObjectsOfTypeAll<TextAsset>(), Unity throws error
		//MemoryOf<TextAsset>();
		MemoryOf<ScriptableObject>();
		MemoryOf<RuntimeAnimatorController>();
		#endif
		
	}
	
	IEnumerator Load(){
		yield return null;
		audioClipResource.Get();
		textureResource.Get();
		spriteResource.Get();
		animatorControllerResource.Get();
		textResource.Get();
		
		gameObject.AddComponent<AudioSource>();
		gameObject.audio.clip = audioClipResource.Get();
		gameObject.audio.Play();
		
		renderer.material.mainTexture = textureResource.Get();
		
	}
	
	IEnumerator LoadAsync(){
		yield return null;
		yield return audioClipResource.LoadAsync();
		yield return textureResource.LoadAsync();
		yield return spriteResource.LoadAsync();
		yield return animatorControllerResource.LoadAsync();	
		yield return textResource.LoadAsync();
	}
	
	IEnumerator Unload(){
		yield return null;
		audioClipResource.Unload();
		textureResource.Unload();
		spriteResource.Unload();	
		animatorControllerResource.Unload();
		textResource.Unload();
	}
	
	void OnGUI(){
		if( GUILayout.Button("Load") ){
			StartCoroutine(Load());
		}
		if( GUILayout.Button("LoadAsync") ){
			StartCoroutine(LoadAsync());
		}
		if( GUILayout.Button("Unload") ){
			StartCoroutine(Unload());
		}
		
		GUILayout.Label(string.Format("{0} Assets counted, Total memory size {1:0.0}MB",assetsCounted,1f*memoryUsage/(1024*1024)));
		
		scrollPosition = GUILayout.BeginScrollView( scrollPosition );
		GUILayout.TextArea(memoryInventory);
		GUILayout.EndScrollView();
		
	}
	
	void MemoryOf<T>() where T : Object
	{
		string typeName = typeof(T).Name;			
		
        var objects = Resources.FindObjectsOfTypeAll<T>();
		foreach(var t in objects){
			
			assetsCounted++;
			int size = Profiler.GetRuntimeMemorySize(t);
			memoryUsage += size;
			memoryInventory+=string.Format("[{2}] {0}   {1}\n",size,t.name,typeName);
		}		
	}
	
	
}
