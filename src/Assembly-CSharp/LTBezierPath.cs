using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class LTBezierPath
{
	// Token: 0x06000132 RID: 306 RVA: 0x0000403D File Offset: 0x0000223D
	public LTBezierPath()
	{
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00004FC6 File Offset: 0x000031C6
	public LTBezierPath(Vector3[] pts_)
	{
		this.setPoints(pts_);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000610F0 File Offset: 0x0005F2F0
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

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000135 RID: 309 RVA: 0x00004FD5 File Offset: 0x000031D5
	public float distance
	{
		get
		{
			return this.length;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00061204 File Offset: 0x0005F404
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

	// Token: 0x06000137 RID: 311 RVA: 0x00061278 File Offset: 0x0005F478
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

	// Token: 0x06000138 RID: 312 RVA: 0x000612E4 File Offset: 0x0005F4E4
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

	// Token: 0x06000139 RID: 313 RVA: 0x00004FDD File Offset: 0x000031DD
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00004FEC File Offset: 0x000031EC
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000501A File Offset: 0x0000321A
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00061350 File Offset: 0x0005F550
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

	// Token: 0x0600013D RID: 317 RVA: 0x000613A4 File Offset: 0x0005F5A4
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

	// Token: 0x040000FB RID: 251
	public Vector3[] pts;

	// Token: 0x040000FC RID: 252
	public float length;

	// Token: 0x040000FD RID: 253
	public bool orientToPath;

	// Token: 0x040000FE RID: 254
	public bool orientToPath2d;

	// Token: 0x040000FF RID: 255
	private LTBezier[] beziers;

	// Token: 0x04000100 RID: 256
	private float[] lengthRatio;

	// Token: 0x04000101 RID: 257
	private int currentBezier;

	// Token: 0x04000102 RID: 258
	private int previousBezier;
}
