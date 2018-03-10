using UnityEngine;

namespace RFOSSCore {
	public class WorldSpaceObject : MonoBehaviour {

		PlayerInfo player;

		public PrecisePosition precisePosition;
		public bool inLocalSpace;

		[HideInInspector]
		public PlayerControllable controllable;

		private void Start() {
			player = FindObjectOfType<PlayerInfo>();
			precisePosition.FromVector3(transform.position);
		}

		private void Update() {
			if(controllable == null) {
				if(inLocalSpace)
					transform.position = (precisePosition - player.worldPosition).ToVector3();
			} else {
				if(controllable.isControlled) {
					//precisePosition = controllable.player.worldPosition;
					//print(controllable.gameObject.name + " is controlled");
				} else {
					if(inLocalSpace) {
						//print(controllable.gameObject.name + " not controlled");
						transform.position = (precisePosition - player.worldPosition).ToVector3();
					}
				}
			}
		}
		
	}
}
