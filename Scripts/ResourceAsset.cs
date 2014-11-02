using UnityEngine;

[System.Serializable]
public class ResourceAsset
{
	public string GUID;
	public string path;
	protected Object cachedObject;
	protected ResourceRequest resourceRequest;
	
    public T Get<T>() where T : Object
    {
		if( cachedObject == null ){
			Load<T>();
		}
		
		return cachedObject as T;
    }
    
    public void Load<T>() where T : Object
    {
		if( string.IsNullOrEmpty( path ) ){
			Debug.LogWarning("No asset linked to this ResourceAsset");
			return;
		}
		
		cachedObject  = Resources.Load<T>(path);		
    }
    
    public ResourceRequest LoadAsync<T>() where T:Object
    {
    	if( string.IsNullOrEmpty( path ) ){
    		Debug.LogWarning("No asset linked to this ResourceAsset");
    		return null;
    	}
   		return resourceRequest = Resources.LoadAsync<T>(path);
    }

    public void Unload()
    {
        if (cachedObject != null)
        {
			if( cachedObject is Sprite ){
				Sprite cachedSprite = cachedObject as Sprite;
				if(cachedSprite!=null){
					Resources.UnloadAsset(cachedSprite.texture);
				}
			}
            if( !( cachedObject is GameObject ) ){
				Resources.UnloadAsset(cachedObject);
				if( resourceRequest != null ) Resources.UnloadAsset(resourceRequest.asset);
        	}
        	
			
			cachedObject = null;
			resourceRequest = null;
        }
    }

    public static System.Type AssetType()
    {
    	return typeof(Object);
    }
}

[System.Serializable]
public class ResourceAsset<T> : ResourceAsset where T : Object
{
	public T Get()
	{
		return Get<T>();
	}
}

[System.Serializable]
public class ResourceGameObject : ResourceAsset
{
	public GameObject Get()
	{
		return Get<GameObject>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<GameObject>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(GameObject);
    }
}

[System.Serializable]
public class ResourceAudioClip : ResourceAsset
{
	public AudioClip Get()
	{
		return Get<AudioClip>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<AudioClip>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(AudioClip);
    }
}

[System.Serializable]
public class ResourceTextAsset : ResourceAsset
{
	public TextAsset Get()
	{
		return Get<TextAsset>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<TextAsset>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(TextAsset);
    }
}

[System.Serializable]
public class ResourceTexture : ResourceAsset
{
	public Texture Get()
	{
		return Get<Texture>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<Texture>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(Texture);
    }
}

[System.Serializable]
public class ResourceSprite : ResourceAsset
{
	public Sprite Get()
	{
		return Get<Sprite>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<Sprite>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(Sprite);
	}
}

[System.Serializable]
public class ResourceRuntimeAnimatorController : ResourceAsset
{
	public RuntimeAnimatorController Get()
	{
		return Get<RuntimeAnimatorController>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<RuntimeAnimatorController>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(RuntimeAnimatorController);
	}
}

[System.Serializable]
public class ResourceScriptableObject : ResourceAsset
{
	public ScriptableObject Get()
	{
		return Get<ScriptableObject>();
	}
	
	public ResourceRequest LoadAsync()
	{
		return LoadAsync<ScriptableObject>();
	}
	
	new public static System.Type AssetType()
	{
		return typeof(ScriptableObject);
	}
}


