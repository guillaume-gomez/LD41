using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour {

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Player") {
      GameManager.instance.Win();
    }
  }
}
