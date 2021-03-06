﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {
  // a public object array for which objects to spawn
  public GameObject[] obj;
  //min and max times for another spawn
  public float spawnMin = 2f;
  public float spawnMax = 3f;

  public bool randomizeCall = true;

  // Use this for initialization
  void Start () {
    //Spawn();
  }

  void Spawn() {
    float rand = Random.Range (0, 1000);
    //if random number is greater than 700 make a bomb
    if (rand > 700) {
      Instantiate (obj [Random.Range (0, obj.GetLength (0))], transform.position, Quaternion.identity);
    }
    if(randomizeCall) {
      //invoke spawn at random time interval between min and max
      Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Player" || other.gameObject.tag == "EnemyRunner") {
      Spawn();
    }
  }
}