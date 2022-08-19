using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
[Serializable]
public class LTSpline
{
	// Token: 0x06000138 RID: 312 RVA: 0x00007A39 File Offset: 0x00005C39
	public LTSpline(Vector3[] pts)
	{
		this.init(pts, true);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007A50 File Offset: 0x00005C50
	public LTSpline(Vector3[] pts, bool constantSpeed)
	{
		this.constantSpeed = constantSpeed;
		this.init(pts, constantSpeed);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007A70 File Offset: 0x00005C70
	private void init(Vector3[] pts, bool constantSpeed)
	{
		if (pts.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a spline path, you must pass four or more values!");
			return;
		}
		this.pts = new Vector3[pts.Length];
		Array.Copy(pts, this.pts, pts.Length);
		this.numSections = pts.Length - 3;
		float num = float.PositiveInfinity;
		Vector3 vector = this.pts[1];
		float num2 = 0f;
		for (int i = 1; i < this.pts.Length - 1; i++)
		{
			float num3 = Vector3.Distance(this.pts[i], vector);
			if (num3 < num)
			{
				num = num3;
			}
			num2 += num3;
		}
		if (constantSpeed)
		{
			num = num2 / (float)(this.numSections * LTSpline.SUBLINE_COUNT);
			float num4 = num / (float)LTSpline.SUBLINE_COUNT;
			int num5 = (int)Mathf.Ceil(num2 / num4) * LTSpline.DISTANCE_COUNT;
			if (num5 <= 1)
			{
				num5 = 2;
			}
			this.ptsAdj = new Vector3[num5];
			vector = this.interp(0f);
			int num6 = 1;
			this.ptsAdj[0] = vector;
			this.distance = 0f;
			for (int j = 0; j < num5 + 1; j++)
			{
				float num7 = (float)j / (float)num5;
				Vector3 vector2 = this.interp(num7);
				float num8 = Vector3.Distance(vector2, vector);
				if (num8 >= num4 || num7 >= 1f)
				{
					this.ptsAdj[num6] = vector2;
					this.distance += num8;
					vector = vector2;
					num6++;
				}
			}
			this.ptsAdjLength = num6;
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00007BE4 File Offset: 0x00005DE4
	public Vector3 map(float u)
	{
		if (u >= 1f)
		{
			return this.pts[this.pts.Length - 2];
		}
		float num = u * (float)(this.ptsAdjLength - 1);
		int num2 = (int)Mathf.Floor(num);
		int num3 = (int)Mathf.Ceil(num);
		if (num2 < 0)
		{
			num2 = 0;
		}
		Vector3 vector = this.ptsAdj[num2];
		Vector3 vector2 = this.ptsAdj[num3];
		float num4 = num - (float)num2;
		return vector + (vector2 - vector) * num4;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007C68 File Offset: 0x00005E68
	public Vector3 interp(float t)
	{
		this.currPt = Mathf.Min(Mathf.FloorToInt(t * (float)this.numSections), this.numSections - 1);
		float num = t * (float)this.numSections - (float)this.currPt;
		Vector3 vector = this.pts[this.currPt];
		Vector3 vector2 = this.pts[this.currPt + 1];
		Vector3 vector3 = this.pts[this.currPt + 2];
		Vector3 vector4 = this.pts[this.currPt + 3];
		return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num * num * num) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num * num) + (-vector + vector3) * num + 2f * vector2);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00007DA0 File Offset: 0x00005FA0
	public float ratioAtPoint(Vector3 pt)
	{
		float num = float.MaxValue;
		int num2 = 0;
		for (int i = 0; i < this.ptsAdjLength; i++)
		{
			float num3 = Vector3.Distance(pt, this.ptsAdj[i]);
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		return (float)num2 / (float)(this.ptsAdjLength - 1);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007DF0 File Offset: 0x00005FF0
	public Vector3 point(float ratio)
	{
		float num = (ratio > 1f) ? 1f : ratio;
		if (!this.constantSpeed)
		{
			return this.interp(num);
		}
		return this.map(num);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00007E28 File Offset: 0x00006028
	public void place2d(Transform transform, float ratio)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = this.point(ratio) - transform.position;
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007E94 File Offset: 0x00006094
	public void placeLocal2d(Transform transform, float ratio)
	{
		if (transform.parent == null)
		{
			this.place2d(transform, ratio);
			return;
		}
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = this.point(ratio) - transform.localPosition;
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.localEulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00007F17 File Offset: 0x00006117
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00007F26 File Offset: 0x00006126
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00007F54 File Offset: 0x00006154
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00007F63 File Offset: 0x00006163
	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007F9C File Offset: 0x0000619C
	public void gizmoDraw(float t = -1f)
	{
		if (this.ptsAdj == null || this.ptsAdj.Length == 0)
		{
			return;
		}
		Vector3 vector = this.ptsAdj[0];
		for (int i = 0; i < this.ptsAdjLength; i++)
		{
			Vector3 vector2 = this.ptsAdj[i];
			Gizmos.DrawLine(vector, vector2);
			vector = vector2;
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00007FF0 File Offset: 0x000061F0
	public void drawGizmo(Color color)
	{
		if (this.ptsAdjLength >= 4)
		{
			Vector3 vector = this.ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int i = 0; i < this.ptsAdjLength; i++)
			{
				Vector3 vector2 = this.ptsAdj[i];
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			Gizmos.color = color2;
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000804C File Offset: 0x0000624C
	public static void drawGizmo(Transform[] arr, Color color)
	{
		if (arr.Length >= 4)
		{
			Vector3[] array = new Vector3[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				array[i] = arr[i].position;
			}
			LTSpline ltspline = new LTSpline(array);
			Vector3 vector = ltspline.ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int j = 0; j < ltspline.ptsAdjLength; j++)
			{
				Vector3 vector2 = ltspline.ptsAdj[j];
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			Gizmos.color = color2;
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000080E3 File Offset: 0x000062E3
	public static void drawLine(Transform[] arr, float width, Color color)
	{
		int num = arr.Length;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000080EC File Offset: 0x000062EC
	public void drawLinesGLLines(Material outlineMaterial, Color color, float width)
	{
		GL.PushMatrix();
		outlineMaterial.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Begin(1);
		GL.Color(color);
		if (this.constantSpeed)
		{
			if (this.ptsAdjLength >= 4)
			{
				Vector3 vector = this.ptsAdj[0];
				for (int i = 0; i < this.ptsAdjLength; i++)
				{
					Vector3 vector2 = this.ptsAdj[i];
					GL.Vertex(vector);
					GL.Vertex(vector2);
					vector = vector2;
				}
			}
		}
		else if (this.pts.Length >= 4)
		{
			Vector3 vector3 = this.pts[0];
			float num = 1f / ((float)this.pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 vector4 = this.interp(t);
				GL.Vertex(vector3);
				GL.Vertex(vector4);
				vector3 = vector4;
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000081D4 File Offset: 0x000063D4
	public Vector3[] generateVectors()
	{
		if (this.pts.Length >= 4)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3 item = this.pts[0];
			list.Add(item);
			float num = 1f / ((float)this.pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 item2 = this.interp(t);
				list.Add(item2);
			}
			list.ToArray();
		}
		return null;
	}

	// Token: 0x040000F4 RID: 244
	public static int DISTANCE_COUNT = 3;

	// Token: 0x040000F5 RID: 245
	public static int SUBLINE_COUNT = 20;

	// Token: 0x040000F6 RID: 246
	public float distance;

	// Token: 0x040000F7 RID: 247
	public bool constantSpeed = true;

	// Token: 0x040000F8 RID: 248
	public Vector3[] pts;

	// Token: 0x040000F9 RID: 249
	[NonSerialized]
	public Vector3[] ptsAdj;

	// Token: 0x040000FA RID: 250
	public int ptsAdjLength;

	// Token: 0x040000FB RID: 251
	public bool orientToPath;

	// Token: 0x040000FC RID: 252
	public bool orientToPath2d;

	// Token: 0x040000FD RID: 253
	private int numSections;

	// Token: 0x040000FE RID: 254
	private int currPt;
}
