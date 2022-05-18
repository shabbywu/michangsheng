using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class UIGeometry
{
	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600084E RID: 2126 RVA: 0x0000AD01 File Offset: 0x00008F01
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x0000AD11 File Offset: 0x00008F11
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0000AD43 File Offset: 0x00008F43
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00083B84 File Offset: 0x00081D84
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

	// Token: 0x06000852 RID: 2130 RVA: 0x00083C40 File Offset: 0x00081E40
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

	// Token: 0x040005D8 RID: 1496
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x040005D9 RID: 1497
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x040005DA RID: 1498
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x040005DB RID: 1499
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x040005DC RID: 1500
	private Vector3 mRtpNormal;

	// Token: 0x040005DD RID: 1501
	private Vector4 mRtpTan;
}
