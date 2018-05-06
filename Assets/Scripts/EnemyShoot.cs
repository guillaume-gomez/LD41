using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
  public AudioClip[] shootSounds;
  public AudioClip[] ouchSounds;

  public GameObject bulletPrefab;
  public GameObject forbiddenSprite;
  public Transform shotSpawner;
  private GameObject targetToShoot;

  public float detectionDistance = 50.0f;
  protected Animator animator;
  private float fireRate = 1.0f;
  private float hurtOffset = 0.0f;
  private float nextFire;

  protected void Awake() {
     animator = GetComponent<Animator>();
     forbiddenSprite.SetActive(false);
  }

  void Start() {
     targetToShoot = GameObject.FindWithTag("Player");
  }

	// Update is called once per frame
  void Update () {
    if(!GameManager.instance.doingSetup) {
      if(Vector3.Distance(transform.position, targetToShoot.transform.position) <= detectionDistance && Time.time > (nextFire + hurtOffset)) {
        animator.SetBool("shoot", true);
        nextFire = Time.time + fireRate;
        SoundManager.instance.RandomizeSfx(shootSounds);
        // to do set animation
        // create a bullet
        Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
      } else {
        animator.SetBool("shoot", false);
      }
    }
	}

  public void Stop() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    forbiddenSprite.SetActive(true);
    hurtOffset = 5.0f;
    Invoke("Cured", 2.0f);
  }

  public void Cured() {
    hurtOffset = 0.0f;
    forbiddenSprite.SetActive(false);
  }


}
