﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public float speed = 10;
  public int damage = 1;
  public float destroyTime = 1.5f;

  // Use this for initialization
  void Start () {
    Destroy(gameObject, destroyTime);
  }

  // Update is called once per frame
  void Update () {
    transform.Translate(Vector3.right * speed * Time.deltaTime);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "EnemyRunner") {
      other.gameObject.SendMessage("Stop");
      Destroy(gameObject);
    }
  }
}
