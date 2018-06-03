using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : CharacterBase {
  public float velocityX = 0.3f;
  public bool jump = false;
  public float detectionDistance = 40.0f;
  private float currentVelocityX = 0.3f;
  private string name = "";


  protected override void ComputeVelocity() {
    Vector2 move = Vector2.zero;
    move.x = currentVelocityX;

    bool needJump = false;
    GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletEnemy");
    foreach(GameObject bullet in bullets) {
      if (
        bullet.transform.position.x > transform.position.x
        && Vector3.Distance(transform.position, bullet.transform.position) <= detectionDistance
      ) {
        needJump = true;
        break;
      }
    }
    if((jump || needJump) && grounded) {
      SoundManager.instance.PlaySingle(jumpSound);
      if(needJump) {
          velocity.y = jumpTakeOffSpeed * 0.75f;
        } else {
          velocity.y = jumpTakeOffSpeed;
        }
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

  public override void Bombed() {
    base.Suffer();
    Debug.Log("Bombed Ee");
    currentVelocityX = velocityX / 5;
    Invoke("Cured", 2.0f * damageDuration);
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

  public float DetectionDistance {
    set {
      detectionDistance = value;
    }
  }

  public string Name {
    set {
      name = value;
    }
    get {
      return name;
    }
  }
}
