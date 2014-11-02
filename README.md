UnityResourceReference
======================

Allows you to use the Unity Inspector panel to link non-memory resident references of assets located in Resources folders.

###Classes

* ResourceTexture
* ResourceSprite
* ResourceAudioClip
* ResourceGameObject

###Usage
  In your Monobehaviour or ScripableObject instead of a direct reference to the asset, use one of the Resource types listed above. You can then drag the attach the appropriate asset using the inspector panel **Make sure your asset is within a Resources directory**.
  

