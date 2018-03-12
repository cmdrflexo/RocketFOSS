using UnityEngine;

namespace RFOSSCore.PlanetGen {
	public class PlanetManager : MonoBehaviour {

		BodyInfo planetInfo = new BodyInfo();
		PlanetGenerator gen = new PlanetGenerator();
		PlanetSide[] sides = new PlanetSide[6];

		public GameObject cam;

		public float[] LODThresholds = new float[7];

		public Material defaultMaterial;

		private void Start() {
			CreatePlanet();
			
			//GetHeightDataTEST();
		}

		private void GetHeightDataTEST() {
			float[,] heightData = GetHeightData();
			int heightDataSize = 1000;
			int sideSize = 10;
			int vSize = 8;
			Vector2 faceCoordinates = new Vector2(6, 8);
			int facePatchSize = heightDataSize / sideSize / vSize;

			float scaling = sideSize / vSize;
			Vector2 offset = faceCoordinates * scaling;

			float[,] faceHeightData = new float[vSize, vSize];
			
			string printOut = string.Empty;
			for(int y = 0; y < vSize; y++) {
				string line = string.Empty;
				for(int x = 0; x < vSize; x++) {
					
					//print("offset: " + offset);
					//print(
					//	"x * (int)offset.x: "   + (facePatchSize * x + (int)offset.x * x) +
					//	", y * (int)offset.y: " + (facePatchSize * y + (int)offset.y * y)
					//);

					faceHeightData[x, y] = heightData[
						facePatchSize * x + (int)offset.x * x,
						facePatchSize * y + (int)offset.y * y
					];

					//line += "[" + faceHeightData[x, y] + "]";

				}
				//printOut = line + "\n" + printOut;
			}
			//print(printOut);
		}

		private void Update() {
			foreach(PlanetSide side in sides)
				foreach(PlanetFace face in side.faces) {
					//face.LOD = 4;
					float distance = Vector3.Distance(cam.transform.position, face.centerPoint);
					face.LOD = 0;
					for(int i = 0; i < LODThresholds.Length; i++)
						if(distance < LODThresholds[i])
							face.LOD = i + 1;
				}
		}

		private void CreatePlanet() {
			planetInfo.name = "Planet";
			planetInfo.sideResolution = 10;
			planetInfo.radius = 100;
			planetInfo.gameObject = new GameObject(planetInfo.name);
			planetInfo.defaultMaterial = defaultMaterial;

			float[,] heightData = GetHeightData();

			//sides = new PlanetSide[1];
			sides[0] = new PlanetSide(this, planetInfo, new Vector3(  0,    0,  0), heightData);
			sides[1] = new PlanetSide(this, planetInfo, new Vector3(0, 90, 0), heightData);
			sides[2] = new PlanetSide(this, planetInfo, new Vector3(0, 180, 0), heightData);
			sides[3] = new PlanetSide(this, planetInfo, new Vector3(0, -90, 0), heightData);
			sides[4] = new PlanetSide(this, planetInfo, new Vector3(90, 0, 1), heightData);
			sides[5] = new PlanetSide(this, planetInfo, new Vector3(-90, 0, -1), heightData);
		}

		public Texture2D heightmap;
		private float[,] GetHeightData() {
			float[,] heightData = new float[heightmap.width, heightmap.width];
			//print("heightmap.width: " + heightmap.width);
			//print("(int)Mathf.Sqrt(heightData.GetLength(0): " + heightData.GetLength(0));
			for(int y = 0; y <= heightmap.width - 1; y++)
				for(int x = 0; x <= heightmap.width - 1; x++)
					heightData[x, y] = heightmap.GetPixel(x, y).grayscale;
			return heightData;
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.green;
			if(sides[0] != null)
				foreach(Vector3 sideVertex in sides[0].vertices)
					Gizmos.DrawWireSphere(sideVertex, 0.5f);
			//if(sides[0].faces[0, 0] != null) {
			//	PlanetFace face = sides[0].faces[0, 0];
			//	int vSize = (int)Mathf.Pow(2, face.LOD + 1) + 1;
			//	for(int y = 0, i = 0; y < vSize; y++)
			//		for(int x = 0; x < vSize; x++, i++) {
			//			face
			//		}
			//}
		}
	}
}
