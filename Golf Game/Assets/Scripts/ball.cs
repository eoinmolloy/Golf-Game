using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour {
	Vector3 P, P0, V, V0;
	float g, time;
	float elasticity;
	bool animation;
	float power;
	GameObject[] targets;
	int bounces;
	public Text text;
	public GameObject prefab;
	int trailNum = 1;
	float counter = 0.0f;
	Score_Manager scoreScript;

	public AudioClip soundToPlay;
	AudioSource audio;

	// Use this for initialization
	public void Start () {
		//adding the array of targets
		targets = GameObject.FindGameObjectsWithTag("target");
		bounces = 0;
		GameObject thePlayer = GameObject.Find("Launcher");
		Launcher newPower = thePlayer.GetComponent<Launcher>();
		power = newPower.power;
		print (power);
		P0 = GameObject.Find ("Launcher").transform.position;
		g = 9.81f;
		V0 = GameObject.Find ("Launcher").transform.forward.normalized * power;
		elasticity = 0.7f;

		GameObject scoreObject = GameObject.Find("GameManager");
		scoreScript = scoreObject.GetComponent<Score_Manager>();

		audio = GetComponent<AudioSource> ();
		animation = true;
	}
	//adding score to manager
	void addScore(){
		scoreScript.score += 1;
	}

	public void adjustPower(int newPower){
		power = newPower;
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


		
			if (V.magnitude < .5)
				animation = false;
		}


		counter += Time.deltaTime;
		if (counter > 0.5f) {
			Instantiate (prefab, P, Quaternion.identity);
			prefab.name = "trail" + trailNum;
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
		if (P.x <= -24.5 && V.x > 0) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.x = - V0.x;
			bounces++;
		}
		if (P.x >= 24.5 && V.x > 0) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.x = - V0.x;
			bounces++;
		}
		if (P.z <= -11) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.z = - V0.z;
			bounces++;
		}

		if (P.z >= 11) 
		{
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.z = - V0.z;
			bounces++;
		}
	}

	void checkTargetCollision(){
		int i = 0;
		bool hit = false;
		if (hit == false) {
			for (i = 0; i < targets.Length; i++) {
				//print (i + ":" + targets [i].name);			
				float dist = Vector3.Distance (targets [i].transform.position, transform.position);
				//print (targets[i].name +" : " + dist);

				if (dist < 1.0f && bounces == 2) {
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
