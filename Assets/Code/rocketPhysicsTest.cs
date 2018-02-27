using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketPhysicsTest : MonoBehaviour {

    public float sasStrength = 1.0f;
    public bool useSAS = true;
    public float thrust = 1.0f;
    public float throttleSensitivity = 0.01f;
    public GameObject ParticleSys;

    public GameObject navball_control;
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
            torqueVector = new Vector3(Input.GetAxis("ws"), -Input.GetAxis("ad"), -Input.GetAxis("qe"));
            rb.AddRelativeTorque(torqueVector * sasStrength);
        }
        else if(useSAS)
        {
            if (rb.angularVelocity.magnitude >= .2)
            {
                torqueVector = -rb.angularVelocity.normalized;
                rb.AddTorque(torqueVector * sasStrength);
            }
            else if(rb.angularVelocity.magnitude > 0)
            {
               rb.angularVelocity = Vector3.zero;
            }
        }
        torqueVector = Vector3.zero;

        //Thrust test

        if(Input.GetAxis("throttle") != 0)
        {
            throttlePosition = Mathf.Clamp01(throttlePosition + (Input.GetAxis("throttle") * throttleSensitivity));
        }

        if(thrust * throttlePosition > 0)
        {
            rb.AddRelativeForce(0, 0, thrust * throttlePosition);
        }

        if (ParticleSys)
        {
            ParticleSys.GetComponent<ParticleSystem>().startLifetime = throttlePosition;

            if(throttlePosition == 0)
            {
                ParticleSys.GetComponent<ParticleSystem>().enableEmission = false;
            }
            else
            {
                ParticleSys.GetComponent<ParticleSystem>().enableEmission = true;
            }
        }

        //Navball Control

        if (navball_control)
        {
            navball_control.GetComponent<navball_control>().setNewThrottlePosition(throttlePosition);
            navball_control.GetComponent<navball_control>().setNewVelocity(rb.velocity.magnitude);
            navball_control.GetComponent<navball_control>().setNewAltitude(transform.position.y);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("throttlemax"))
        {
            throttlePosition = 1.0f;
        }
        else if (Input.GetButtonDown("throttlemin"))
        {
            throttlePosition = 0.0f;
        }
    }
}
