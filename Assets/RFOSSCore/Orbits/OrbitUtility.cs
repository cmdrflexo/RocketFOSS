using UnityEngine;
using RFOSSCore.Constants;
using System.Collections.Generic;

namespace RFOSSCore.Orbits {
	public class OrbitUtility {
		
		double g = PhysicsConstants.g;

		public OrbitPoint[] GenerateOrbit(BodyInfo body) {
			List<OrbitPoint> orbitPoints = new List<OrbitPoint>();
			OrbitalElements orbit = body.orbit;
			double semiMinorAxis = orbit.SemiMinorAxis();
			//double moveOffset = (orbit.apoapsis + orbit.periapsis) / 2 - orbit.periapsis;
			float timer = Mathf.PI * 2;
			for(float t = 0; t < timer; t++) {
				double posX = orbit.semiMajorAxis * Mathd.Cos(timer)/* - moveOffset*/;
				double posZ = semiMinorAxis * Mathd.Sin(timer);
				double posY = 0; // ?
				double nPosX = posX * Mathd.Cos(orbit.inclination) - posY * Mathd.Sin(orbit.inclination);
				double nPosY = posY * Mathd.Cos(orbit.inclination) + posX * Mathd.Sin(orbit.inclination);
				double nPosX2 = nPosX * Mathd.Cos(orbit.inclination) - posZ  * Mathd.Sin(orbit.inclination);
				double nPosZ  = posZ  * Mathd.Cos(orbit.inclination) + nPosX * Mathd.Sin(orbit.inclination);
				PrecisePosition pointPosition = new PrecisePosition(nPosX, nPosY, nPosZ);
				double pointVelocity = Mathd.Sqrt(
					Mathd.Abs((g * body.mass) * ((2 / body.radius) - (1 / orbit.semiMajorAxis)))
				);
				orbitPoints.Add(new OrbitPoint(pointPosition, pointVelocity));
			}
			return orbitPoints.ToArray();
		}
	}

	public struct OrbitPoint {
		public PrecisePosition position;
		public double velocity;
		public OrbitPoint(PrecisePosition position, double velocity) {
			this.position = position;
			this.velocity = velocity;
		}
	}
}
