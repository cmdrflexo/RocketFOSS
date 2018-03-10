using UnityEngine;

namespace RFOSSCore.Settings {
	public class Controls : MonoBehaviour {
	
		// EVA Translate
		public readonly KeyCode evaForward    = KeyCode.W;
		public readonly KeyCode evaBack       = KeyCode.S;
		public readonly KeyCode evaLeft       = KeyCode.A;
		public readonly KeyCode evaRight      = KeyCode.D;
		public readonly KeyCode evaUp         = KeyCode.Space;
		public readonly KeyCode evaDown       = KeyCode.LeftShift;
		// EVA Rotate
		public readonly KeyCode evaRotateXPos = KeyCode.I;
		public readonly KeyCode evaRotateXNeg = KeyCode.K;
		public readonly KeyCode evaRotateYPos = KeyCode.L;
		public readonly KeyCode evaRotateYNeg = KeyCode.J;
		public readonly KeyCode evaRotateZPos = KeyCode.U;
		public readonly KeyCode evaRotateZNeg = KeyCode.O;
		// EVA Interact
		public readonly KeyCode evaGrab       = KeyCode.G;

		public bool ControlInput(string control) {
			switch(control) {
				case "evaForward":
					return Input.GetKey(evaForward);
				case "evaBack":
					return Input.GetKey(evaBack);
				case "evaLeft":
					return Input.GetKey(evaLeft);
				case "evaRight":
					return Input.GetKey(evaRight);
				case "evaUp":
					return Input.GetKey(evaUp);
				case "evaDown":
					return Input.GetKey(evaDown);
				case "evaRotateXPos":
					return Input.GetKey(evaRotateXPos);
				case "evaRotateXNeg":
					return Input.GetKey(evaRotateXNeg);
				case "evaRotateYPos":
					return Input.GetKey(evaRotateYPos);
				case "evaRotateYNeg":
					return Input.GetKey(evaRotateYNeg);
				case "evaRotateZPos":
					return Input.GetKey(evaRotateZPos);
				case "evaRotateZNeg":
					return Input.GetKey(evaRotateZNeg);
				case "evaGrab":
					return Input.GetKey(evaGrab);
			}
			return false;
		}

	}
}
