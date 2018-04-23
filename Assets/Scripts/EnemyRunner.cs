using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : CharacterBase {
  public float velocityX = 0.3f;
  public bool jump = false;
  private float currentVelocityX = 0.3f;

  protected override void ComputeVelocity() {
    Vector2 move = Vector2.zero;
    move.x = currentVelocityX;

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

  public override void Stop() {
    base.Stop();
    currentVelocityX = 0.0f;
    Invoke("Cured", damageDuration);
  }

  public override void Suffer() {
    base.Suffer();
    currentVelocityX = velocityX / 2;
    Invoke("Cured", damageDuration);
  }

  public override void Cured() {
    currentVelocityX = velocityX;
  }

  void Jump() {
    jump = true;
  }

  public float VelocityX {
    set {
      velocityX = value;
      currentVelocityX = value;
    }
  }
}
