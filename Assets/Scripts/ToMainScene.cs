using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ToMainScene : MonoBehaviour {

  // Use this for initialization
  void Start () {
    Invoke("GoToMainScene", 30f);
  }

  // Update is called once per frame
  void Update () {
     if(Input.GetButtonDown("Submit")) {
       GoToMainScene();
     }
  }

  void GoToMainScene() {
    SceneManager.LoadScene((int)ScreensEnum.MainScreen, LoadSceneMode.Single);
  }
}
