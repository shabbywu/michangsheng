using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Texture")]
public class UITexture : UIBasicSprite
{
	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0003F8B7 File Offset: 0x0003DAB7
	// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0003F8E9 File Offset: 0x0003DAE9
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

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0003F913 File Offset: 0x0003DB13
	// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0003F91B File Offset: 0x0003DB1B
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

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0003F94C File Offset: 0x0003DB4C
	// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0003F98C File Offset: 0x0003DB8C
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

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0003F9C0 File Offset: 0x0003DBC0
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

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0003FA1E File Offset: 0x0003DC1E
	// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0003FA26 File Offset: 0x0003DC26
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

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0003FA43 File Offset: 0x0003DC43
	// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0003FA4B File Offset: 0x0003DC4B
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

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0003FA68 File Offset: 0x0003DC68
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

	// Token: 0x06000A85 RID: 2693 RVA: 0x0003FC30 File Offset: 0x0003DE30
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

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
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

	// Token: 0x04000674 RID: 1652
	[HideInInspector]
	[SerializeField]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04000675 RID: 1653
	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	// Token: 0x04000676 RID: 1654
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x04000677 RID: 1655
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x04000678 RID: 1656
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x04000679 RID: 1657
	[NonSerialized]
	private int mPMA = -1;
}
