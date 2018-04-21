using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : PhysicsObject {
  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 30;
  public float velocityX = 0.3f;
  public AudioClip jumpSound;
  public bool jump = false;

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

    if(jump && grounded) {
      SoundManager.instance.PlaySingle(jumpSound);
      velocity.y = jumpTakeOffSpeed;
    } else if (jump) {
      if(velocity.y > 0) {
        velocity.y = velocity.y * 0.5f;
        jump = false;
      }
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
    targetVelocity = move * maxSpeed;
  }

  void Stop() {
    velocityX = 0.0f;
  }

  void Jump() {
    jump = true;
  }
}
