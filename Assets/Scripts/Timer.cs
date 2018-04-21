using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

  public Text timerText;
  private float startTimer;
  public bool started = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
    if(started) {
      float t = Time.time - startTimer;

      string minutes = (((int) t) / 60).ToString();
      string seconds = (t % 60).ToString("f2");

      timerText.text = minutes + ": " + seconds;
    }
	}

  public void StopTimer() {
    timerText.color = Color.yellow;
    started = false;
  }

  public void StartTimer() {
    startTimer = Time.time;
    started = true;
  }
}
