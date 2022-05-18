using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000922 RID: 2338
	internal class MyQuads
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x001AE790 File Offset: 0x001AC990
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

		// Token: 0x06003B7D RID: 15229 RVA: 0x001AE7CC File Offset: 0x001AC9CC
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

		// Token: 0x06003B7E RID: 15230 RVA: 0x001AE81C File Offset: 0x001ACA1C
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

		// Token: 0x06003B7F RID: 15231 RVA: 0x001AE8AC File Offset: 0x001ACAAC
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

		// Token: 0x04003631 RID: 13873
		private static Mesh[] meshes;

		// Token: 0x04003632 RID: 13874
		private static int currentQuads;
	}
}
