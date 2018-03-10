using UnityEngine;

namespace RFOSSCore {
	[RequireComponent(typeof(PlayerInfo))]
	public class PlayerCameraController : MonoBehaviour {
		PlayerInfo player;
		public GameObject focus;

		private void Start() {
			player = GetComponent<PlayerInfo>();
		}

		private void Update() {
			//focus.transform.parent = player.controlling.transform;
			//focus.transform.localPosition = Vector3.zero;
		}
	}
}