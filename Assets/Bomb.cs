using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

  public AudioClip[] explosionSounds;
  public AudioClip diveExplosionSounds;
  public GameObject explosionBombPrefab;
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
        }
      }

      if(other.gameObject.tag == "Plateform") {
        Explode();
      }
    //}
  }
  private void Explode() {
    SoundManager.instance.RandomizeSfx(explosionSounds);
    Destroy(this.gameObject);
    Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    GameObject explosiongm = Instantiate(explosionBombPrefab, explosionPosition, transform.rotation);
    Destroy(explosiongm, 1.5f);
  }

}
