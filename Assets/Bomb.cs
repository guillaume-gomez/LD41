﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

  public AudioClip[] explosionSounds;
  public AudioClip diveExplosionSounds;
  //a public float for the explosion radius public
  float explodeRadius = 3f;

  // Use this for initialization
  void Start () {
    Invoke("Explode", 2f);
    SoundManager.instance.PlaySingle(diveExplosionSounds);
    // anim = GetComponent <Animator>();
  }

  // Update is called once per frame
  private void OnTriggerEnter2D(Collider2D other) {
    //if (anim.GetCurrentAnimatorStateInfo (0).IsName ("bombdead")) {
      //destroy all the objects in a radius unless they are tagged Player or hand
      Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
      foreach(Collider2D col in colliders) {
        if (col.tag == "Player" || col.tag == "EnemyRunner")
        {
          GameObject gameobjCharacterBase = col.GetComponent<Collider2D>().gameObject;
          CharacterBase characterBase = gameobjCharacterBase.GetComponent<CharacterBase>();
          characterBase.Stop();
          Explode();
        } else if(col.tag == "Plateform") {
          Explode();
        }
      }
    //}
  }
  private void Explode() {
    SoundManager.instance.RandomizeSfx(explosionSounds);
    Destroy(this.gameObject);
  }

}
