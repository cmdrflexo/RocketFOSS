namespace RFOSSCore.Orbits {
	[System.Serializable]
	public struct OrbitalElements {
		public double semiMajorAxis;
		public double eccentricity;
		public double inclination;
		public double longitudeOfAscendingNode;
		public double argumentOfPeriapsis;
		public double trueAnomaly;
		public double apoapsis;
		public double periapsis;
		public OrbitalElements(
			double semiMajorAxis,
			double eccentricity,
			double inclination,
			double longitudeOfAscendingNode,
			double argumentOfPeriapsis,
			double trueAnomaly,
			double apoapsis,
			double periapsis
		) {
			this.semiMajorAxis            = semiMajorAxis;
			this.eccentricity             = eccentricity;
			this.inclination              = inclination;
			this.longitudeOfAscendingNode = longitudeOfAscendingNode;
			this.argumentOfPeriapsis      = argumentOfPeriapsis;
			this.trueAnomaly              = trueAnomaly;
			this.apoapsis                 = apoapsis;
			this.periapsis                = periapsis;
		}
		public double SemiMinorAxis() {
			return Mathd.Sqrt(apoapsis * periapsis);
		}
	}
}
