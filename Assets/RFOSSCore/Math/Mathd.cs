using UnityEngine;

namespace RFOSSCore {
	public class Mathd {

		public static double Abs(double d) {
			return Mathf.Abs((float)d);
		}

		public static double Sqrt(double d) {
			return Mathf.Sqrt((float)d);
		}

		public static double Pow(double d, double p) {
			return Mathf.Pow((float)d, (float)p);
		}

		public static double Sin(double d) {
			return Mathf.Sin((float)d);
		}

		public static double Cos(double d) {
			return Mathf.Cos((float)d);
		}

		public static double Tan(double d) {
			return Mathf.Tan((float)d);
		}
	}
}
