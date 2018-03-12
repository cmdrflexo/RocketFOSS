using System.Collections.Generic;
using UnityEngine;

namespace RFOSSCore.PlanetGen {
	public class MeshUtility {

		MeshResources meshResources = new MeshResources();
		EdgeSolutions edge = new EdgeSolutions();
		
		public Mesh GetFaceTriangles(Vector3[] vertices, PlanetFace face) {
			Mesh mesh = new Mesh();
			int[] triangles = meshResources.gridTriangles[face.LOD];
			int vSize = (int)Mathf.Sqrt(vertices.Length);
			if(face.LOD > 0) {
				List<int> triangleList = new List<int>(triangles);
				int[] edgeTriangles = edge.GetEdges(face);
				foreach(int t in edgeTriangles)
					triangleList.Add(t);
				triangles = triangleList.ToArray();
			}
			Vector2[] uvs = new Vector2[vertices.Length];

			for(int y = 0, i = 0; y < vSize; y++)
				for(int x = 0; x < vSize; x++, i++)
					uvs[i] = new Vector2((float)x / (vSize - 1), (float)y / (vSize - 1));
			
			//int heightDataSize = face.side.heightData.GetLength(0);
			//int sideSize = face.side.faces.GetLength(0);
			//int facePatchSize = heightDataSize / sideSize;
			//for(int y = 0, i = 0; y < vSize; y++)
			//	for(int x = 0; x < vSize; x++, i++)
			//		// (int)((facePatchSize * coordinates.y) + (y * (facePatchSize / (vSize + 1f))))
			//		uvs[i] = new Vector2(
			//			facePatchSize * face.coordinates.x + x,
			//			facePatchSize * face.coordinates.y + y
			//		);



			mesh.vertices  = vertices;
			mesh.triangles = triangles;
			mesh.uv = uvs;
			//mesh.tangents
			return mesh;
		}
		
		public Vector3[] SpherizeSide(Vector3[] vertices, PlanetSide side) {
			int vSize = (int)Mathf.Sqrt(vertices.Length);
			for(int y = 0, i = 0; y < vSize; y++)
				for(int x = 0; x < vSize; x++, i++) {
					Vector3 v = new Vector3(x, y, 0) * 2f / (vSize - 1f) - Vector3.one;
					float x2 = v.x * v.x;
					float y2 = v.y * v.y;
					float z2 = v.z * v.z;
					Vector3 n = new Vector3(
						v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f),
						v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f),
						v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f)
					);
					vertices[i] = n * side.planet.radius;
				}
			return vertices;
		}

		public Vector3[] SpherizeFace(Vector3[] vertices, float radius) {
			int vSize = (int)Mathf.Sqrt(vertices.Length);
			for(int y = 0, i = 0; y < vSize; y++)
				for(int x = 0; x < vSize; x++, i++) {
					Vector3 n = (vertices[i] - Vector3.zero).normalized;
					vertices[i] = n * radius;
				}

			return vertices;
		}

		public Vector3[] RotateVertices(Vector3[] vertices, PlanetSide side) {
			for(int v = 0; v < vertices.Length; v++) {
				Quaternion rotation = Quaternion.AngleAxis(
					(int)side.sideAxis.magnitude, side.sideAxis.normalized
				);
				Vector3 vector2 = vertices[v] - Vector3.zero;
				vector2 = rotation * vector2;
				vertices[v] = Vector3.zero + vector2;
			}
			return vertices;
		}

		public Vector3[] AddHeightData(Vector3[] vertices, float[,] heightData, float scale) {
			int vSize = (int)Mathf.Sqrt(vertices.Length);
			for(int y = 0, i = 0; y < vSize; y++)
				for(int x = 0; x < vSize; x++, i++) {
					Vector3 n = (vertices[i] - Vector3.zero).normalized;
					vertices[i] += n * (1 + heightData[x, y] * scale);
				}
			return vertices;
		}

		public int[] GridSolver(int size) {
			int[] triangles = new int[size * size * 6];
			for(int ti = 0, vi = 0, y = 0; y < size; y++, vi++)
				for(int x = 0; x < size; x++, ti += 6, vi++) {
					if(y < size - 1 && y > 0 && x < size - 1 && x > 0) {
						triangles[ti] = vi;
						triangles[ti + 2] = triangles[ti + 3] = vi + 1;
						triangles[ti + 1] = triangles[ti + 4] = vi + size + 1;
						triangles[ti + 5] = vi + size + 2;
					}
				}
			return triangles;
		}

	}

}
