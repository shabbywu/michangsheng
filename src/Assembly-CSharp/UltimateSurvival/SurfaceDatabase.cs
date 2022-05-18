using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008FE RID: 2302
	public class SurfaceDatabase : ScriptableSingleton<SurfaceDatabase>
	{
		// Token: 0x06003AF9 RID: 15097 RVA: 0x001AAD58 File Offset: 0x001A8F58
		public SurfaceData GetSurfaceData(Texture texture)
		{
			for (int i = 0; i < this.m_Surfaces.Length; i++)
			{
				if (this.m_Surfaces[i].HasTexture(texture))
				{
					return this.m_Surfaces[i];
				}
			}
			return this.m_DefaultSurface;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x001AAD98 File Offset: 0x001A8F98
		public SurfaceData GetSurfaceData(Ray ray, float maxDistance, LayerMask mask)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, ref raycastHit, maxDistance, mask, 1))
			{
				return this.GetSurfaceData(raycastHit.collider, raycastHit.point, raycastHit.triangleIndex);
			}
			return null;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x0002AC7A File Offset: 0x00028E7A
		public SurfaceData GetSurfaceData(RaycastHit hitInfo)
		{
			return this.GetSurfaceData(hitInfo.collider, hitInfo.point, hitInfo.triangleIndex);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x001AADD4 File Offset: 0x001A8FD4
		public SurfaceData GetSurfaceData(Collider collider, Vector3 position, int triangleIndex)
		{
			Texture texture;
			if (collider.GetType() == typeof(TerrainCollider))
			{
				Terrain component = collider.GetComponent<Terrain>();
				TerrainData terrainData = component.terrainData;
				float[] terrainTextureMix = this.GetTerrainTextureMix(position, terrainData, component.GetPosition());
				int terrainTextureIndex = this.GetTerrainTextureIndex(position, terrainTextureMix);
				texture = terrainData.splatPrototypes[terrainTextureIndex].texture;
			}
			else
			{
				texture = this.GetMeshTexture(collider, triangleIndex);
			}
			if (texture)
			{
				for (int i = 0; i < this.m_Surfaces.Length; i++)
				{
					if (this.m_Surfaces[i].HasTexture(texture))
					{
						return this.m_Surfaces[i];
					}
				}
				return this.m_DefaultSurface;
			}
			return this.m_DefaultSurface;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x001AAE84 File Offset: 0x001A9084
		private Texture GetMeshTexture(Collider collider, int triangleIndex)
		{
			SurfaceIdentity component = collider.GetComponent<SurfaceIdentity>();
			if (component)
			{
				return component.Texture;
			}
			Renderer component2 = collider.GetComponent<Renderer>();
			MeshCollider meshCollider = collider as MeshCollider;
			if (!component2 || !component2.sharedMaterial || !component2.sharedMaterial.mainTexture)
			{
				return null;
			}
			if (!meshCollider || meshCollider.convex)
			{
				return component2.material.mainTexture;
			}
			Mesh sharedMesh = meshCollider.sharedMesh;
			int num = -1;
			int num2 = sharedMesh.triangles[triangleIndex * 3];
			int num3 = sharedMesh.triangles[triangleIndex * 3 + 1];
			int num4 = sharedMesh.triangles[triangleIndex * 3 + 2];
			for (int i = 0; i < sharedMesh.subMeshCount; i++)
			{
				int[] triangles = sharedMesh.GetTriangles(i);
				for (int j = 0; j < triangles.Length; j += 3)
				{
					if (triangles[j] == num2 && triangles[j + 1] == num3 && triangles[j + 2] == num4)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					break;
				}
			}
			return component2.materials[num].mainTexture;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x001AAF98 File Offset: 0x001A9198
		private float[] GetTerrainTextureMix(Vector3 worldPos, TerrainData terrainData, Vector3 terrainPos)
		{
			int num = (int)((worldPos.x - terrainPos.x) / terrainData.size.x * (float)terrainData.alphamapWidth);
			int num2 = (int)((worldPos.z - terrainPos.z) / terrainData.size.z * (float)terrainData.alphamapHeight);
			float[,,] alphamaps = terrainData.GetAlphamaps(num, num2, 1, 1);
			float[] array = new float[alphamaps.GetUpperBound(2) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = alphamaps[0, 0, i];
			}
			return array;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x001AB028 File Offset: 0x001A9228
		private int GetTerrainTextureIndex(Vector3 worldPos, float[] textureMix)
		{
			float num = 0f;
			int result = 0;
			for (int i = 0; i < textureMix.Length; i++)
			{
				if (textureMix[i] > num)
				{
					result = i;
					num = textureMix[i];
				}
			}
			return result;
		}

		// Token: 0x0400354A RID: 13642
		[SerializeField]
		private SurfaceData m_DefaultSurface;

		// Token: 0x0400354B RID: 13643
		[SerializeField]
		private SurfaceData[] m_Surfaces;
	}
}
