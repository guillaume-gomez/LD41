using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

  protected Rigidbody2D rb2d;
  public float minNormalGroundY = 0.65f;
  public float gravityModifier = 1f;

  protected Vector2 targetVelocity;
  protected bool grounded = false;
  protected Vector2 groundNormal;

  protected Vector2 velocity;
  protected ContactFilter2D contactfilter;
  protected RaycastHit2D[] hitbuffer = new RaycastHit2D[16];
  protected List<RaycastHit2D> hitbufferList = new List<RaycastHit2D>(16);

  protected const float minMoveDistance = 0.001f;
  protected const float shellRadius = 0.01f;

  void OnEnable() {
    rb2d = GetComponent<Rigidbody2D>();
  }

  // Use this for initialization
  void Start() {
    contactfilter.useTriggers = false;
    contactfilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    contactfilter.useLayerMask = true;
  }

  // Update is called once per frame
  void Update() {
    targetVelocity = Vector2.zero;
    ComputeVelocity();
  }

  protected virtual void ComputeVelocity() {

  }

  void FixedUpdate() {
    velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
    velocity.x = targetVelocity.x;

    grounded = false;
    Vector2 deltaPosition = velocity * Time.deltaTime;

    // x
    Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
    Vector2 move = moveAlongGround * deltaPosition.x;
    Movement(move, false);

    // y
    move = Vector2.up * deltaPosition.y;
    Movement(move, true);
  }


  void Movement(Vector2 move, bool yMovement) {
    float distance = move.magnitude;
    if(distance > minMoveDistance) {
      int count = rb2d.Cast(move, contactfilter, hitbuffer, distance + shellRadius);
      hitbufferList.Clear();
      for(int i = 0; i < count; i++) {
        hitbufferList.Add(hitbuffer[i]);
      }

      for(int i = 0; i < hitbufferList.Count; i++) {
        Vector2 currentNormal = hitbufferList[i].normal;
        if(currentNormal.y > minNormalGroundY) {
          grounded = true;
          if(yMovement) {
            groundNormal = currentNormal;
            currentNormal.x = 0;
          }
        }
        float projection = Vector2.Dot(velocity, currentNormal);
        if(projection < 0.0f) {
          velocity = velocity - (projection * currentNormal);
        }

        float modifiedDistance = hitbufferList[i].distance - shellRadius;
        distance = modifiedDistance < distance ? modifiedDistance : distance;
      }
    }
    rb2d.position = rb2d.position + move.normalized * distance;
    // Debug.Log(rb2d.position);
  }

}


