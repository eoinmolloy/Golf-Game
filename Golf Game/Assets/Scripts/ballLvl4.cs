using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLvl4 : MonoBehaviour {
	Vector3 P, P0, V, V0;
	float g, time;
	float elasticity;
	bool animation;
	float power;
	GameObject[] targets;

	Score_Manager scoreScript;



	// Use this for initialization
	void Start () {
		//adding the array of targets
		targets = GameObject.FindGameObjectsWithTag("target");
	
		GameObject thePlayer = GameObject.Find("Launcher");
		Launcher newPower = thePlayer.GetComponent<Launcher>();
		power = newPower.power;
		P0 = GameObject.Find ("Launcher").transform.position;
		g = 9.81f;
		V0 = GameObject.Find ("Launcher").transform.forward.normalized * power;
		elasticity = 0.8f;

		GameObject scoreObject = GameObject.Find("GameManager");
		scoreScript = scoreObject.GetComponent<Score_Manager>();

		animation = true;
	}




	Vector3 calculateVelocity (float t)
	{
		float x, y, z;
		x = V0.x;
		z = V0.z;
		y = -g * t + V0.y;

		return (new Vector3 (x, y, z));
	}

	Vector3 calculatePosition (float t)
	{
		float x, y, z;
		x = V0.x * t + P0.x;
		z = V0.z * t + P0.z;
		y = V0.y * t + P0.y - .5f * g * t*t;

		return (new Vector3 (x, y, z));
	}


	// Update is called once per frame
	void Update () {

		if (animation) {
			time += Time.deltaTime;

			P = calculatePosition (time);
			V = calculateVelocity (time);
			transform.position = P;
			CheckStairCollision ();
			checkCollision ();
			checkTargetCollision ();
		}


	}

	void checkCollision()
	{
		checkCollisionWithGround ();
		checkCollisionWithWalls ();
	}
	//checks collisions with the ground
	void checkCollisionWithGround ()
	{
		if (P.y <= 0.25 && V.y <0) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.y = - V0.y;

		}
	}
	//Checks Collisions with the walls
	void checkCollisionWithWalls ()
	{
		if (P.x <= -24.5) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.x = - V0.x;

		}
		if (P.x >= 24.5) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.x = - V0.x;

		}
		if (P.z <= -11) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.z = - V0.z;

		}

		if (P.z >= 11) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.z = - V0.z;

		}
	}

	void CheckStairCollision(){
		if (V.y < 0) {
			if (P.z <= 5 && P.z >= -5) {
				if (P.y <= 4 && P.x >= 8) {
					print ("x: " + P.x);
					print ("y: " + P.y);

					time = 0;
					P0 = P;
					V0 = V * elasticity;
					V0.y = -V0.y;
				}
				if (P.y <= 4 && P.x >= 5) {
					print ("x: " + P.x);
					print ("y: " + P.y);

					time = 0;
					P0 = P;
					V0 = V * elasticity;
					V0.y = -V0.y;
				}
				if (P.y <= 3 && P.x >= 3) {
					print ("x: " + P.x);
					print ("y: " + P.y);

					time = 0;
					P0 = P;
					V0 = V * elasticity;
					V0.y = -V0.y;
				}
				if (P.y <= 2 && P.x >= 1) {
					print ("x: " + P.x);
					print ("y: " + P.y);

					time = 0;
					P0 = P;
					V0 = V * elasticity;
					V0.y = -V0.y;
				}
				if (P.y <= 1 && P.x >= -1) {
					print ("x: " + P.x);
					print ("y: " + P.y);

					time = 0;
					P0 = P;
					V0 = V * elasticity;
					V0.y = -V0.y;
				}
			}
		}
	}

	//adding score to manager
	void addScore(){
		scoreScript.score += 5;
	}


	void checkTargetCollision(){
		int i = 0;
		bool hit = false;
		if (hit == false) {
			for (i = 0; i < targets.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (targets [i].transform.position, transform.position);
				print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (targets [i].name + "HITTTTTTT");
					hit = true;		
					addScore ();

					Destroy (targets[i]);
					Destroy (gameObject);
				}
			}
		}
	}

}
