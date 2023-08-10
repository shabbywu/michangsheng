using System;
using UnityEngine;

[ExecuteInEditMode]
public class Lightbeam : MonoBehaviour
{
	public bool IsModifyingMesh;

	public Material DefaultMaterial;

	public LightbeamSettings Settings;

	public float RadiusTop
	{
		get
		{
			return Settings.RadiusTop;
		}
		set
		{
			Settings.RadiusTop = value;
		}
	}

	public float RadiusBottom
	{
		get
		{
			return Settings.RadiusBottom;
		}
		set
		{
			Settings.RadiusBottom = value;
		}
	}

	public float Length
	{
		get
		{
			return Settings.Length;
		}
		set
		{
			Settings.Length = value;
		}
	}

	public int Subdivisions
	{
		get
		{
			return Settings.Subdivisions;
		}
		set
		{
			Settings.Subdivisions = value;
		}
	}

	public int SubdivisionsHeight
	{
		get
		{
			return Settings.SubdivisionsHeight;
		}
		set
		{
			Settings.SubdivisionsHeight = value;
		}
	}

	public void GenerateBeam()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		MeshFilter component = ((Component)this).GetComponent<MeshFilter>();
		CombineInstance[] array = (CombineInstance[])(object)new CombineInstance[2];
		((CombineInstance)(ref array[0])).mesh = GenerateMesh(reverseNormals: false);
		((CombineInstance)(ref array[0])).transform = Matrix4x4.identity;
		((CombineInstance)(ref array[1])).mesh = GenerateMesh(reverseNormals: true);
		((CombineInstance)(ref array[1])).transform = Matrix4x4.identity;
		Mesh val = new Mesh();
		val.CombineMeshes(array);
		if ((Object)(object)component.sharedMesh == (Object)null)
		{
			component.sharedMesh = new Mesh();
		}
		component.sharedMesh.Clear();
		component.sharedMesh.vertices = val.vertices;
		component.sharedMesh.uv = val.uv;
		component.sharedMesh.triangles = val.triangles;
		component.sharedMesh.tangents = val.tangents;
		component.sharedMesh.normals = val.normals;
	}

	private Mesh GenerateMesh(bool reverseNormals)
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_0393: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		int num = Settings.Subdivisions * (Settings.SubdivisionsHeight + 1);
		num += Settings.SubdivisionsHeight + 1;
		Vector3[] array = (Vector3[])(object)new Vector3[num];
		Vector2[] array2 = (Vector2[])(object)new Vector2[num];
		Vector3[] array3 = (Vector3[])(object)new Vector3[num];
		int[] array4 = new int[Settings.Subdivisions * 2 * Settings.SubdivisionsHeight * 3];
		int num2 = Settings.SubdivisionsHeight + 1;
		float num3 = (float)Math.PI * 2f / (float)Settings.Subdivisions;
		float lengthFrac = Settings.Length / (float)Settings.SubdivisionsHeight;
		float num4 = 1f / (float)Settings.Subdivisions;
		float num5 = 1f / (float)Settings.SubdivisionsHeight;
		for (int i = 0; i < Settings.Subdivisions + 1; i++)
		{
			float xAngle = Mathf.Cos((float)i * num3);
			float yAngle = Mathf.Sin((float)i * num3);
			Vector3 val = CalculateVertex(lengthFrac, xAngle, yAngle, 0, Settings.RadiusTop);
			Vector3 val2 = CalculateVertex(lengthFrac, xAngle, yAngle, num2 - 1, Settings.RadiusBottom) - val;
			for (int j = 0; j < num2; j++)
			{
				float radius = Mathf.Lerp(Settings.RadiusTop, Settings.RadiusBottom, num5 * (float)j);
				Vector3 val3 = CalculateVertex(lengthFrac, xAngle, yAngle, j, radius);
				Vector3 normalized = ((Vector3)(ref val2)).normalized;
				Vector3 val4 = new Vector3(val3.x, 0f, val3.z);
				Vector3 val5 = Vector3.Cross(normalized, ((Vector3)(ref val4)).normalized);
				val5 = ((!reverseNormals) ? Vector3.Cross(((Vector3)(ref val5)).normalized, ((Vector3)(ref val2)).normalized) : Vector3.Cross(((Vector3)(ref val2)).normalized, ((Vector3)(ref val5)).normalized));
				int num6 = i * num2 + j;
				array[num6] = val3;
				array2[num6] = new Vector2(num4 * (float)i, 1f - num5 * (float)j);
				array3[num6] = ((Vector3)(ref val5)).normalized;
				array2[num6] = new Vector2(num4 * (float)i, 1f - num5 * (float)j);
			}
		}
		int num7 = 0;
		for (int k = 0; k < Settings.Subdivisions; k++)
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
		Mesh val6 = new Mesh();
		val6.Clear();
		val6.vertices = array;
		val6.uv = array2;
		val6.triangles = array4;
		val6.normals = array3;
		val6.RecalculateBounds();
		CalculateMeshTangents(val6);
		return val6;
	}

	private static Vector3 CalculateVertex(float lengthFrac, float xAngle, float yAngle, int j, float radius)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		float num = radius * xAngle;
		float num2 = radius * yAngle;
		return new Vector3(num, (float)j * (lengthFrac * -1f), num2);
	}

	private static void CalculateMeshTangents(Mesh mesh)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		int num = triangles.Length;
		int num2 = vertices.Length;
		Vector3[] array = (Vector3[])(object)new Vector3[num2];
		Vector3[] array2 = (Vector3[])(object)new Vector3[num2];
		Vector4[] array3 = (Vector4[])(object)new Vector4[num2];
		Vector3 val7 = default(Vector3);
		Vector3 val8 = default(Vector3);
		for (long num3 = 0L; num3 < num; num3 += 3)
		{
			long num4 = triangles[num3];
			long num5 = triangles[num3 + 1];
			long num6 = triangles[num3 + 2];
			Vector3 val = vertices[num4];
			Vector3 val2 = vertices[num5];
			Vector3 val3 = vertices[num6];
			Vector2 val4 = uv[num4];
			Vector2 val5 = uv[num5];
			Vector2 val6 = uv[num6];
			float num7 = val2.x - val.x;
			float num8 = val3.x - val.x;
			float num9 = val2.y - val.y;
			float num10 = val3.y - val.y;
			float num11 = val2.z - val.z;
			float num12 = val3.z - val.z;
			float num13 = val5.x - val4.x;
			float num14 = val6.x - val4.x;
			float num15 = val5.y - val4.y;
			float num16 = val6.y - val4.y;
			float num17 = 1f / (num13 * num16 - num14 * num15);
			((Vector3)(ref val7))._002Ector((num16 * num7 - num15 * num8) * num17, (num16 * num9 - num15 * num10) * num17, (num16 * num11 - num15 * num12) * num17);
			((Vector3)(ref val8))._002Ector((num13 * num8 - num14 * num7) * num17, (num13 * num10 - num14 * num9) * num17, (num13 * num12 - num14 * num11) * num17);
			ref Vector3 reference = ref array[num4];
			reference += val7;
			ref Vector3 reference2 = ref array[num5];
			reference2 += val7;
			ref Vector3 reference3 = ref array[num6];
			reference3 += val7;
			ref Vector3 reference4 = ref array2[num4];
			reference4 += val8;
			ref Vector3 reference5 = ref array2[num5];
			reference5 += val8;
			ref Vector3 reference6 = ref array2[num6];
			reference6 += val8;
		}
		for (long num18 = 0L; num18 < num2; num18++)
		{
			Vector3 val9 = normals[num18];
			Vector3 val10 = array[num18];
			Vector3.OrthoNormalize(ref val9, ref val10);
			array3[num18].x = val10.x;
			array3[num18].y = val10.y;
			array3[num18].z = val10.z;
			array3[num18].w = ((Vector3.Dot(Vector3.Cross(val9, val10), array2[num18]) < 0f) ? (-1f) : 1f);
		}
		mesh.tangents = array3;
	}
}
