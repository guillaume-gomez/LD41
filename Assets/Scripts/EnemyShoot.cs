using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
  public AudioClip[] shootSounds;

  public GameObject bulletPrefab;
  public Transform shotSpawner;
  public GameObject targetToShoot;

  public float detectionDistance = 50.0f;
  private float fireRate = 1.0f;
  private float nextFire;

	// Update is called once per frame
	void Update () {
    if(Vector3.Distance(transform.position, targetToShoot.transform.position) <= detectionDistance && Time.time > nextFire) {
      nextFire = Time.time + fireRate;
      SoundManager.instance.RandomizeSfx(shootSounds);
      // to do set animation
      // create a bullet
      Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
    }
	}

  void Stop() {
    Debug.Log("JE SUIS BLESSE");
  }

}
