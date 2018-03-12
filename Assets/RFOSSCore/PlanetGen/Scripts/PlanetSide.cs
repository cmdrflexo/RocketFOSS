using UnityEngine;

namespace RFOSSCore.PlanetGen {
	public class PlanetSide {
		PlanetManager manager;
		public BodyInfo planet;
		public Vector3[] vertices;
		public PlanetFace[,] faces;
		public Vector3 sideAxis;
		public MeshUtility meshUtility = new MeshUtility();
		public float[,] heightData;

		string[] sideAxisName = new string[] { "xPos", "xNeg", "yPos", "yNeg", "zPos", "zNeg" };

		public PlanetSide(PlanetManager planetManager, BodyInfo planetInfo, Vector3 sideAxis, float[,] heightData) {
			manager = planetManager;
			planet = planetInfo;
			this.sideAxis = sideAxis;
			this.heightData = heightData;
			CreateVertices();
			CreateFaces();
		}

		private void CreateVertices() {
			vertices = new Vector3[(planet.sideResolution + 1) * (planet.sideResolution + 1)];
			vertices = meshUtility.SpherizeSide(vertices, this);
			vertices = meshUtility.AddHeightData(vertices, heightData, 1);
			vertices = meshUtility.RotateVertices(vertices, this);
		}

		public void CreateFaces() {
			faces = new PlanetFace[planet.sideResolution, planet.sideResolution];
			for(int y = 0; y < planet.sideResolution; y++) {
				for(int x = 0; x < planet.sideResolution; x++) {
					GameObject faceGameObject = new GameObject(sideAxis + " Face[" + x + ", " + y + "]");
					faceGameObject.transform.parent = planet.gameObject.transform;
					faces[x, y] = faceGameObject.AddComponent<PlanetFace>();
					faces[x, y].side = this;
					faces[x, y].coordinates = new Vector2Int(x, y);
					faces[x, y].corners = new Vector3[] {
						vertices[x + y * (planet.sideResolution + 1)],
						vertices[x + y * (planet.sideResolution + 1) + planet.sideResolution + 1],
						vertices[x + y * (planet.sideResolution + 1) + planet.sideResolution + 2],
						vertices[x + y * (planet.sideResolution + 1) + 1]
					};
					faces[x, y].LOD = 0;
				}
			}
		}
	}
}
