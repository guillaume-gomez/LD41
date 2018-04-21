using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "EnemyRunner") {
      other.gameObject.SendMessage("Jump");
    }
  }
}
