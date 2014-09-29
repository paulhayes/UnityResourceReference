using UnityEngine;

[System.Serializable]
public class ResourceAsset
{
	public string GUID;
	public string path;
	protected Object cachedObject;
	
    public T Get<T>() where T : Object
    {
		if( cachedObject == null ){
			Load<T>();
		}
		
		return cachedObject as T;
    }
    
    public void Load<T>() where T : Object
    {
		cachedObject  = Resources.Load<T>(path);		
    }

    public void Unload()
    {
        if (cachedObject != null)
        {
            cachedObject = null;
            Resources.UnloadAsset(cachedObject);
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
	
	new public static System.Type AssetType()
	{
		return typeof(Texture);
    }
}



