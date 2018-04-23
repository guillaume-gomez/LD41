using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase {
  public AudioClip[] shootSounds;

  public GameObject bulletPrefab;
  public Transform shotSpawner;
  public float coeff = 1.0f;

  private float fireRate = 0.8f;
  private float nextFire;
  private Vector3 originalSpawner;

  public float Coeff { get; set; }

  protected virtual void Awake () {
    base.Awake();
    originalSpawner = shotSpawner.position;
  }

  protected override void ComputeVelocity() {
    bool startJump = false;
    Vector2 move = Vector2.zero;
    move.x = Input.GetAxis("Horizontal");

    if(Input.GetButtonDown("Jump") && grounded) {
      SoundManager.instance.PlaySingle(jumpSound);
      velocity.y = jumpTakeOffSpeed;
      startJump = true;
    } else if (Input.GetButtonUp("Jump")) {
      if(velocity.y > 0) {
        velocity.y = velocity.y * 0.5f;
      }
    }

    bool flipSprite = spriteRenderer.flipX ? (move.x > 0.00f) : (move.x < 0.00f);
    if (flipSprite) {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    animator.SetBool("startJump", startJump);
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
      if(targetVelocity.x > 0.01f) {
        shotSpawner.position = new Vector3(shotSpawner.position.x, originalSpawner.y - 0.55f, 0);
      } else {
        shotSpawner.position = new Vector3(shotSpawner.position.x, originalSpawner.y, 0);
        originalSpawner.y = shotSpawner.position.y;
      }
      // to do set animation
      // create a bullet
      Invoke("Shoot", 0.3f);
    }
    animator.SetBool("shoot", shooted);
  }

  private void Shoot() {
    Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
  }

  public override void Stop() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    animator.SetBool("hurt", true);
    coeff = 0.5f;
    Invoke("Cured", damageDuration);
  }

  public override void Suffer() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    coeff = 0.5f;
    Invoke("Cured", damageDuration);
  }

  public override void Cured() {
    animator.SetBool("hurt", false);
    coeff = 1.0f;
  }
}
