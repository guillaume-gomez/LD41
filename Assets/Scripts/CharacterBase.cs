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

  protected override void ComputeVelocity() {
    base.ComputeVelocity();
  }

  public virtual void Stop() {
    SoundManager.instance.RandomizeSfx(ouchSounds);
    Invoke("Cured", damageDuration);
  }

  public virtual void Cured() {
  }

}
