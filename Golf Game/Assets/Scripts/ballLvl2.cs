using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLvl2 : MonoBehaviour {
	Vector3 P, P0, V, V0;
	float g, time;
	float elasticity;
	bool animation;
	float power;
	GameObject[] targets;
	Score_Manager scoreScript;

	bool scored;


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
			checkCollision ();
			checkTargetCollision ();

			if (V.magnitude < .05) {
				animation = false;
			}
		}


	}

	void checkCollision()
	{
		checkCollisionWithGround ();
		checkTargetCollision ();
		checkCollisionWithWalls ();
	}
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
	//adding score to manager
	void addScore(){
		scoreScript.score += 2;
	}

	void checkTargetCollision(){
		if(scored == false){
			if (V.y < 0) {
				if (P.x <= -21.5 && P.x >= -22.5) {
					if (P.y >= 2.4 && P.y <= 2.6) {
						if (P.z <= 0.5 && P.z >= -0.5) {
							time = 0;
							P0 = P;
							V0.x = 0;
							V0.z = 0;
							V0.y = -2;
							addScore ();
							scored = true;
						}
					}
				}
			}
		}
	}

}
