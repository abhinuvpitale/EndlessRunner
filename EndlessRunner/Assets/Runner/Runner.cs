using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {

    // Keeps a track of distance travelled that can be used by the Skyline Manager Script.
    public static float distanceTraveled;
    public float acceleration;
    public Vector3 jumpVelocity;
    Rigidbody rigidbody;
    private bool touchingPlatform;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        // transform.Translate(5f * Time.deltaTime, 0f, 0f);        // Doesnt work so well with Physics, added ForceMode to compensate
        if (touchingPlatform && Input.GetButtonDown("Jump"))
        {            
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatform = false;

        }
        distanceTraveled = transform.localPosition.x;

    }
    void FixedUpdate()
    {
        if (touchingPlatform)
        {            
            rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter()
    {
        touchingPlatform = true;
    }

    void OnCollisionExit()
    {
        touchingPlatform = false;
    }

}
