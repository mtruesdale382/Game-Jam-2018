using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBob : MonoBehaviour {

    Transform myTransform;
    Vector3 initialPosition;

    public float amplitude = 0.5f;
    public float bobDivisor = 2.0f;
    public float spinSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        myTransform = gameObject.transform;
        initialPosition = myTransform.position;
    }
	
	void FixedUpdate () {
        float yOffset = Mathf.Sin(Time.realtimeSinceStartup * bobDivisor) * amplitude;

        myTransform.position = initialPosition + new Vector3(0.0f, yOffset, 0.0f);
        myTransform.Rotate(0.0f, spinSpeed, 0.0f);
    }
}
