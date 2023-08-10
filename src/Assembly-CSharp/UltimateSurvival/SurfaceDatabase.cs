using UnityEngine;

namespace UltimateSurvival;

public class SurfaceDatabase : ScriptableSingleton<SurfaceDatabase>
{
	[SerializeField]
	private SurfaceData m_DefaultSurface;

	[SerializeField]
	private SurfaceData[] m_Surfaces;

	public SurfaceData GetSurfaceData(Texture texture)
	{
		for (int i = 0; i < m_Surfaces.Length; i++)
		{
			if (m_Surfaces[i].HasTexture(texture))
			{
				return m_Surfaces[i];
			}
		}
		return m_DefaultSurface;
	}

	public SurfaceData GetSurfaceData(Ray ray, float maxDistance, LayerMask mask)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(ray, ref val, maxDistance, LayerMask.op_Implicit(mask), (QueryTriggerInteraction)1))
		{
			return GetSurfaceData(((RaycastHit)(ref val)).collider, ((RaycastHit)(ref val)).point, ((RaycastHit)(ref val)).triangleIndex);
		}
		return null;
	}

	public SurfaceData GetSurfaceData(RaycastHit hitInfo)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return GetSurfaceData(((RaycastHit)(ref hitInfo)).collider, ((RaycastHit)(ref hitInfo)).point, ((RaycastHit)(ref hitInfo)).triangleIndex);
	}

	public SurfaceData GetSurfaceData(Collider collider, Vector3 position, int triangleIndex)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Texture val = null;
		if (((object)collider).GetType() == typeof(TerrainCollider))
		{
			Terrain component = ((Component)collider).GetComponent<Terrain>();
			TerrainData terrainData = component.terrainData;
			float[] terrainTextureMix = GetTerrainTextureMix(position, terrainData, component.GetPosition());
			int terrainTextureIndex = GetTerrainTextureIndex(position, terrainTextureMix);
			val = (Texture)(object)terrainData.splatPrototypes[terrainTextureIndex].texture;
		}
		else
		{
			val = GetMeshTexture(collider, triangleIndex);
		}
		if (Object.op_Implicit((Object)(object)val))
		{
			for (int i = 0; i < m_Surfaces.Length; i++)
			{
				if (m_Surfaces[i].HasTexture(val))
				{
					return m_Surfaces[i];
				}
			}
			return m_DefaultSurface;
		}
		return m_DefaultSurface;
	}

	private Texture GetMeshTexture(Collider collider, int triangleIndex)
	{
		SurfaceIdentity component = ((Component)collider).GetComponent<SurfaceIdentity>();
		if (Object.op_Implicit((Object)(object)component))
		{
			return component.Texture;
		}
		Renderer component2 = ((Component)collider).GetComponent<Renderer>();
		MeshCollider val = (MeshCollider)(object)((collider is MeshCollider) ? collider : null);
		if (!Object.op_Implicit((Object)(object)component2) || !Object.op_Implicit((Object)(object)component2.sharedMaterial) || !Object.op_Implicit((Object)(object)component2.sharedMaterial.mainTexture))
		{
			return null;
		}
		if (!Object.op_Implicit((Object)(object)val) || val.convex)
		{
			return component2.material.mainTexture;
		}
		Mesh sharedMesh = val.sharedMesh;
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

	private float[] GetTerrainTextureMix(Vector3 worldPos, TerrainData terrainData, Vector3 terrainPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
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
}
