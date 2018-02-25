using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketPhysicsTest : MonoBehaviour {

    public float sasStrength = 1.0f;
    public bool useSAS = true;
    public float thrust = 1.0f;
    public float throttleSensitivity = 0.01f;
    public GameObject ParticleSys;
    private Vector3 torqueVector;
    private Rigidbody rb;

    private float throttlePosition = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        //Torquing test
		if(Input.GetAxis("ws") + Input.GetAxis("ad") + Input.GetAxis("qe") != 0)
        {
            torqueVector = new Vector3(Input.GetAxis("ws"), -Input.GetAxis("qe"), -Input.GetAxis("ad"));
            rb.AddRelativeTorque(torqueVector * sasStrength);
        }
        else if(useSAS)
        {
            var localAngularVelocity = transform.InverseTransformDirection(rb.angularVelocity).normalized;
            torqueVector = -localAngularVelocity;
            rb.AddRelativeTorque(torqueVector * sasStrength);
        }        

        torqueVector = Vector3.zero;

        //Thrust test

        if(Input.GetAxis("throttle") != 0)
        {
            throttlePosition = Mathf.Clamp01(throttlePosition + (Input.GetAxis("throttle") * throttleSensitivity));
        }
        if (Input.GetButtonDown("throttlemax"))
        {
            throttlePosition = 1.0f;
        }
        else if (Input.GetButtonDown("throttlemin"))
        {
            throttlePosition = 0.0f;
        }

        if(thrust * throttlePosition > 0)
        {
            rb.AddRelativeForce(0, thrust * throttlePosition, 0);
        }
        print("throttle: " + throttlePosition + "  --|--  thrust:" + throttlePosition * thrust);

        if (ParticleSys)
        {
            ParticleSys.GetComponent<ParticleSystem>().startLifetime = throttlePosition;

            if(throttlePosition == 0)
            {

            }
        }
    }
}
