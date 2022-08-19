using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000618 RID: 1560
	public class SurfaceDatabase : ScriptableSingleton<SurfaceDatabase>
	{
		// Token: 0x060031CB RID: 12747 RVA: 0x00161418 File Offset: 0x0015F618
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

		// Token: 0x060031CC RID: 12748 RVA: 0x00161458 File Offset: 0x0015F658
		public SurfaceData GetSurfaceData(Ray ray, float maxDistance, LayerMask mask)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, ref raycastHit, maxDistance, mask, 1))
			{
				return this.GetSurfaceData(raycastHit.collider, raycastHit.point, raycastHit.triangleIndex);
			}
			return null;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00161494 File Offset: 0x0015F694
		public SurfaceData GetSurfaceData(RaycastHit hitInfo)
		{
			return this.GetSurfaceData(hitInfo.collider, hitInfo.point, hitInfo.triangleIndex);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x001614B4 File Offset: 0x0015F6B4
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

		// Token: 0x060031CF RID: 12751 RVA: 0x00161564 File Offset: 0x0015F764
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

		// Token: 0x060031D0 RID: 12752 RVA: 0x00161678 File Offset: 0x0015F878
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

		// Token: 0x060031D1 RID: 12753 RVA: 0x00161708 File Offset: 0x0015F908
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

		// Token: 0x04002C29 RID: 11305
		[SerializeField]
		private SurfaceData m_DefaultSurface;

		// Token: 0x04002C2A RID: 11306
		[SerializeField]
		private SurfaceData[] m_Surfaces;
	}
}
