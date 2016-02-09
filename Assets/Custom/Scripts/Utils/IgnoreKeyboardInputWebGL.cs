using UnityEngine;
using System.Collections;

public class IgnoreKeyboardInputWebGL : MonoBehaviour {

	// Use this for initialization
	void Start () {
    #if !UNITY_EDITOR && UNITY_WEBGL
    WebGLInput.captureAllKeyboardInput = false;
    #endif
	}
}
