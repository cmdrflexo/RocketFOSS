using System;
using UnityEngine;

namespace RFOSSCore.PlanetGen {
	public class PlanetFace : MonoBehaviour {

		public PlanetSide side;
		public Vector2Int coordinates;
		public Vector3[] corners;
		public Vector3 centerPoint;
		public Vector3[] vertices;
		public int LOD;
		int previousLOD;

		public MeshRenderer meshRenderer;
		public MeshFilter meshFilter;
		public MeshCollider meshCollider;
		
		public Mesh[] meshes = new Mesh[9];
		MeshUtility meshUtility = new MeshUtility();
		
		private void Start() {
			meshes = new Mesh[8];
			meshFilter = gameObject.AddComponent<MeshFilter>();
			meshRenderer = gameObject.AddComponent<MeshRenderer>();
			meshCollider = gameObject.AddComponent<MeshCollider>();
			CreateVertices();
			UpdateMesh(true);
			//CreateAllMeshes();
		}

		private void Update() {
			if(LOD != previousLOD) { // add check for neighbor LOD changes to trigger update
				previousLOD = LOD;
				UpdateMesh(true); // initialize true, until edge fans can get updated
			}
		}

		private void CreateVertices() {
			int vSize = (int)Mathf.Pow(2, LOD + 1) + 1;
			vertices = new Vector3[vSize * vSize];
			for(int y = 0, i = 0; y < vSize; y++)
				for(int x = 0; x < vSize; x++, i++) {
					float xProgress = x / (vSize - 1f);
					float yProgress = y / (vSize - 1f);
					vertices[i] = Vector3.Lerp(
						Vector3.Lerp(corners[0], corners[1], yProgress),
						Vector3.Lerp(corners[3], corners[2], yProgress),
						xProgress
					);
				}
			vertices = side.meshUtility.SpherizeFace(vertices, side.planet.radius);
			centerPoint = vertices[vertices.Length / 2];
			vertices = side.meshUtility.AddHeightData(vertices, GetHeightData(), 1);
		}

		private float[,] GetHeightData() {
			int heightDataSize = side.heightData.GetLength(0);
			int sideSize = side.faces.GetLength(0);
			int vSize = (int)Mathf.Pow(2, LOD + 1) + 1;
			int facePatchSize = heightDataSize / sideSize;

			float[,] faceHeightData = new float[vSize, vSize];

			for(int y = 0; y < vSize; y++) {
				for(int x = 0; x < vSize; x++) {
					faceHeightData[x, y] = side.heightData[
						(int)((facePatchSize * coordinates.x) + (x * (facePatchSize / (vSize + 1f)))),
						(int)((facePatchSize * coordinates.y) + (y * (facePatchSize / (vSize + 1f))))
					];
				}
			}
			return faceHeightData;
		}

		private void CreateAllMeshes() {
			for(LOD = 0; LOD < meshes.Length; LOD++) {
				CreateVertices();
				meshes[LOD] = meshUtility.GetFaceTriangles(vertices, this);
				meshes[LOD].RecalculateNormals();
			}
			LOD = 0;
			meshFilter.mesh = meshes[0];
			meshRenderer.material = side.planet.defaultMaterial;
			//meshRenderer.material.color = new Color(LOD * 0.2f, 0, 1 - LOD * 0.2f, 1);
		}

		private void UpdateMesh(bool initialize = false) {
			if(initialize || meshes[LOD] == null) {
				CreateVertices();
				meshes[LOD] = meshUtility.GetFaceTriangles(vertices, this);
				meshes[LOD].RecalculateNormals();
				meshFilter.mesh = meshes[LOD];
				meshRenderer.material = side.planet.defaultMaterial;
				//meshRenderer.material.color = new Color(LOD * 0.2f, 0, 1 - LOD * 0.2f, 1);
			} else {
				meshFilter.mesh = meshes[LOD];
			}
			CreateCollider();
		}

		public bool disableColliders = true;
		private void CreateCollider() {
			if(!disableColliders && LOD >= 6) {
				meshCollider.enabled = true;
				meshCollider.sharedMesh = meshes[LOD];
			} else {
				meshCollider.enabled = false;
			}
		}
	}
}
