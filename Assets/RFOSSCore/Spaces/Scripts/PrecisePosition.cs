using UnityEngine;

namespace RFOSSCore {
	[System.Serializable]
	public class PrecisePosition {

		public double x, y, z;
		
		public PrecisePosition(double x = 0, double y = 0, double z = 0) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3 ToVector3() {
			return new Vector3(
				(float)x,
				(float)y,
				(float)z
			);
		}

		public PrecisePosition FromVector3(Vector3 vector3) {
			x = vector3.x;
			y = vector3.y;
			z = vector3.z;
			return this;
		}
		
		public static double Distance(PrecisePosition a, PrecisePosition b) {
			return Mathd.Sqrt(
				Mathd.Pow(a.x - b.x, 2) +
				Mathd.Pow(a.y - b.y, 2) +
				Mathd.Pow(a.z - b.z, 2)
			);
		}

		public static PrecisePosition operator +(PrecisePosition a, PrecisePosition b) {
			return new PrecisePosition(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static PrecisePosition operator -(PrecisePosition a, PrecisePosition b) {
			return new PrecisePosition(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static PrecisePosition operator *(PrecisePosition a, PrecisePosition b) {
			return new PrecisePosition(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static PrecisePosition operator *(PrecisePosition p, double d) {
			return new PrecisePosition(p.x * d, p.y * d, p.z * d);
		}

		public static PrecisePosition operator *(PrecisePosition p, float f) {
			return new PrecisePosition(p.x * f, p.y * f, p.z * f);
		}

		public static PrecisePosition operator /(PrecisePosition a, PrecisePosition b) {
			return new PrecisePosition(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static PrecisePosition operator /(PrecisePosition a, float f) {
			return new PrecisePosition(a.x / f, a.y / f, a.z / f);
		}

		public static PrecisePosition operator /(PrecisePosition p, double d) {
			return new PrecisePosition(p.x / d, p.y / d, p.z / d);
		}
	}
}
