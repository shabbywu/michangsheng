using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F4 RID: 756
public class MyUIPolygon : MaskableGraphic
{
	// Token: 0x06001A6D RID: 6765 RVA: 0x000BC5C0 File Offset: 0x000BA7C0
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

	// Token: 0x06001A6E RID: 6766 RVA: 0x000BC606 File Offset: 0x000BA806
	public void DrawPolygon(int _sides, float[] _VerticesDistances)
	{
		this.sides = _sides;
		this.VerticesDistances = _VerticesDistances;
		this.rotation = 0f;
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x000BC621 File Offset: 0x000BA821
	public void DrawPolygon(int _sides, float[] _VerticesDistances, float _rotation)
	{
		this.sides = _sides;
		this.VerticesDistances = _VerticesDistances;
		this.rotation = _rotation;
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x000BC638 File Offset: 0x000BA838
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

	// Token: 0x06001A71 RID: 6769 RVA: 0x000BC6E0 File Offset: 0x000BA8E0
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

	// Token: 0x06001A72 RID: 6770 RVA: 0x000BC920 File Offset: 0x000BAB20
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

	// Token: 0x06001A73 RID: 6771 RVA: 0x000BC981 File Offset: 0x000BAB81
	public void updateImage()
	{
		this.SetNativeSize();
		this.SetVerticesDirty();
	}

	// Token: 0x0400153F RID: 5439
	public bool fill = true;

	// Token: 0x04001540 RID: 5440
	public float thickness = 5f;

	// Token: 0x04001541 RID: 5441
	[Range(3f, 360f)]
	public int sides = 3;

	// Token: 0x04001542 RID: 5442
	[Range(0f, 360f)]
	public float rotation;

	// Token: 0x04001543 RID: 5443
	[Range(0f, 1f)]
	public float[] VerticesDistances = new float[3];

	// Token: 0x04001544 RID: 5444
	private float size;

	// Token: 0x04001545 RID: 5445
	private UIVertex[] vbo = new UIVertex[4];
}
