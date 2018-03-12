using UnityEngine;
using RFOSSCore;
using RFOSSCore.Constants;
using RFOSSCore.Orbits;

[RequireComponent(typeof(LineRenderer))]
public class TESTPlanetaryArrangement : MonoBehaviour {

	public PrecisePosition shipPosition;
	public GameObject ship;
	public double massShip;
	public Vector3 velocity = new Vector3(0, 0, 3.62f);
	
	double g = PhysicsConstants.g;

	public float scaleMultiplier = 1e-05f;

	public BodyInfo[] bodies;
	BodyInfo influencingBody;

	Vector3[] path = new Vector3[3000];
	LineRenderer lineRenderer;

	// ///
	OrbitPoint[] orbitPoints;
	// ///

	void Start() {
		lineRenderer = GetComponent<LineRenderer>();

		OrbitUtility orbitUtility = new OrbitUtility();
		orbitPoints = orbitUtility.GenerateOrbit(bodies[0]);


	}

	void Update() {
		
		foreach(BodyInfo body in bodies)
			if(WithinSOI(body))
				influencingBody = body;

		//while(i < target) {
		//	//minDistance = 10000000;

		//	for(int k = 0; k < planets.Count; k++) {
		//		planets[k].GO.GetComponent<PlanetOrbit>().movePlanetAdd();
		//		double distance = Vector3.Distance(
		//			currentPos - planets[ReferenceIndex].GO.transform.position,
		//			planets[k].GO.GetComponent<PlanetOrbit>().newPosAdd -
		//				planets[ReferenceIndex].GO.transform.position
		//		);
		//		if(distance < minDistance && distance < planets[k].SOI) {
		//			minDistance = distance;
		//			id = k;
		//		}
		//	}

		//	minDistance = Vector3.Distance(currentPos - planets[ReferenceIndex].GO.transform.position, planets[id].GO.GetComponent<PlanetOrbit>().newPosAdd - planets[ReferenceIndex].GO.transform.position);

		//	double force = g * (massShip * planets[id].mass) / (minDistance * minDistance);

		//	Vector3 direction = (planets[id].GO.GetComponent<PlanetOrbit>().newPosAdd - planets[ReferenceIndex].GO.transform.position) - (currentPos - planets[ReferenceIndex].GO.transform.position);

		//	direction.Normalize();

		//	velocity += (direction * (float)force);

		//	currentPos += velocity;

		//	if(i == 0) { ship.transform.position = currentPos; tempVel = velocity; }

		//	Color temp;
		//	if(id == 0) {
		//		temp.r = 0;
		//		temp.g = 0;
		//		temp.b = 1;
		//		temp.a = 1;
		//	} else if(id == 1) {
		//		temp.r = 1;
		//		temp.g = 0;
		//		temp.b = 0;
		//		temp.a = 1;
		//	} else if(id == 2) {
		//		temp.r = 0;
		//		temp.g = 1;
		//		temp.b = 0;
		//		temp.a = 1;
		//	} else {
		//		temp.r = 1;
		//		temp.g = 1;
		//		temp.b = 1;
		//		temp.a = 1;
		//	}

		//	path[i] = currentPos;

		//	if(minDistance < planets[id].diameter) { break; }

		//	i++;
		//}

		//for(int k = 0; k < planets.Count; k++) {
		//	planets[k].GO.GetComponent<PlanetOrbit>().reset();
		//}

		//velocity = tempVel;

		//LR.SetPositions(path);
		//LR.positionCount = i;

		//Vector3 dir = path[1] - path[0];

		//if(Input.GetKey(KeyCode.W)) {
		//	velocity += dir * 0.005f;
		//}

		//if(Input.GetKey(KeyCode.S)) {
		//	velocity -= dir * 0.005f;
		//}

		//i = 0;
	}

	public float SOIScaler = 1;
	private bool WithinSOI(BodyInfo body) {
		double distance = PrecisePosition.Distance(shipPosition, body.precisePosition);
		float SOI = body.radius * SOIScaler;
		return distance < SOI;
	}

	private void OnDrawGizmos() {
		if(bodies.Length > 0) {
			Gizmos.color = Color.green;
			foreach(BodyInfo body in bodies)
				Gizmos.DrawWireSphere(
					(body.precisePosition * scaleMultiplier).ToVector3(),
					body.radius * SOIScaler * scaleMultiplier
				);
		}
		if(orbitPoints != null) {
			Gizmos.color = Color.white;
			foreach(OrbitPoint point in orbitPoints)
				Gizmos.DrawWireSphere(point.position.ToVector3(), 100);
		}
	}
}
