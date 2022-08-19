using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[ExecuteInEditMode]
public class Lightbeam : MonoBehaviour
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00015D63 File Offset: 0x00013F63
	// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00015D70 File Offset: 0x00013F70
	public float RadiusTop
	{
		get
		{
			return this.Settings.RadiusTop;
		}
		set
		{
			this.Settings.RadiusTop = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00015D7E File Offset: 0x00013F7E
	// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00015D8B File Offset: 0x00013F8B
	public float RadiusBottom
	{
		get
		{
			return this.Settings.RadiusBottom;
		}
		set
		{
			this.Settings.RadiusBottom = value;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00015D99 File Offset: 0x00013F99
	// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00015DA6 File Offset: 0x00013FA6
	public float Length
	{
		get
		{
			return this.Settings.Length;
		}
		set
		{
			this.Settings.Length = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00015DB4 File Offset: 0x00013FB4
	// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00015DC1 File Offset: 0x00013FC1
	public int Subdivisions
	{
		get
		{
			return this.Settings.Subdivisions;
		}
		set
		{
			this.Settings.Subdivisions = value;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060003FA RID: 1018 RVA: 0x00015DCF File Offset: 0x00013FCF
	// (set) Token: 0x060003FB RID: 1019 RVA: 0x00015DDC File Offset: 0x00013FDC
	public int SubdivisionsHeight
	{
		get
		{
			return this.Settings.SubdivisionsHeight;
		}
		set
		{
			this.Settings.SubdivisionsHeight = value;
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00015DEC File Offset: 0x00013FEC
	public void GenerateBeam()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		CombineInstance[] array = new CombineInstance[2];
		array[0].mesh = this.GenerateMesh(false);
		array[0].transform = Matrix4x4.identity;
		array[1].mesh = this.GenerateMesh(true);
		array[1].transform = Matrix4x4.identity;
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(array);
		if (component.sharedMesh == null)
		{
			component.sharedMesh = new Mesh();
		}
		component.sharedMesh.Clear();
		component.sharedMesh.vertices = mesh.vertices;
		component.sharedMesh.uv = mesh.uv;
		component.sharedMesh.triangles = mesh.triangles;
		component.sharedMesh.tangents = mesh.tangents;
		component.sharedMesh.normals = mesh.normals;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00015ED8 File Offset: 0x000140D8
	private Mesh GenerateMesh(bool reverseNormals)
	{
		int num = this.Settings.Subdivisions * (this.Settings.SubdivisionsHeight + 1);
		num += this.Settings.SubdivisionsHeight + 1;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		Vector3[] array3 = new Vector3[num];
		int[] array4 = new int[this.Settings.Subdivisions * 2 * this.Settings.SubdivisionsHeight * 3];
		int num2 = this.Settings.SubdivisionsHeight + 1;
		float num3 = 6.2831855f / (float)this.Settings.Subdivisions;
		float lengthFrac = this.Settings.Length / (float)this.Settings.SubdivisionsHeight;
		float num4 = 1f / (float)this.Settings.Subdivisions;
		float num5 = 1f / (float)this.Settings.SubdivisionsHeight;
		for (int i = 0; i < this.Settings.Subdivisions + 1; i++)
		{
			float xAngle = Mathf.Cos((float)i * num3);
			float yAngle = Mathf.Sin((float)i * num3);
			Vector3 vector = Lightbeam.CalculateVertex(lengthFrac, xAngle, yAngle, 0, this.Settings.RadiusTop);
			Vector3 vector2 = Lightbeam.CalculateVertex(lengthFrac, xAngle, yAngle, num2 - 1, this.Settings.RadiusBottom) - vector;
			for (int j = 0; j < num2; j++)
			{
				float radius = Mathf.Lerp(this.Settings.RadiusTop, this.Settings.RadiusBottom, num5 * (float)j);
				Vector3 vector3 = Lightbeam.CalculateVertex(lengthFrac, xAngle, yAngle, j, radius);
				Vector3 vector4 = Vector3.Cross(vector2.normalized, new Vector3(vector3.x, 0f, vector3.z).normalized);
				if (reverseNormals)
				{
					vector4 = Vector3.Cross(vector2.normalized, vector4.normalized);
				}
				else
				{
					vector4 = Vector3.Cross(vector4.normalized, vector2.normalized);
				}
				int num6 = i * num2 + j;
				array[num6] = vector3;
				array2[num6] = new Vector2(num4 * (float)i, 1f - num5 * (float)j);
				array3[num6] = vector4.normalized;
				array2[num6] = new Vector2(num4 * (float)i, 1f - num5 * (float)j);
			}
		}
		int num7 = 0;
		for (int k = 0; k < this.Settings.Subdivisions; k++)
		{
			for (int l = 0; l < num2 - 1; l++)
			{
				int num8 = k * num2 + l;
				int num9 = num8 + 1;
				int num10 = num8 + num2;
				if (num10 >= num)
				{
					num10 %= num;
				}
				if (reverseNormals)
				{
					array4[num7++] = num8;
					array4[num7++] = num9;
					array4[num7++] = num10;
				}
				else
				{
					array4[num7++] = num9;
					array4[num7++] = num8;
					array4[num7++] = num10;
				}
				int num11 = num8 + 1;
				int num12 = num8 + num2;
				if (num12 >= num)
				{
					num12 %= num;
				}
				int num13 = num12 + 1;
				if (reverseNormals)
				{
					array4[num7++] = num11;
					array4[num7++] = num13;
					array4[num7++] = num12;
				}
				else
				{
					array4[num7++] = num11;
					array4[num7++] = num12;
					array4[num7++] = num13;
				}
			}
		}
		Mesh mesh = new Mesh();
		mesh.Clear();
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array4;
		mesh.normals = array3;
		mesh.RecalculateBounds();
		Lightbeam.CalculateMeshTangents(mesh);
		return mesh;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00016278 File Offset: 0x00014478
	private static Vector3 CalculateVertex(float lengthFrac, float xAngle, float yAngle, int j, float radius)
	{
		float num = radius * xAngle;
		float num2 = radius * yAngle;
		return new Vector3(num, (float)j * (lengthFrac * -1f), num2);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000162A0 File Offset: 0x000144A0
	private static void CalculateMeshTangents(Mesh mesh)
	{
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		int num = triangles.Length;
		int num2 = vertices.Length;
		Vector3[] array = new Vector3[num2];
		Vector3[] array2 = new Vector3[num2];
		Vector4[] array3 = new Vector4[num2];
		for (long num3 = 0L; num3 < (long)num; num3 += 3L)
		{
			long num4 = (long)triangles[(int)(checked((IntPtr)num3))];
			long num5 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 1L))))];
			long num6 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 2L))))];
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			Vector2 vector4;
			Vector2 vector5;
			Vector2 vector6;
			checked
			{
				vector = vertices[(int)((IntPtr)num4)];
				vector2 = vertices[(int)((IntPtr)num5)];
				vector3 = vertices[(int)((IntPtr)num6)];
				vector4 = uv[(int)((IntPtr)num4)];
				vector5 = uv[(int)((IntPtr)num5)];
				vector6 = uv[(int)((IntPtr)num6)];
			}
			float num7 = vector2.x - vector.x;
			float num8 = vector3.x - vector.x;
			float num9 = vector2.y - vector.y;
			float num10 = vector3.y - vector.y;
			float num11 = vector2.z - vector.z;
			float num12 = vector3.z - vector.z;
			float num13 = vector5.x - vector4.x;
			float num14 = vector6.x - vector4.x;
			float num15 = vector5.y - vector4.y;
			float num16 = vector6.y - vector4.y;
			float num17 = 1f / (num13 * num16 - num14 * num15);
			Vector3 vector7;
			vector7..ctor((num16 * num7 - num15 * num8) * num17, (num16 * num9 - num15 * num10) * num17, (num16 * num11 - num15 * num12) * num17);
			Vector3 vector8;
			vector8..ctor((num13 * num8 - num14 * num7) * num17, (num13 * num10 - num14 * num9) * num17, (num13 * num12 - num14 * num11) * num17);
			checked
			{
				array[(int)((IntPtr)num4)] += vector7;
				array[(int)((IntPtr)num5)] += vector7;
				array[(int)((IntPtr)num6)] += vector7;
				array2[(int)((IntPtr)num4)] += vector8;
				array2[(int)((IntPtr)num5)] += vector8;
				array2[(int)((IntPtr)num6)] += vector8;
			}
		}
		for (long num18 = 0L; num18 < (long)num2; num18 += 1L)
		{
			checked
			{
				Vector3 vector9 = normals[(int)((IntPtr)num18)];
				Vector3 vector10 = array[(int)((IntPtr)num18)];
				Vector3.OrthoNormalize(ref vector9, ref vector10);
				array3[(int)((IntPtr)num18)].x = vector10.x;
				array3[(int)((IntPtr)num18)].y = vector10.y;
				array3[(int)((IntPtr)num18)].z = vector10.z;
				array3[(int)((IntPtr)num18)].w = ((Vector3.Dot(Vector3.Cross(vector9, vector10), array2[(int)((IntPtr)num18)]) < 0f) ? -1f : 1f);
			}
		}
		mesh.tangents = array3;
	}

	// Token: 0x04000228 RID: 552
	public bool IsModifyingMesh;

	// Token: 0x04000229 RID: 553
	public Material DefaultMaterial;

	// Token: 0x0400022A RID: 554
	public LightbeamSettings Settings;
}
