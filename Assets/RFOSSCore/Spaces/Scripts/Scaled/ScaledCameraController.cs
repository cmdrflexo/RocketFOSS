using UnityEngine;

namespace RFOSSCore {
	public class ScaledCameraController : MonoBehaviour {

		public GameObject regularCamera;
		PlayerInfo player;

		private void Start() {
			player = FindObjectOfType<PlayerInfo>();
		}

		private void LateUpdate() {
			transform.rotation = regularCamera.transform.rotation;
			transform.position = player.worldPosition.ToVector3() / 1000f;
		}

	}
}
