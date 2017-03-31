using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class golf3 : MonoBehaviour 
{

	SpinProjectile golfBall;
	public float time = 0.0f;

	double rx, ry, rz, windX, windY, omega, radius = 0;

	float power;

	Vector3 P0, V0;

	void Start () 
	{

		double vx0 = V0.x;//5.2f;
		double vy0 = V0.z;//5.2f;
		double vz0 = V0.y;//10.0f;


		double mass = 20.0f;
		double area = .2f;
		double cd = .4f;
		double density = 1.2f;

		power = GameObject.Find ("Launcher").GetComponent<Launcher> ().power;
		windX = GameObject.Find ("Launcher").GetComponent<Launcher> ().windX ;
		windY = GameObject.Find ("Launcher").GetComponent<Launcher> ().windY;
		omega = GameObject.Find ("Launcher").GetComponent<Launcher> ().spinValue;

		GameObject thePlayer = GameObject.Find("Launcher");
		//Launcher newPower = thePlayer.GetComponent<Launcher>();
		//power = newPower.power;

		P0 = GameObject.Find ("Launcher").transform.position;
		V0 = GameObject.Find ("Launcher").transform.forward.normalized * power;
		float windFwd = 1;

		GameObject.Find ("Launcher").GetComponent<Launcher> ().spin = (float)rx;
		GameObject.Find ("Launcher").GetComponent<Launcher> ().spin = (float)ry;
		GameObject.Find ("Launcher").GetComponent<Launcher> ().spin = (float)rz;



		golfBall = new SpinProjectile(gameObject.transform.position.x, gameObject.transform.position.z, gameObject.transform.position.y,
			V0.x, V0.z, V0.y, 0.0, mass, area, density, cd, windX, windY, 0, 1, 0, omega, radius);
		
		gameObject.transform.position = new Vector3((float)golfBall.GetX(), (float)golfBall.GetZ(), (float)golfBall.GetY());
//		print ("Omega "+omega);


	}

	void Update () 
	{
		time+=Time.deltaTime/100;
		golfBall.UpdateLocationAndVelocity(time);	
		double vx,vy,vz;
		vx = golfBall.GetVx();
		vy = golfBall.GetVz();
		vz = golfBall.GetVy();

		Vector3 currentVelocity = new Vector3 ((float)vx, (float)vy, (float)vz);
		Vector3 currentPosition = new Vector3 ((float)golfBall.GetX (), (float)golfBall.GetZ (), (float)golfBall.GetY ());


		if (golfBall.GetZ () >= 1) {
			gameObject.transform.position = new Vector3 ((float)golfBall.GetX (), (float)golfBall.GetZ (), (float)golfBall.GetY ());
//			print ((float)golfBall.GetX ());
			//print ("x="+golfBall.GetX() +" y="+golfBall.GetZ());



		} else 
		{
			print ("HIt the ground once");
			GameObject.Find ("Launcher").GetComponent<Launcher> ().initialPositionfterFirstBounce = currentPosition;
			GameObject.Find ("Launcher").GetComponent<Launcher> ().initialVelocityAfterFirstBounce = new Vector3 (currentVelocity.x, - currentVelocity.y, currentVelocity.z);
			GameObject.Find ("Launcher").GetComponent<Launcher> ().instantiateNewBallShortRange (currentPosition, new Vector3 (currentVelocity.x, - currentVelocity.y, currentVelocity.z));

			Destroy (gameObject);

		}
	}


	void checkCollisionWithGround ()
	{
		/*
		if ((float)golfBall.GetY() <= 0.25 && (float)golfBall.GetVy() < 0) 
		{
			time = 0;
			P0 = golfBall.GetY();
			V0 = golfBall.GetVy();
			V0.y = - V0.y;

		}
		*/
	}

}
