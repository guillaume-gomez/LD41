using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : PhysicsObject {
  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;
  public float damageDuration = 0.8f;
  public AudioClip jumpSound;
  public AudioClip[] ouchSounds;

  protected Animator animator;
  protected SpriteRenderer spriteRenderer;

  protected virtual void Awake () {
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }

  public virtual void Update ()
  {
    if(!GameManager.instance.doingSetup) {
      targetVelocity = Vector2.zero;
      ComputeVelocity ();
    }
  }

  protected override void ComputeVelocity() {
    base.ComputeVelocity();
  }

  public virtual void Suffer() {
  }

  public virtual void Stop() {
  }

  public virtual void Cured() {
  }

  public virtual void Bombed() {
  }

}
