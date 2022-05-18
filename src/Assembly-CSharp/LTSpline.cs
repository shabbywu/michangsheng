using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
[Serializable]
public class LTSpline
{
	// Token: 0x0600013E RID: 318 RVA: 0x00005029 File Offset: 0x00003229
	public LTSpline(Vector3[] pts)
	{
		this.init(pts, true);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00005040 File Offset: 0x00003240
	public LTSpline(Vector3[] pts, bool constantSpeed)
	{
		this.constantSpeed = constantSpeed;
		this.init(pts, constantSpeed);
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00061410 File Offset: 0x0005F610
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

	// Token: 0x06000141 RID: 321 RVA: 0x00061584 File Offset: 0x0005F784
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

	// Token: 0x06000142 RID: 322 RVA: 0x00061608 File Offset: 0x0005F808
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

	// Token: 0x06000143 RID: 323 RVA: 0x00061740 File Offset: 0x0005F940
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

	// Token: 0x06000144 RID: 324 RVA: 0x00061790 File Offset: 0x0005F990
	public Vector3 point(float ratio)
	{
		float num = (ratio > 1f) ? 1f : ratio;
		if (!this.constantSpeed)
		{
			return this.interp(num);
		}
		return this.map(num);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000617C8 File Offset: 0x0005F9C8
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

	// Token: 0x06000146 RID: 326 RVA: 0x00061834 File Offset: 0x0005FA34
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

	// Token: 0x06000147 RID: 327 RVA: 0x0000505E File Offset: 0x0000325E
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000506D File Offset: 0x0000326D
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000509B File Offset: 0x0000329B
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000050AA File Offset: 0x000032AA
	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000618B8 File Offset: 0x0005FAB8
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

	// Token: 0x0600014C RID: 332 RVA: 0x0006190C File Offset: 0x0005FB0C
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

	// Token: 0x0600014D RID: 333 RVA: 0x00061968 File Offset: 0x0005FB68
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

	// Token: 0x0600014E RID: 334 RVA: 0x000050E3 File Offset: 0x000032E3
	public static void drawLine(Transform[] arr, float width, Color color)
	{
		int num = arr.Length;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00061A00 File Offset: 0x0005FC00
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

	// Token: 0x06000150 RID: 336 RVA: 0x00061AE8 File Offset: 0x0005FCE8
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

	// Token: 0x04000103 RID: 259
	public static int DISTANCE_COUNT = 3;

	// Token: 0x04000104 RID: 260
	public static int SUBLINE_COUNT = 20;

	// Token: 0x04000105 RID: 261
	public float distance;

	// Token: 0x04000106 RID: 262
	public bool constantSpeed = true;

	// Token: 0x04000107 RID: 263
	public Vector3[] pts;

	// Token: 0x04000108 RID: 264
	[NonSerialized]
	public Vector3[] ptsAdj;

	// Token: 0x04000109 RID: 265
	public int ptsAdjLength;

	// Token: 0x0400010A RID: 266
	public bool orientToPath;

	// Token: 0x0400010B RID: 267
	public bool orientToPath2d;

	// Token: 0x0400010C RID: 268
	private int numSections;

	// Token: 0x0400010D RID: 269
	private int currPt;
}
