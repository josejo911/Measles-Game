using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Timer : MonoBehaviour {

	public Text UIText;
	public static float time = 90;
	int min = 0;
	int seg = 0;
	public AudioClip beeper;
	public AudioSource mysource;
	bool beepStarted = false;

	// Use this for initialization
	void Start () {
		mysource.playOnAwake = false;
		mysource.clip = beeper;
	}
	// Update is called once per frame
	void Update () {
		if (time > 0) {
			time -= Time.deltaTime;
			TimeSpan t = TimeSpan.FromSeconds (time);
			if (time <= 11) {
				if (!beepStarted) {
					StartCoroutine ("Beep");
					beepStarted = true;
				}
				UIText.color = Color.red;
			}
			string answer = string.Format ("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
			UIText.text = answer;
		} else {
			StopCoroutine ("Beep");
            LoadByIndex(4);

		}
		

	}

	IEnumerator Beep(){

		while (time > 0) {
			mysource.PlayOneShot (beeper);
			yield return new WaitForSeconds (1f);
		}
	}
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
