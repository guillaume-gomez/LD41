using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

  Dictionary <string, DataScore > playerTimers;
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

  public void CreateScore(bool playerWon, string timer, int position) {
    Init();
    playerTimers.Clear();
    int nbEnemys = GameManager.instance.NbEnemys;
    if(playerWon) {
      instance.SetPosition("Murphy", 1);
      instance.SetTimer("Murphy", timer);
      for(int i = 0; i < nbEnemys; i++) {
        instance.SetPosition("Enemy " + i, i + 2);
        instance.SetTimer("Enemy " + i, "Too   late");
      }
    } else {
      for(int i = 0; i < nbEnemys + 1; i++) {
        if(i == 0) {
          instance.SetPosition("Enemy " + i, 1);
          instance.SetTimer("Enemy " + i, timer);
        } else if(i == (position - 1)) {
          instance.SetPosition("Murphy", position);
          instance.SetTimer("Murphy", "Too   late");
        } else {
        instance.SetPosition("Enemy " + i, i + 1);
        instance.SetTimer("Enemy " + i, "Too   late");
        }
      }
    }
  }

  private void Init () {
    if(playerTimers != null) {
      return;
    }
    playerTimers = new Dictionary<string, DataScore>();
  }

  public string GetTimer(string username) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      return "";
    }

    return playerTimers[username].timer;
  }

  public int GetPosition(string username) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      return 0;
    }

    return playerTimers[username].position;
  }

  public void SetTimer(string username, string value) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      playerTimers[username] = new DataScore();
    }
    playerTimers[username].timer = value;
  }

  public void SetPosition(string username, int position) {
    Init();

    if(playerTimers.ContainsKey(username) == false) {
      playerTimers[username] = new DataScore();
    }
    playerTimers[username].position = position;
  }

  public void changeTimer(string username, string amount) {
    Init();
    string currTimer = GetTimer(username);
    SetTimer(username, currTimer + amount);
  }

  public string[] GetPlayerNames() {
    return playerTimers.Keys.ToArray();
  }
}
