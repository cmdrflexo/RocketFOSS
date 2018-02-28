using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour {

    public float semiMayorAxis = 0;
    public float semiMinorAxis = 0;
    public float eccentricity = 0;
    public float rMax = 0; //Apoapsis
    public float rMin = 0; //Periapsis
    public float inclinationNode = 0; //Values between 0 and 2PI
    public float inclinationAngle = 0; //Vallues between -PI/2 and PI/2
	public float mass = 0;
	public float currentPos = 0;
    float posX = 0;
    float posZ = 0;
    float posY = 0;
    float r;
    float timer = 0; //Values between 0 and 2PI
	float G = 6.67408f;
    public GameObject Object;
    public Transform offset;
    Vector3 prev;
    Vector3 temp;

    struct orbitPoint {
        public Vector3 position;
        public float velocity;
    }

    orbitPoint[] orbit;

	void generateOrbit() {
		int index = 0;
		semiMayorAxis = (rMax + rMin) / 2;
		semiMinorAxis = Mathf.Sqrt(rMax * rMin);
		eccentricity = Mathf.Sqrt(1 - (semiMinorAxis * semiMinorAxis) / (semiMayorAxis * semiMayorAxis));

		float movOffset = ((rMax + rMin) / 2) - rMin;

		while (timer < Mathf.PI * 2) {   
            posX = semiMayorAxis * Mathf.Cos(timer) - movOffset;
            posZ = semiMinorAxis * Mathf.Sin(timer);

			r = Mathf.Sqrt((posX - offset.position.x) * (posX - offset.position.x) + (posZ - offset.position.z) * (posZ - offset.position.z));

            float nPosX = posX * Mathf.Cos(inclinationAngle) - posY * Mathf.Sin(inclinationAngle);
            float nPosY = posY * Mathf.Cos(inclinationAngle) + posX * Mathf.Sin(inclinationAngle);

            float nPosX2 = nPosX * Mathf.Cos(inclinationNode) - posZ * Mathf.Sin(inclinationNode);
            float nPosZ = posZ * Mathf.Cos(inclinationNode) + nPosX * Mathf.Sin(inclinationNode);

            prev = temp;

            temp = new Vector3(nPosX2, nPosY, nPosZ);
			temp = temp + offset.position;
			float tempVel = Mathf.Sqrt((G * mass) * ((2 / r) - (1 / semiMayorAxis)));

			orbitPoint tempOrbitPoint;
			tempOrbitPoint.position = temp;
			tempOrbitPoint.velocity = tempVel;

			orbit[index] = tempOrbitPoint;

			//Debug.DrawLine(prev, temp, Color.red, 60);

			timer += (Mathf.PI * 2) / 720;
			index++;
        }
        timer = 0;

        //Send orbital points to LineRenderer to render orbit
        if (GetComponent<LineRenderer>())
        {
            GetComponent<LineRenderer>().positionCount = orbit.Length;
            for (int i = 0; i < orbit.Length; i++)
            {
                GetComponent<LineRenderer>().SetPosition(i, orbit[i].position);
            }
        }
    }

	void movePlanet() {
		int index1 = Mathf.FloorToInt(currentPos);
		int index2 = index1 + 1;
		float param = currentPos - index1;
		Vector3 currentPosition = (1 - param) * orbit[index1].position + param * (orbit[index2].position);
		Object.transform.position = currentPosition;

		float currentVel = (1 - param) * orbit[index1].velocity + param * (orbit[index2].velocity);

		currentPos += currentVel;

		if (currentPos > 720) {currentPos = 0;}
	}

    void Start () {
		orbit = new orbitPoint[721];
		generateOrbit();
    }

	void Update () {
        movePlanet();
    }
}
