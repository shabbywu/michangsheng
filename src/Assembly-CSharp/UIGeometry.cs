using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class UIGeometry
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002F0AD File Offset: 0x0002D2AD
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002F0BD File Offset: 0x0002D2BD
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0002F0EF File Offset: 0x0002D2EF
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002F120 File Offset: 0x0002D320
	public void ApplyTransform(Matrix4x4 widgetToPanel)
	{
		if (this.verts.size > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int size = this.verts.size;
			while (i < size)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
			this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
			return;
		}
		this.mRtpVerts.Clear();
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002F1DC File Offset: 0x0002D3DC
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < this.mRtpVerts.size; i++)
				{
					v.Add(this.mRtpVerts.buffer[i]);
					u.Add(this.uvs.buffer[i]);
					c.Add(this.cols.buffer[i]);
				}
				return;
			}
			for (int j = 0; j < this.mRtpVerts.size; j++)
			{
				v.Add(this.mRtpVerts.buffer[j]);
				u.Add(this.uvs.buffer[j]);
				c.Add(this.cols.buffer[j]);
				n.Add(this.mRtpNormal);
				t.Add(this.mRtpTan);
			}
		}
	}

	// Token: 0x040004CB RID: 1227
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040004CC RID: 1228
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040004CD RID: 1229
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x040004CE RID: 1230
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x040004CF RID: 1231
	private Vector3 mRtpNormal;

	// Token: 0x040004D0 RID: 1232
	private Vector4 mRtpTan;
}
