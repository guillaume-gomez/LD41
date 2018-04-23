using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchStory : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Invoke("ToStoryScreen", 10f);
  }

  // Update is called once per frame
  void Update () {
     if(Input.GetButtonDown("Submit")) {
       ToStoryScreen();
     }
  }

  void ToStoryScreen() {
    SceneManager.LoadScene((int)ScreensEnum.HistoryScreen, LoadSceneMode.Single);
  }
}
