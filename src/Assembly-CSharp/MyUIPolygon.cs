using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000451 RID: 1105
public class MyUIPolygon : MaskableGraphic
{
	// Token: 0x06001D93 RID: 7571 RVA: 0x00102828 File Offset: 0x00100A28
	public void DrawPolygon(int _sides)
	{
		this.sides = _sides;
		this.VerticesDistances = new float[_sides + 1];
		for (int i = 0; i < _sides; i++)
		{
			this.VerticesDistances[i] = 1f;
		}
		this.rotation = 0f;
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x000189CB File Offset: 0x00016BCB
	public void DrawPolygon(int _sides, float[] _VerticesDistances)
	{
		this.sides = _sides;
		this.VerticesDistances = _VerticesDistances;
		this.rotation = 0f;
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x000189E6 File Offset: 0x00016BE6
	public void DrawPolygon(int _sides, float[] _VerticesDistances, float _rotation)
	{
		this.sides = _sides;
		this.VerticesDistances = _VerticesDistances;
		this.rotation = _rotation;
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00102870 File Offset: 0x00100A70
	private void Update()
	{
		this.size = base.rectTransform.rect.width;
		if (base.rectTransform.rect.width > base.rectTransform.rect.height)
		{
			this.size = base.rectTransform.rect.height;
		}
		else
		{
			this.size = base.rectTransform.rect.width;
		}
		this.thickness = Mathf.Clamp(this.thickness, 0f, this.size / 2f);
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x00102918 File Offset: 0x00100B18
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		Vector2 vector3;
		vector3..ctor(0f, 0f);
		Vector2 vector4;
		vector4..ctor(0f, 1f);
		Vector2 vector5;
		vector5..ctor(1f, 1f);
		Vector2 vector6;
		vector6..ctor(1f, 0f);
		float num = 360f / (float)this.sides;
		int num2 = this.sides + 1;
		if (this.VerticesDistances.Length != num2)
		{
			this.VerticesDistances = new float[num2];
			for (int i = 0; i < num2 - 1; i++)
			{
				this.VerticesDistances[i] = 1f;
			}
		}
		this.VerticesDistances[num2 - 1] = this.VerticesDistances[0];
		for (int j = 0; j < num2; j++)
		{
			float num3 = -base.rectTransform.pivot.x * this.size * this.VerticesDistances[j];
			float num4 = -base.rectTransform.pivot.x * this.size * this.VerticesDistances[j] + this.thickness;
			float num5 = 0.017453292f * ((float)j * num + this.rotation);
			float num6 = Mathf.Cos(num5);
			float num7 = Mathf.Sin(num5);
			vector3..ctor(0f, 1f);
			vector4..ctor(1f, 1f);
			vector5..ctor(1f, 0f);
			vector6..ctor(0f, 0f);
			Vector2 vector7 = vector;
			Vector2 vector8;
			vector8..ctor(num3 * num6, num3 * num7);
			Vector2 zero;
			Vector2 vector9;
			if (this.fill)
			{
				zero = Vector2.zero;
				vector9 = Vector2.zero;
			}
			else
			{
				zero..ctor(num4 * num6, num4 * num7);
				vector9 = vector2;
			}
			vector = vector8;
			vector2 = zero;
			this.SetVbo(new Vector2[]
			{
				vector7,
				vector8,
				zero,
				vector9
			}, new Vector2[]
			{
				vector3,
				vector4,
				vector5,
				vector6
			});
			vh.AddUIVertexQuad(this.vbo);
		}
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x00102B58 File Offset: 0x00100D58
	private void SetVbo(Vector2[] vertices, Vector2[] uvs)
	{
		for (int i = 0; i < vertices.Length; i++)
		{
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = this.color;
			simpleVert.position = vertices[i];
			simpleVert.uv0 = uvs[i];
			this.vbo[i] = simpleVert;
		}
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x000189FD File Offset: 0x00016BFD
	public void updateImage()
	{
		this.SetNativeSize();
		this.SetVerticesDirty();
	}

	// Token: 0x0400194C RID: 6476
	public bool fill = true;

	// Token: 0x0400194D RID: 6477
	public float thickness = 5f;

	// Token: 0x0400194E RID: 6478
	[Range(3f, 360f)]
	public int sides = 3;

	// Token: 0x0400194F RID: 6479
	[Range(0f, 360f)]
	public float rotation;

	// Token: 0x04001950 RID: 6480
	[Range(0f, 1f)]
	public float[] VerticesDistances = new float[3];

	// Token: 0x04001951 RID: 6481
	private float size;

	// Token: 0x04001952 RID: 6482
	private UIVertex[] vbo = new UIVertex[4];
}
