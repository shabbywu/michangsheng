using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class LTBezierPath
{
	// Token: 0x0600012C RID: 300 RVA: 0x000027FC File Offset: 0x000009FC
	public LTBezierPath()
	{
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000076B6 File Offset: 0x000058B6
	public LTBezierPath(Vector3[] pts_)
	{
		this.setPoints(pts_);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000076C8 File Offset: 0x000058C8
	public void setPoints(Vector3[] pts_)
	{
		if (pts_.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, you must pass four or more values!");
		}
		if (pts_.Length % 4 != 0)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, they must be in sets of four: controlPoint1, controlPoint2, endPoint2, controlPoint2, controlPoint2...");
		}
		this.pts = pts_;
		int num = 0;
		this.beziers = new LTBezier[this.pts.Length / 4];
		this.lengthRatio = new float[this.beziers.Length];
		this.length = 0f;
		for (int i = 0; i < this.pts.Length; i += 4)
		{
			this.beziers[num] = new LTBezier(this.pts[i], this.pts[i + 2], this.pts[i + 1], this.pts[i + 3], 0.05f);
			this.length += this.beziers[num].length;
			num++;
		}
		for (int i = 0; i < this.beziers.Length; i++)
		{
			this.lengthRatio[i] = this.beziers[i].length / this.length;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600012F RID: 303 RVA: 0x000077DC File Offset: 0x000059DC
	public float distance
	{
		get
		{
			return this.length;
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000077E4 File Offset: 0x000059E4
	public Vector3 point(float ratio)
	{
		float num = 0f;
		for (int i = 0; i < this.lengthRatio.Length; i++)
		{
			num += this.lengthRatio[i];
			if (num >= ratio)
			{
				return this.beziers[i].point((ratio - (num - this.lengthRatio[i])) / this.lengthRatio[i]);
			}
		}
		return this.beziers[this.lengthRatio.Length - 1].point(1f);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00007858 File Offset: 0x00005A58
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

	// Token: 0x06000132 RID: 306 RVA: 0x000078C4 File Offset: 0x00005AC4
	public void placeLocal2d(Transform transform, float ratio)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = this.point(ratio) - transform.localPosition;
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.localEulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00007930 File Offset: 0x00005B30
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000793F File Offset: 0x00005B3F
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000796D File Offset: 0x00005B6D
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000797C File Offset: 0x00005B7C
	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		ratio = Mathf.Clamp01(ratio);
		transform.localPosition = this.point(ratio);
		ratio = Mathf.Clamp01(ratio + 0.001f);
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000079D0 File Offset: 0x00005BD0
	public void gizmoDraw(float t = -1f)
	{
		Vector3 vector = this.point(0f);
		for (int i = 1; i <= 120; i++)
		{
			float ratio = (float)i / 120f;
			Vector3 vector2 = this.point(ratio);
			Gizmos.color = ((this.previousBezier == this.currentBezier) ? Color.magenta : Color.grey);
			Gizmos.DrawLine(vector2, vector);
			vector = vector2;
			this.previousBezier = this.currentBezier;
		}
	}

	// Token: 0x040000EC RID: 236
	public Vector3[] pts;

	// Token: 0x040000ED RID: 237
	public float length;

	// Token: 0x040000EE RID: 238
	public bool orientToPath;

	// Token: 0x040000EF RID: 239
	public bool orientToPath2d;

	// Token: 0x040000F0 RID: 240
	private LTBezier[] beziers;

	// Token: 0x040000F1 RID: 241
	private float[] lengthRatio;

	// Token: 0x040000F2 RID: 242
	private int currentBezier;

	// Token: 0x040000F3 RID: 243
	private int previousBezier;
}
