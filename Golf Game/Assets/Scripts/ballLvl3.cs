using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLvl3 : MonoBehaviour {
	Vector3 P, P0, V, V0;
	float g, time;
	float elasticity;
	bool animation;
	public int power;
	GameObject[] targets;
	GameObject[] enemys;
	GameObject[] SecA;
	GameObject[] SecB;
	GameObject[] SecC;
	GameObject[] SecD;

	Score_Manager scoreScript;

	bool scored = false;

	// Use this for initialization
	void Start () {
		SecA = new GameObject[2];
		SecB = new GameObject[2];
		SecC = new GameObject[2];
		SecD = new GameObject[2];
		//adding the array of targets
		targets = GameObject.FindGameObjectsWithTag("target");
		enemys = GameObject.FindGameObjectsWithTag("enemy");

		GameObject scoreObject = GameObject.Find("GameManager");
		scoreScript = scoreObject.GetComponent<Score_Manager>();

		int a = 0;
		int b = 0;
		int c = 0;
		int d = 0;
		foreach (GameObject enemy in enemys) {
			float num = enemy.transform.position.x;
			float num2 = enemy.transform.position.z;

			print ("x : "+num);
			print ("z : "+num2);

			if (enemy.transform.position.x <= 0 && enemy.transform.position.z >= 0) {
				SecA [a] = enemy;
				print ("a:"+SecA[a]);
				a++;
			}
			if (enemy.transform.position.x <= 0 && enemy.transform.position.z <= 0) {
				SecB [b] = enemy;
				print ("b:"+SecB[b]);
				b++;
			}
			if (enemy.transform.position.x >= 0 && enemy.transform.position.z >= 0) {
				SecC [c] = enemy;
				print ("c:"+SecC[c]);
				c++;
			}
			if (enemy.transform.position.x >= 0 && enemy.transform.position.z <= 0) {
				SecD [d] = enemy;
				print ("d:"+SecD[d]);
				d++;
			}
		}
	
		power = 8;
		P0 = GameObject.Find ("Launcher").transform.position;
		g = 9.81f;
		V0 = GameObject.Find ("Launcher").transform.forward.normalized * power;
		elasticity = 0.8f;

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
		
			if (V.magnitude < .1)
				animation = false;
		}

	}
		

	void checkCollision()
	{
		checkCollisionWithGround ();
		checkTargetCollision ();
		checkCollisionWithWalls ();
		checkEnemyCollision ();
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

	void checkEnemyCollision(){
		if (this.transform.position.x <= 0 && this.transform.position.z >= 0) {
			for (int i = 0; i < SecA.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (SecA [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (SecA [i].name + "HITTTTTTT");

					Destroy (gameObject);

				}
			}
				
		}
		if (this.transform.position.x <= 0 && this.transform.position.z <= 0) {
			for (int i = 0; i < SecB.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (SecB [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (SecB [i].name + "HITTTTTTT");

					Destroy (gameObject);

				}
			}

		}
		if (this.transform.position.x >= 0 && this.transform.position.z <= 0) {
			for (int i = 0; i < SecD.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (SecD [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (SecD [i].name + "HITTTTTTT");

					Destroy (gameObject);

				}
			}

		}
		if (this.transform.position.x >= 0 && this.transform.position.z >= 0) {
			for (int i = 0; i < SecC.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (SecC [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (SecC [i].name + "HITTTTTTT");

					Destroy (gameObject);

				}
			}

		}
		/*
		if (this.transform.position.x <= 0 && this.transform.position.z <= 0) {
			SecB [b] = enemy;
			print ("b:"+SecB[b]);
			b++;
		}
		if (this.transform.position.x >= 0 && this.transform.position.z >= 0) {
			SecC [c] = enemy;
			print ("c:"+SecC[c]);
			c++;
		}
		if (this.transform.position.x >= 0 && this.transform.position.z <= 0) {
			SecD [d] = enemy;
			print ("d:"+SecD[d]);
			d++;
		}
		*/
	}

	//adding score to manager
	void addScore(){
		scoreScript.score += 2;
	}

	void checkTargetCollision(){
		int i = 0;
			if(scored == false){
			for (i = 0; i < targets.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (targets [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f) {
					print (targets [i].name + "HITTTTTTT");
					addScore ();
					scored = true;
					Destroy (targets[i]);
					Destroy (gameObject);
				}
			}
		}
	}

}
