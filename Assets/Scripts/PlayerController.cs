using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase {
  public AudioClip[] shootSounds;

  public GameObject bulletPrefab;
  public Transform shotSpawner;
  public float coeff = 1.0f;

  private float fireRate = 0.5f;
  private float nextFire;

  public float Coeff { get; set; }

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

    bool flipSprite = spriteRenderer.flipX ? (move.x > 0.00f) : (move.x < 0.00f);
    if (flipSprite) {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
    targetVelocity = move * maxSpeed * coeff;
  }

  public override void Update() {
    base.Update();
    bool shooted = false;
    if( Input.GetButtonDown("Fire1") && Time.time > nextFire && !spriteRenderer.flipX) { // tweak with flipsprite
      shooted = true;
      nextFire = Time.time + fireRate;
      SoundManager.instance.RandomizeSfx(shootSounds);
      // to do set animation
      // create a bullet
      Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
    }
    animator.SetBool("shoot", shooted);
  }

  public override void Stop() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    animator.SetBool("hurt", true);
    coeff = 0.5f;
    Invoke("Cured", damageDuration);
  }

  public override void Cured() {
    animator.SetBool("hurt", false);
    coeff = 1.0f;
  }
}
