using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {
	public Transform currMount;
	public float speedFactor = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, currMount.position, speedFactor);
		transform.rotation = Quaternion.Slerp (transform.rotation, currMount.rotation, speedFactor);
	}

	public void setPos(Transform newMount){
		currMount = newMount;
	}


}
