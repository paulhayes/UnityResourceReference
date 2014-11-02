UnityResourceReference
======================

Allows you to use the Unity Inspector panel to link non-memory resident references of assets located in Resources folders.

![](https://raw.githubusercontent.com/paulhayes/UnityResourceReference/master/screen.png)

###Classes

* ResourceTexture
* ResourceSprite
* ResourceAudioClip
* ResourceRuntimeAnimatorController
* ResourceTextAsset

###Usage
  In your Monobehaviour or ScripableObject instead of a direct reference to the asset, use one of the Resource types listed above. You can then drag the attach the appropriate asset using the inspector panel **Make sure your asset is within a Resources directory**.
  
When you want to get your hands on the asset you can either use the Get() function ( which is syncronous ) or LoadAsync which returns a resource ref. 

Please see [Example.cs](https://github.com/paulhayes/UnityResourceReference/blob/master/Example/Example.cs) for a more complete example of usage.

