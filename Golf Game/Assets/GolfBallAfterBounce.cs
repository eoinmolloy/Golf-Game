using UnityEngine;
using System.Collections;
using System;


public class GolfBallAfterBounce : MonoBehaviour {
	Vector3 P, P0, V, V0, rough;
	float g, time;
	float elasticity;
	bool animation;



	// Use this for initialization
	void Start () {
		//P0 = GameObject.Find ("launcher").transform.position;
		P0 = gameObject.transform.position;
		//V0 = new Vector3 (5, 5, 0);
		g = 9.81f;
		float dis = Mathf.Sqrt((P0.x - rough.x) * (P0.x - rough.x) + 0 + (P0.z - rough.z) * (P0.z - rough.z));
		print(dis);
		if (dis > 35) {
			Destroy (gameObject);
		} else {
			elasticity = 0.5f;
		}
		//V0 = GameObject.Find ("launcher").transform.forward.normalized * 5;
		V0 = GameObject.Find ("Launcher").GetComponent<Launcher>().initialVelocityAfterFirstBounce * 0.6f;
		GameObject green = GameObject.Find ("HeavyRough");
		rough = green.transform.position;
		elasticity = 0.5f;
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
			//print ("Magnitude" + V.magnitude);
			transform.position = P;
			checkCollision ();
			if (V.magnitude < .1)
				animation = false;
		}

	}

	void checkCollision()
	{
		checkCollisionWithGround ();
	}

	void checkCollisionWithGround ()
	{
		float dis = Mathf.Sqrt((P.x - rough.x) * (P.x - rough.x) + 0 + (P.z - rough.z) * (P.z - rough.z));
		//print (dis);
		if (P.y <= .5 && V.y <0) 
		{
			if(dis < 35 && dis > 20){
				print (dis);
				print ("Heavy Rough");
				time = 0;
				elasticity = 0.2f;
				P0 = P;
				V0 = V*elasticity;
				V0.y = - V0.y;
			}
			if(dis < 20 && dis > 14){
				print (dis);
				print ("Rough");
				time = 0;
				elasticity = 0.4f;
				P0 = P;
				V0 = V*elasticity;
				V0.y = - V0.y;
			}
			if(dis < 14){
				print (dis);
				print ("GoodGreeen");
				time = 0;
				elasticity = 0.6f;
				P0 = P;
				V0 = V*elasticity;
				V0.y = - V0.y;
			}
			/*
			print (dis);
			print ("Collided with ground");
			time = 0;
			P0 = P;
			V0 = V*elasticity;
			V0.y = - V0.y;
			//print ("Position after collision" + P0);
			*/

		}

	}
		
}