using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class CountDown : MonoBehaviour {

  public int timeLeft = 3;
  public Text countdown;

  void Start () {
    StartCoroutine("LoseTime");
    Time.timeScale = 1; //Just making sure that the timeScale is right
  }

  void Update () {
    countdown.text = ("" + timeLeft); //Showing the Score on the Canvas
  }

  //Simple Coroutine
  IEnumerator LoseTime()
  {
    while (true) {
      yield return new WaitForSeconds (1);
      timeLeft--;
    }
  }
}