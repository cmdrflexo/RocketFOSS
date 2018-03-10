using UnityEngine;

namespace RFOSSCore {
	public class PlayerInfo : MonoBehaviour {

		public PrecisePosition worldPosition;
		public PlayerControllable controlling;

		// ///
		// TESTING SWITCHING BETWEEN CONTROLLABLES
		// SHOW BE USED IN GAME/SCENE MANAGER
		//public PlayerControllable[] evaCharacters;
		//int c = 0;
		//private void Update() {
		//	foreach(PlayerControllable controllable in evaCharacters) {
		//		controllable.isControlled = false;
		//	}
		//	if(Input.GetKeyDown(KeyCode.RightBracket) && c < evaCharacters.Length - 1)
		//		c++;
		//	if(Input.GetKeyDown(KeyCode.LeftBracket) && c > 0)
		//		c--;
			
		//	controlling = evaCharacters[c];
		//	evaCharacters[c].isControlled = true;
			
		//}
		// ///
	}
}
