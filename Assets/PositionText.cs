using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PositionText : MonoBehaviour {

  public Text positionText;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
    positionText.text = "Position : " + GameManager.instance.GetHeroPosition().ToString() + "/ " + GameManager.instance.NbEnemys.ToString();
  }
}
