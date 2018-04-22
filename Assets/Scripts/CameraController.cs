using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    private Vector3 originalCameraPosition;
    private float shakeAmt = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate () {
        transform.position = player.transform.position + offset;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        originalCameraPosition = transform.position;
        shakeAmt = coll.relativeVelocity.magnitude * .0025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if(shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        transform.position = originalCameraPosition;
    }
}
