using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExampleScriptableObjectEditor : Editor {

	[MenuItem("Assets/Create/ExampleScriptableObject")]
	protected static void CreateExampleScriptableObject()
	{
		ProjectWindowUtil.CreateAsset( new ExampleScriptableObject(), "new ExampleScriptableObject.asset");
	}
}
