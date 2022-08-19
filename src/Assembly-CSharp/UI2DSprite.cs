using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
public class UI2DSprite : UIBasicSprite
{
	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000335F8 File Offset: 0x000317F8
	// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00033600 File Offset: 0x00031800
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

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0003362A File Offset: 0x0003182A
	// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00033632 File Offset: 0x00031832
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

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0003365C File Offset: 0x0003185C
	// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0003369C File Offset: 0x0003189C
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

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000336D4 File Offset: 0x000318D4
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

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0003370C File Offset: 0x0003190C
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

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00033758 File Offset: 0x00031958
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

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060008CA RID: 2250 RVA: 0x00033A07 File Offset: 0x00031C07
	// (set) Token: 0x060008CB RID: 2251 RVA: 0x00033A0F File Offset: 0x00031C0F
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

	// Token: 0x060008CC RID: 2252 RVA: 0x00033A2C File Offset: 0x00031C2C
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

	// Token: 0x060008CD RID: 2253 RVA: 0x00033A68 File Offset: 0x00031C68
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

	// Token: 0x060008CE RID: 2254 RVA: 0x00033B00 File Offset: 0x00031D00
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

	// Token: 0x0400055A RID: 1370
	[HideInInspector]
	[SerializeField]
	private Sprite mSprite;

	// Token: 0x0400055B RID: 1371
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400055C RID: 1372
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x0400055D RID: 1373
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x0400055E RID: 1374
	public Sprite nextSprite;

	// Token: 0x0400055F RID: 1375
	[NonSerialized]
	private int mPMA = -1;
}
