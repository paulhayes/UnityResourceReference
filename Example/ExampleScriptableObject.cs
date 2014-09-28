using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ExampleScriptableObject : ScriptableObject {
	public ResourceRef foo;
	public ResourceRef bar;
	public List<ResourceRef> refs;
	
}
