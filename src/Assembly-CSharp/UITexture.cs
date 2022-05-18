using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Texture")]
public class UITexture : UIBasicSprite
{
	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0000D5BA File Offset: 0x0000B7BA
	// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0000D5EC File Offset: 0x0000B7EC
	public override Texture mainTexture
	{
		get
		{
			if (this.mTexture != null)
			{
				return this.mTexture;
			}
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			return null;
		}
		set
		{
			if (this.mTexture != value)
			{
				base.RemoveFromPanel();
				this.mTexture = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0000D616 File Offset: 0x0000B816
	// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0000D61E File Offset: 0x0000B81E
	public override Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				base.RemoveFromPanel();
				this.mShader = null;
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0000D64F File Offset: 0x0000B84F
	// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0000D68F File Offset: 0x0000B88F
	public override Shader shader
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.shader;
			}
			if (this.mShader == null)
			{
				this.mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				base.RemoveFromPanel();
				this.mShader = value;
				this.mPMA = -1;
				this.mMat = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00091ED4 File Offset: 0x000900D4
	public override bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
	// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
	public override Vector4 border
	{
		get
		{
			return this.mBorder;
		}
		set
		{
			if (this.mBorder != value)
			{
				this.mBorder = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
	// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0000D6ED File Offset: 0x0000B8ED
	public Rect uvRect
	{
		get
		{
			return this.mRect;
		}
		set
		{
			if (this.mRect != value)
			{
				this.mRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00091F34 File Offset: 0x00090134
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.mTexture != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int width = this.mTexture.width;
				int height = this.mTexture.height;
				int num5 = 0;
				int num6 = 0;
				float num7 = 1f;
				float num8 = 1f;
				if (width > 0 && height > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((width & 1) != 0)
					{
						num5++;
					}
					if ((height & 1) != 0)
					{
						num6++;
					}
					num7 = 1f / (float)width * (float)this.mWidth;
					num8 = 1f / (float)height * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num5 * num7;
				}
				else
				{
					num3 -= (float)num5 * num7;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num6 * num8;
				}
				else
				{
					num4 -= (float)num6 * num8;
				}
			}
			Vector4 border = this.border;
			float num9 = border.x + border.z;
			float num10 = border.y + border.w;
			float num11 = Mathf.Lerp(num, num3 - num9, this.mDrawRegion.x);
			float num12 = Mathf.Lerp(num2, num4 - num10, this.mDrawRegion.y);
			float num13 = Mathf.Lerp(num + num9, num3, this.mDrawRegion.z);
			float num14 = Mathf.Lerp(num2 + num10, num4, this.mDrawRegion.w);
			return new Vector4(num11, num12, num13, num14);
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x000920FC File Offset: 0x000902FC
	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !base.hasBorder) && mainTexture != null)
		{
			int num = mainTexture.width;
			int num2 = mainTexture.height;
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0009217C File Offset: 0x0009037C
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Rect rect;
		rect..ctor(this.mRect.x * (float)mainTexture.width, this.mRect.y * (float)mainTexture.height, (float)mainTexture.width * this.mRect.width, (float)mainTexture.height * this.mRect.height);
		Rect inner = rect;
		Vector4 border = this.border;
		inner.xMin += border.x;
		inner.yMin += border.y;
		inner.xMax -= border.z;
		inner.yMax -= border.w;
		float num = 1f / (float)mainTexture.width;
		float num2 = 1f / (float)mainTexture.height;
		rect.xMin *= num;
		rect.xMax *= num;
		rect.yMin *= num2;
		rect.yMax *= num2;
		inner.xMin *= num;
		inner.xMax *= num;
		inner.yMin *= num2;
		inner.yMax *= num2;
		int size = verts.size;
		base.Fill(verts, uvs, cols, rect, inner);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, size, verts, uvs, cols);
		}
	}

	// Token: 0x04000818 RID: 2072
	[HideInInspector]
	[SerializeField]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04000819 RID: 2073
	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	// Token: 0x0400081A RID: 2074
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400081B RID: 2075
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x0400081C RID: 2076
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x0400081D RID: 2077
	[NonSerialized]
	private int mPMA = -1;
}
