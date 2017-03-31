using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score_Manager : MonoBehaviour {
	public Text Textscore;
	public int score = 0;
	public string level;
	// Use this for initialization
	void Start () {
		Textscore.text = "Score : "+score;
	}
	
	// Update is called once per frame
	void Update () {
		Textscore.text = "Score : "+score;
		if (score >= 10) {
			SceneManager.LoadScene (level);
		}
	}


}
