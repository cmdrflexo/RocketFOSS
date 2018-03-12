using UnityEngine;

namespace RFOSSCore {
	[RequireComponent(typeof(WorldSpaceObject))]
	public class PlayerControllable : MonoBehaviour {

		public bool isControlled;
		public bool canTranslate, canRotate;

		[HideInInspector]
		public PlayerInfo player;
		WorldSpaceObject worldSpaceObject;

		private void Start() {
			player = FindObjectOfType<PlayerInfo>();
			worldSpaceObject = GetComponent<WorldSpaceObject>();
		}

		private void Update() {
			//if(isControlled)
			//	player.worldPosition = worldSpaceObject.precisePosition;
		}

	}
}