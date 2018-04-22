using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase {
  public AudioClip[] shootSounds;

  public GameObject bulletPrefab;
  public Transform shotSpawner;

  private float fireRate = 0.5f;
  private float nextFire;

  protected override void ComputeVelocity() {
    Vector2 move = Vector2.zero;
    move.x = Input.GetAxis("Horizontal");

    if(Input.GetButtonDown("Jump") && grounded) {
      SoundManager.instance.PlaySingle(jumpSound);
      velocity.y = jumpTakeOffSpeed;
    } else if (Input.GetButtonUp("Jump")) {
      if(velocity.y > 0) {
        velocity.y = velocity.y * 0.5f;
      }
    }

    bool flipSprite = spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f);
    if (flipSprite) {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
    targetVelocity = move * maxSpeed;
  }

  public override void Update() {
    base.Update();
    if( Input.GetButtonDown("Fire1") && Time.time > nextFire) {
      nextFire = Time.time + fireRate;
      SoundManager.instance.RandomizeSfx(shootSounds);
      // to do set animation
      // create a bullet
      Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
    }
  }

  public override void Stop() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    Invoke("Cured", damageDuration);
  }

  public override void Cured() {
    Debug.Log("Cured man !");
  }
}
