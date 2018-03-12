using RFOSSCore.Orbits;
using UnityEngine;

namespace RFOSSCore {
	[System.Serializable]
	public struct BodyInfo {
		// Scene Object
		public GameObject gameObject;
		public Material defaultMaterial;
		public string name;
		// PlanetGen
		public int sideResolution;
		// Orbit
		public OrbitalElements orbit;
		public PrecisePosition precisePosition;
		// Properties
		public float mass;
		public float radius;
	}
}