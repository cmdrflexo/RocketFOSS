using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Vector3 Speed;
    public bool Randomize = false;

    private void Start()
    {
        if (Randomize)
        {
            Vector3 speedTemp = new Vector3();
            speedTemp.x = Random.Range(-Speed.x, Speed.x);
            speedTemp.y = Random.Range(-Speed.y, Speed.y);
            speedTemp.z = Random.Range(-Speed.z, Speed.z);

            Speed = speedTemp;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Speed);
    }
}