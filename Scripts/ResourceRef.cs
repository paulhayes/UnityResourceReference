using UnityEngine;
using System.Collections;

[System.Serializable]
public class ResourceRef : ScriptableObject
{
	public string path;
	public string GUID;
    	private Object cache;
	public T Get<T>() where T : Object
	{		 
		//Debug.Log("Runtime -> Loading Path:"+path+" GUID:"+GUID);
	    if (cache == null)
	    {
	        Load<T>();
	    }
	    return cache as T;
	}
	
	public ResourceRequest GetAsync<T>()
	{
		return Resources.LoadAsync(path,typeof(T));
	}
	
	public void Unload()
	{
	    	if (cache != null)
	    	{
            		Resources.UnloadAsset(cache);
        	}

	}

    	public void Load<T>() where T : Object
    	{
        	cache = (T)Resources.Load(path, typeof(T));
    	}

    	public virtual System.Type ResourceType()
	{
		return typeof(Object);
	}
	
	public void OnEnable()
	{
		hideFlags = HideFlags.HideAndDontSave;
	}
    
    public override string ToString ()
	{
		return string.Format ("[ResourceRef](path:{0},GUID:{1})",path,GUID);
	}
    
}

/*
[System.Serializable]
public class GameObjectResource : ResourceRef 
{
	public GameObject Get()
	{
		return Get<GameObject>();
	}
	
	public override System.Type ResourceType()
	{
		return typeof(GameObject);
    }
	
	
}
*/

/*
[System.Serializable]
public class TextAssetResource : ResourceRef 
{

	public TextAsset Get()
	{
		return Get<TextAsset>();
	}
	
	public override System.Type ResourceType()
	{
		return typeof(TextAsset);
    }
}

[System.Serializable]
public class AudioClipResource : ResourceRef 
{
	public AudioClip Get()
	{
		return Get<AudioClip>();
	}
	
	public override System.Type ResourceType()
	{
		return typeof(AudioClip);
    }
}

[System.Serializable]
public class TextureResource : ResourceRef 
{
	public Texture Get()
	{
		return Get<Texture>();
	}
	
	public override System.Type ResourceType()
	{
		return typeof(Texture);
    }
}
*/



