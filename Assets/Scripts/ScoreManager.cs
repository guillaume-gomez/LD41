using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

  Dictionary <string, Dictionary<string, string> > playerTimers;
  public static ScoreManager instance = null;

  void Awake() {
    if (instance == null)
    {
        instance = this;
    } else if (instance != this)
    {
        Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  void Start() {
    int nbEnemys = GameManager.instance.NbEnemys;
    for(int i = 0; i < nbEnemys; i++) {
      instance.SetTimer("Enemy " + i, "kills", "9001");
    }
    instance.SetTimer("Murphy", "jsdfjdkf", "3989");
  }

  private void Init () {
    if(playerTimers != null) {
      return;
    }
    playerTimers = new Dictionary<string, Dictionary<string, string>>();
  }

  public string GetTimer(string username, string position) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      return "";
    }

    if(playerTimers[username].ContainsKey(position) == false) {
      return "";
    }
    return playerTimers[username][position];
  }

  public void SetTimer(string username, string position, string value) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      playerTimers[username] = new Dictionary<string, string>();
    }
  }

  public void changeTimer(string username, string position, string amount) {
    Init();
    string currTimer = GetTimer(username, position);
    SetTimer(username, position, currTimer + amount);
  }

  public string[] GetPlayerNames() {
    return playerTimers.Keys.ToArray();
  }
}
