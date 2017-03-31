using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour {
	public GameObject ball;
	public float power;
	public Slider slider;
	public Slider SpinSlider;
	public Text text;
	int ballRemain = 10;
	bool maxValue;
	bool SpinmaxValue;
	int ballPlayed = 0;
	float t;
	public Vector3 initialVelocityAfterFirstBounce;
	public Vector3 initialPositionfterFirstBounce;

	public GameObject ballDrag;

	public float spin;
	public double windX, windY;

	public Text wind;

	public float spinValue;

	// Use this for initialization
	void Start () {
		power = slider.value;
		spin = SpinSlider.value;
		text.text = "Balls Remaining : " + ballRemain;
		windX = Random.Range (-50f, 50f);
		windY = Random.Range (-0.5f, 10f);
		if (windX >= 30f) {
			wind.text = "Wind Speed : Strong Right Wind";
		}
		if (windX >= 0f && windX <= 30f) {
			wind.text = "Wind Speed : Weak Right Wind";
		}
		if (windX <= -30f) {
			wind.text = "Wind Speed : Strong Left Wind";
		}
		if (windX <= 0f && windX >= -30f) {
			wind.text = "Wind Speed : Weak Left Wind";
		}
//		print (windX);

	}

	public void instantiateNewBallShortRange(Vector3 startingPosition, Vector3 startingVelocity)
	{

		GameObject b = (GameObject)(Instantiate (ball, startingPosition, Quaternion.identity));
		//Destroy (b, 10);

	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		spinValue = SpinSlider.value;
		changeWindSpeed ();
		if (ballPlayed <= 9) {
			if (Input.GetMouseButton (0)) {
				if (maxValue == true) {
					slider.value += 0.1f;
					if (slider.value == 30) {
						maxValue = false;
					}
				}
				if (maxValue == false) {
					slider.value -= 0.1f;
					if (slider.value == 13) {
						maxValue = true;
					}
				}
					
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				if (SpinmaxValue == true) {
					SpinSlider.value += 5;
					if (SpinSlider.value == 100) {
						SpinmaxValue = false;
					}
				}
				if (SpinmaxValue == false) {
					SpinSlider.value -= 5;
					if (SpinSlider.value == -100) {
						SpinmaxValue = true;
					}
				}
			}
			power = slider.value;

			if (Input.GetKeyDown (KeyCode.F)) {
				//same pos as launcher

				GameObject b = (GameObject)(Instantiate (ballDrag, transform.position, Quaternion.identity));				
				ballPlayed++;
//				print (ballPlayed);
				ballRemain--;

				text.text = "Balls Remaining : " + ballRemain;
				if (ballRemain == 0) {
					text.text = "No balls remain, press R to restart";
				}

			}



		}

	}
	void changeWindSpeed(){
		if (t >= 5) {
			windX = Random.Range (-50f, 50f);
			windY = Random.Range (0.5f, 10f);
			if (windX >= 30f) {
				wind.text = "Wind Speed : Strong Right Wind";
			}
			if (windX >= 0f && windX <= 30f) {
				wind.text = "Wind Speed : Weak Right Wind";
			}
			if (windX <= -30f) {
				wind.text = "Wind Speed : Strong Left Wind";
			}
			if (windX <= 0f && windX >= -30f) {
				wind.text = "Wind Speed : Weak Left Wind";
			}
//			print (windX);
						
			t = 0;
		}
	
	}
}
