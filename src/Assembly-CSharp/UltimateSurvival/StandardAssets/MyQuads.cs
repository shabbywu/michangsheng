using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000630 RID: 1584
	internal class MyQuads
	{
		// Token: 0x06003242 RID: 12866 RVA: 0x0016502C File Offset: 0x0016322C
		private static bool HasMeshes()
		{
			if (MyQuads.meshes == null)
			{
				return false;
			}
			foreach (Mesh mesh in MyQuads.meshes)
			{
				if (null == mesh)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x00165068 File Offset: 0x00163268
		public static void Cleanup()
		{
			if (MyQuads.meshes == null)
			{
				return;
			}
			for (int i = 0; i < MyQuads.meshes.Length; i++)
			{
				if (null != MyQuads.meshes[i])
				{
					Object.DestroyImmediate(MyQuads.meshes[i]);
					MyQuads.meshes[i] = null;
				}
			}
			MyQuads.meshes = null;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x001650B8 File Offset: 0x001632B8
		public static Mesh[] GetMeshes(int totalWidth, int totalHeight)
		{
			if (MyQuads.HasMeshes() && MyQuads.currentQuads == totalWidth * totalHeight)
			{
				return MyQuads.meshes;
			}
			int num = 10833;
			int num2 = totalWidth * totalHeight;
			MyQuads.currentQuads = num2;
			MyQuads.meshes = new Mesh[Mathf.CeilToInt(1f * (float)num2 / (1f * (float)num))];
			int num3 = 0;
			for (int i = 0; i < num2; i += num)
			{
				int triCount = Mathf.FloorToInt((float)Mathf.Clamp(num2 - i, 0, num));
				MyQuads.meshes[num3] = MyQuads.GetMesh(triCount, i, totalWidth, totalHeight);
				num3++;
			}
			return MyQuads.meshes;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x00165148 File Offset: 0x00163348
		private static Mesh GetMesh(int triCount, int triOffset, int totalWidth, int totalHeight)
		{
			Mesh mesh = new Mesh();
			mesh.hideFlags = 52;
			Vector3[] array = new Vector3[triCount * 4];
			Vector2[] array2 = new Vector2[triCount * 4];
			Vector2[] array3 = new Vector2[triCount * 4];
			int[] array4 = new int[triCount * 6];
			for (int i = 0; i < triCount; i++)
			{
				int num = i * 4;
				int num2 = i * 6;
				int num3 = triOffset + i;
				float num4 = Mathf.Floor((float)(num3 % totalWidth)) / (float)totalWidth;
				float num5 = Mathf.Floor((float)(num3 / totalWidth)) / (float)totalHeight;
				Vector3 vector;
				vector..ctor(num4 * 2f - 1f, num5 * 2f - 1f, 1f);
				array[num] = vector;
				array[num + 1] = vector;
				array[num + 2] = vector;
				array[num + 3] = vector;
				array2[num] = new Vector2(0f, 0f);
				array2[num + 1] = new Vector2(1f, 0f);
				array2[num + 2] = new Vector2(0f, 1f);
				array2[num + 3] = new Vector2(1f, 1f);
				array3[num] = new Vector2(num4, num5);
				array3[num + 1] = new Vector2(num4, num5);
				array3[num + 2] = new Vector2(num4, num5);
				array3[num + 3] = new Vector2(num4, num5);
				array4[num2] = num;
				array4[num2 + 1] = num + 1;
				array4[num2 + 2] = num + 2;
				array4[num2 + 3] = num + 1;
				array4[num2 + 4] = num + 2;
				array4[num2 + 5] = num + 3;
			}
			mesh.vertices = array;
			mesh.triangles = array4;
			mesh.uv = array2;
			mesh.uv2 = array3;
			return mesh;
		}

		// Token: 0x04002CE0 RID: 11488
		private static Mesh[] meshes;

		// Token: 0x04002CE1 RID: 11489
		private static int currentQuads;
	}
}
