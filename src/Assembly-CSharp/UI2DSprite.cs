using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
public class UI2DSprite : UIBasicSprite
{
	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x0000BC22 File Offset: 0x00009E22
	// (set) Token: 0x0600097A RID: 2426 RVA: 0x0000BC2A File Offset: 0x00009E2A
	public Sprite sprite2D
	{
		get
		{
			return this.mSprite;
		}
		set
		{
			if (this.mSprite != value)
			{
				base.RemoveFromPanel();
				this.mSprite = value;
				this.nextSprite = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000BC54 File Offset: 0x00009E54
	// (set) Token: 0x0600097C RID: 2428 RVA: 0x0000BC5C File Offset: 0x00009E5C
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
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x0600097D RID: 2429 RVA: 0x0000BC86 File Offset: 0x00009E86
	// (set) Token: 0x0600097E RID: 2430 RVA: 0x0000BCC6 File Offset: 0x00009EC6
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
				if (this.mMat == null)
				{
					this.mPMA = -1;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x0600097F RID: 2431 RVA: 0x0000BCFE File Offset: 0x00009EFE
	public override Texture mainTexture
	{
		get
		{
			if (this.mSprite != null)
			{
				return this.mSprite.texture;
			}
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000980 RID: 2432 RVA: 0x000873E4 File Offset: 0x000855E4
	public override bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Shader shader = this.shader;
				this.mPMA = ((shader != null && shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000981 RID: 2433 RVA: 0x00087430 File Offset: 0x00085630
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.mSprite != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int num5 = Mathf.RoundToInt(this.mSprite.rect.width);
				int num6 = Mathf.RoundToInt(this.mSprite.rect.height);
				int num7 = Mathf.RoundToInt(this.mSprite.textureRectOffset.x);
				int num8 = Mathf.RoundToInt(this.mSprite.textureRectOffset.y);
				int num9 = Mathf.RoundToInt(this.mSprite.rect.width - this.mSprite.textureRect.width - this.mSprite.textureRectOffset.x);
				int num10 = Mathf.RoundToInt(this.mSprite.rect.height - this.mSprite.textureRect.height - this.mSprite.textureRectOffset.y);
				float num11 = 1f;
				float num12 = 1f;
				if (num5 > 0 && num6 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((num5 & 1) != 0)
					{
						num9++;
					}
					if ((num6 & 1) != 0)
					{
						num10++;
					}
					num11 = 1f / (float)num5 * (float)this.mWidth;
					num12 = 1f / (float)num6 * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num9 * num11;
					num3 -= (float)num7 * num11;
				}
				else
				{
					num += (float)num7 * num11;
					num3 -= (float)num9 * num11;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num10 * num12;
					num4 -= (float)num8 * num12;
				}
				else
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num10 * num12;
				}
			}
			Vector4 border = this.border;
			float num13 = border.x + border.z;
			float num14 = border.y + border.w;
			float num15 = Mathf.Lerp(num, num3 - num13, this.mDrawRegion.x);
			float num16 = Mathf.Lerp(num2, num4 - num14, this.mDrawRegion.y);
			float num17 = Mathf.Lerp(num + num13, num3, this.mDrawRegion.z);
			float num18 = Mathf.Lerp(num2 + num14, num4, this.mDrawRegion.w);
			return new Vector4(num15, num16, num17, num18);
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000982 RID: 2434 RVA: 0x0000BD35 File Offset: 0x00009F35
	// (set) Token: 0x06000983 RID: 2435 RVA: 0x0000BD3D File Offset: 0x00009F3D
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

	// Token: 0x06000984 RID: 2436 RVA: 0x0000BD5A File Offset: 0x00009F5A
	protected override void OnUpdate()
	{
		if (this.nextSprite != null)
		{
			if (this.nextSprite != this.mSprite)
			{
				this.sprite2D = this.nextSprite;
			}
			this.nextSprite = null;
		}
		base.OnUpdate();
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x000876E0 File Offset: 0x000858E0
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
			Rect rect = this.mSprite.rect;
			int num = Mathf.RoundToInt(rect.width);
			int num2 = Mathf.RoundToInt(rect.height);
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

	// Token: 0x06000986 RID: 2438 RVA: 0x00087778 File Offset: 0x00085978
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Rect textureRect = this.mSprite.textureRect;
		Rect inner = textureRect;
		Vector4 border = this.border;
		inner.xMin += border.x;
		inner.yMin += border.y;
		inner.xMax -= border.z;
		inner.yMax -= border.w;
		float num = 1f / (float)mainTexture.width;
		float num2 = 1f / (float)mainTexture.height;
		textureRect.xMin *= num;
		textureRect.xMax *= num;
		textureRect.yMin *= num2;
		textureRect.yMax *= num2;
		inner.xMin *= num;
		inner.xMax *= num;
		inner.yMin *= num2;
		inner.yMax *= num2;
		int size = verts.size;
		base.Fill(verts, uvs, cols, textureRect, inner);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, size, verts, uvs, cols);
		}
	}

	// Token: 0x04000688 RID: 1672
	[HideInInspector]
	[SerializeField]
	private Sprite mSprite;

	// Token: 0x04000689 RID: 1673
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400068A RID: 1674
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x0400068B RID: 1675
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x0400068C RID: 1676
	public Sprite nextSprite;

	// Token: 0x0400068D RID: 1677
	[NonSerialized]
	private int mPMA = -1;
}
