using UnityEngine;
using RFOSSCore.Settings;

namespace RFOSSCore {
	[RequireComponent(typeof(PlayerControllable), typeof(WorldSpaceObject))]
	public class EVACharacter : MonoBehaviour {

		PlayerControllable controllable;
		Controls controls = new Controls();
		WorldSpaceObject worldSpaceObject;
		
		public float moveSpeed = 10;
		public float rotateSpeed = 20;

		Vector3 velocity = new Vector3();


		private void Start() {
			controllable = GetComponent<PlayerControllable>();
			worldSpaceObject = GetComponent<WorldSpaceObject>();
			worldSpaceObject.controllable = controllable;
		}

		private void Update() {
			if(controllable.isControlled)
				GetInput();
			UpdateVelocity();

			if(!controllable.isControlled)
				print(velocity);
		}

		private void GetInput() {
			if(controls.ControlInput("evaForward"))
				velocity += transform.forward * moveSpeed * Time.deltaTime;
			if(controls.ControlInput("evaBack"))
				velocity -= transform.forward * moveSpeed * Time.deltaTime;
			if(controls.ControlInput("evaLeft"))
				velocity -= transform.right * moveSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRight"))
				velocity += transform.right * moveSpeed * Time.deltaTime;
			if(controls.ControlInput("evaUp"))
				velocity += transform.up * moveSpeed * Time.deltaTime;
			if(controls.ControlInput("evaDown"))
				velocity -= transform.up * moveSpeed * Time.deltaTime;

			if(controls.ControlInput("evaRotateXPos"))
				transform.localEulerAngles += Vector3.right * rotateSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRotateXNeg"))
				transform.localEulerAngles -= Vector3.right * rotateSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRotateYPos"))
				transform.localEulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRotateYNeg"))
				transform.localEulerAngles -= Vector3.up * rotateSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRotateZPos"))
				transform.localEulerAngles += Vector3.forward * rotateSpeed * Time.deltaTime;
			if(controls.ControlInput("evaRotateZNeg"))
				transform.localEulerAngles -= Vector3.forward * rotateSpeed * Time.deltaTime;
		}

		private void UpdateVelocity() {
			worldSpaceObject.precisePosition.x += velocity.x;
			worldSpaceObject.precisePosition.y += velocity.y;
			worldSpaceObject.precisePosition.z += velocity.z;
		}

		//bool canAttach = false;
		//private void OnCollisionEnter(Collision collision) {
		//	if(collision.collider == hatch) {
		//		canAttach = true;
		//	}
		//}

	}
}