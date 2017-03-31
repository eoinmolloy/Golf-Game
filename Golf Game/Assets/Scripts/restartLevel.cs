using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartLevel : MonoBehaviour {
	public string level;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (level);
		}
	}
}
