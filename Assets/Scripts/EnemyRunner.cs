using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : PhysicsObject {
  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;
  public float velocityX = 0.3f;

  private Animator animator;
  private SpriteRenderer spriteRenderer;

  void Awake () {
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }

  protected override void ComputeVelocity() {
    Vector2 move = Vector2.zero;
    move.x = velocityX;

    bool flipSprite = spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f);
    if (flipSprite) {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
    targetVelocity = move * maxSpeed;
  }

  void OnCollisionEnter2D(Collision2D other) {
    Debug.Log(other.gameObject.tag);
    if(other.gameObject.tag == "BulletPlayer") {
      velocityX = 0.0f;
    }
  }
}
