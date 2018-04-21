using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;
  public AudioClip jumpSound;

  public GameObject bulletPrefab;
  public Transform shotSpawner;

  private Animator animator;
  private SpriteRenderer spriteRenderer;

  private float fireRate = 0.5f;
  private float nextFire;

  void Awake () {
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }

  protected override void ComputeVelocity() {
    Vector2 move = Vector2.zero;
    move.x = Input.GetAxis("Horizontal");

    if(Input.GetButtonDown("Jump") && grounded) {
      velocity.y = jumpTakeOffSpeed;
    } else if (Input.GetButtonUp("Jump")) {
      if(velocity.y > 0) {
        SoundManager.instance.PlaySingle(jumpSound);
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
      // to do set animation
      // create a bullet
      Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
    }
  }
}
