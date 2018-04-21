using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

  public Text timerText;
  private float startTimer;

	// Use this for initialization
	void Start () {
    startTimer = Time.time;
	}

	// Update is called once per frame
	void Update () {
    float t = Time.time - startTimer;

    string minutes = (((int) t) / 60).ToString();
    string seconds = (t % 60).ToString("f2");

    timerText.text = minutes + ": " + seconds;
	}

  public void Finnish() {
    timerText.color = Color.yellow;
  }
}
