using UnityEngine;
using System.Collections;

public class Golf2 : MonoBehaviour 
{
	
	DragProjectile golfBall;
	public float time = 0.0f;
	void Start () 
	{
		
		double vx0 = 5.2f;
		double vy0 = 5.2f;
		double vz0 = 10.0f;
		double mass = 20.0f;
		double area = .2f;
		double cd = .4f;
		double density = 1.2f;


    golfBall = new DragProjectile(gameObject.transform.position.x, gameObject.transform.position.z, gameObject.transform.position.y,
         vx0, vy0, vz0, 0.0, mass, area, density, cd);
		
		gameObject.transform.position = new Vector3((float)golfBall.GetX(), (float)golfBall.GetZ(), (float)golfBall.GetY());
		
		
	
	}
	
	void Update () 
	{
		time+=Time.deltaTime;
		golfBall.UpdateLocationAndVelocity(time);	
		
		if (golfBall.GetZ() >= 1)
		{
				gameObject.transform.position = new Vector3((float)golfBall.GetX(), (float)golfBall.GetZ(), (float)golfBall.GetY());
				print ("x="+golfBall.GetX() +" y="+golfBall.GetZ());
		}
	}
}
