using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsTest : MonoBehaviour {

    public Vector3 initialForce;
    public GameObject parentObject;
    public float gravity = 1.0f;
    private Rigidbody rb;
    private Vector3 lr;
	// Use this for initialization
	private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(initialForce);
	}

    private void Update()
    {
        Vector3 rPos = (parentObject.transform.position - transform.position).normalized;

        float gForce = gravity * (rb.mass * parentObject.GetComponent<Rigidbody>().mass) / Vector3.Distance(this.transform.position, parentObject.transform.position);

        rb.AddForce(rPos * gForce);
    }
}
