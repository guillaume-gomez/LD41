using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScoreList : MonoBehaviour {

  public GameObject playerScoreEntryPrefab;
  ScoreManager scoremanager;

  // Use this for initialization
  void Start () {
    scoremanager = GameObject.FindObjectOfType<ScoreManager>();
  }
  // Update is called once per frame
  void Update () {
    if(scoremanager == null) {
      Debug.LogError("You forgot to add the score manager compoenent to a game object !");
      return;
    }

    while(this.transform.childCount > 0) {
      Transform c = this.transform.GetChild(0);
      c.SetParent(null);
      Destroy(c.gameObject);
    }


    string[] names = scoremanager.GetPlayerNames();
    foreach(string name in names) {
      GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
      go.transform.SetParent(this.transform);
      go.transform.Find("Username").GetComponent<Text>().text = name;
    }
  }
}
