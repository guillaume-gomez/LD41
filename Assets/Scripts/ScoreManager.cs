using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

  Dictionary <string, Dictionary<string, int> > playerScores;
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
    Debug.Log(nbEnemys);
    for(int i = 0; i < nbEnemys; i++) {
      instance.SetScore("Enemy " + i, "kills", 9001 + i);
    }
    instance.SetScore("Murphy", "jsdfjdkf", 3989);
  }

  private void Init () {
    if(playerScores != null) {
      return;
    }
    playerScores = new Dictionary<string, Dictionary<string, int>>();
  }

  public int GetScore(string username, string scoreType) {
    Init();

    if(playerScores.ContainsKey(username) == false) {
      return 0;
    }

    if(playerScores[username].ContainsKey(scoreType) == false) {
      return 0;
    }
    return playerScores[username][scoreType];
  }

  public void SetScore(string username, string scoreType, int value) {
    Init();

    if(playerScores.ContainsKey(username) == false) {
      playerScores[username] = new Dictionary<string, int>();
    }
  }

  public void changeScore(string username, string scoreType, int amount) {
    Init();
    int currScore = GetScore(username, scoreType);
    SetScore(username, scoreType, currScore + amount);
  }

  public string[] GetPlayerNames() {
    return playerScores.Keys.ToArray();
  }
}
