using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Sprite")]
public class UISprite : UIBasicSprite
{
	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0000D219 File Offset: 0x0000B419
	public override Material material
	{
		get
		{
			if (!(this.mAtlas != null))
			{
				return null;
			}
			return this.mAtlas.spriteMaterial;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0000D236 File Offset: 0x0000B436
	// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0009076C File Offset: 0x0008E96C
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.SetAtlasSprite(this.mAtlas.spriteList[0]);
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = "";
					this.spriteName = spriteName;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0000D23E File Offset: 0x0000B43E
	// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00090828 File Offset: 0x0008EA28
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (this.mSpriteName != value)
				{
					this.mSpriteName = value;
					this.mSprite = null;
					this.mChanged = true;
					this.mSpriteSet = false;
				}
				return;
			}
			if (string.IsNullOrEmpty(this.mSpriteName))
			{
				return;
			}
			this.mSpriteName = "";
			this.mSprite = null;
			this.mChanged = true;
			this.mSpriteSet = false;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0000D246 File Offset: 0x0000B446
	public bool isValid
	{
		get
		{
			return this.GetAtlasSprite() != null;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0000D251 File Offset: 0x0000B451
	// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0000D25C File Offset: 0x0000B45C
	[Obsolete("Use 'centerType' instead")]
	public bool fillCenter
	{
		get
		{
			return this.centerType > UIBasicSprite.AdvancedType.Invisible;
		}
		set
		{
			if (value != this.centerType > UIBasicSprite.AdvancedType.Invisible)
			{
				this.centerType = (value ? UIBasicSprite.AdvancedType.Sliced : UIBasicSprite.AdvancedType.Invisible);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00090898 File Offset: 0x0008EA98
	public override Vector4 border
	{
		get
		{
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return base.border;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0000D27D File Offset: 0x0000B47D
	public override float pixelSize
	{
		get
		{
			if (!(this.mAtlas != null))
			{
				return 1f;
			}
			return this.mAtlas.pixelSize;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000908D8 File Offset: 0x0008EAD8
	public override int minWidth
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.x + vector.z);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += atlasSprite.paddingLeft + atlasSprite.paddingRight;
				}
				return Mathf.Max(base.minWidth, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minWidth;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00090954 File Offset: 0x0008EB54
	public override int minHeight
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.y + vector.w);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += atlasSprite.paddingTop + atlasSprite.paddingBottom;
				}
				return Mathf.Max(base.minHeight, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minHeight;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000B21 RID: 2849 RVA: 0x000909D0 File Offset: 0x0008EBD0
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.GetAtlasSprite() != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int paddingLeft = this.mSprite.paddingLeft;
				int paddingBottom = this.mSprite.paddingBottom;
				int num5 = this.mSprite.paddingRight;
				int num6 = this.mSprite.paddingTop;
				int num7 = this.mSprite.width + paddingLeft + num5;
				int num8 = this.mSprite.height + paddingBottom + num6;
				float num9 = 1f;
				float num10 = 1f;
				if (num7 > 0 && num8 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((num7 & 1) != 0)
					{
						num5++;
					}
					if ((num8 & 1) != 0)
					{
						num6++;
					}
					num9 = 1f / (float)num7 * (float)this.mWidth;
					num10 = 1f / (float)num8 * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num5 * num9;
					num3 -= (float)paddingLeft * num9;
				}
				else
				{
					num += (float)paddingLeft * num9;
					num3 -= (float)num5 * num9;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num6 * num10;
					num4 -= (float)paddingBottom * num10;
				}
				else
				{
					num2 += (float)paddingBottom * num10;
					num4 -= (float)num6 * num10;
				}
			}
			Vector4 vector = (this.mAtlas != null) ? (this.border * this.pixelSize) : Vector4.zero;
			float num11 = vector.x + vector.z;
			float num12 = vector.y + vector.w;
			float num13 = Mathf.Lerp(num, num3 - num11, this.mDrawRegion.x);
			float num14 = Mathf.Lerp(num2, num4 - num12, this.mDrawRegion.y);
			float num15 = Mathf.Lerp(num + num11, num3, this.mDrawRegion.z);
			float num16 = Mathf.Lerp(num2 + num12, num4, this.mDrawRegion.w);
			return new Vector4(num13, num14, num15, num16);
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0000D29E File Offset: 0x0000B49E
	public override bool premultipliedAlpha
	{
		get
		{
			return this.mAtlas != null && this.mAtlas.premultipliedAlpha;
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00090C10 File Offset: 0x0008EE10
	public UISpriteData GetAtlasSprite()
	{
		if (!this.mSpriteSet)
		{
			this.mSprite = null;
		}
		if (this.mSprite == null && this.mAtlas != null)
		{
			if (!string.IsNullOrEmpty(this.mSpriteName))
			{
				UISpriteData sprite = this.mAtlas.GetSprite(this.mSpriteName);
				if (sprite == null)
				{
					return null;
				}
				this.SetAtlasSprite(sprite);
			}
			if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
			{
				UISpriteData uispriteData = this.mAtlas.spriteList[0];
				if (uispriteData == null)
				{
					return null;
				}
				this.SetAtlasSprite(uispriteData);
				if (this.mSprite == null)
				{
					Debug.LogError(this.mAtlas.name + " seems to have a null sprite!");
					return null;
				}
				this.mSpriteName = this.mSprite.name;
			}
		}
		return this.mSprite;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00090CE8 File Offset: 0x0008EEE8
	protected void SetAtlasSprite(UISpriteData sp)
	{
		this.mChanged = true;
		this.mSpriteSet = true;
		if (sp != null)
		{
			this.mSprite = sp;
			this.mSpriteName = this.mSprite.name;
			return;
		}
		this.mSpriteName = ((this.mSprite != null) ? this.mSprite.name : "");
		this.mSprite = sp;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x00090D48 File Offset: 0x0008EF48
	public override void MakePixelPerfect()
	{
		if (!this.isValid)
		{
			return;
		}
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		UISpriteData atlasSprite = this.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !atlasSprite.hasBorder) && mainTexture != null)
		{
			int num = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
			int num2 = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
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

	// Token: 0x06000B26 RID: 2854 RVA: 0x0000D2BB File Offset: 0x0000B4BB
	protected override void OnInit()
	{
		if (!this.mFillCenter)
		{
			this.mFillCenter = true;
			this.centerType = UIBasicSprite.AdvancedType.Invisible;
		}
		base.OnInit();
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0000D2D9 File Offset: 0x0000B4D9
	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (this.mChanged || !this.mSpriteSet)
		{
			this.mSpriteSet = true;
			this.mSprite = null;
			this.mChanged = true;
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00090E14 File Offset: 0x0008F014
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if (this.mSprite == null)
		{
			this.mSprite = this.atlas.GetSprite(this.spriteName);
		}
		if (this.mSprite == null)
		{
			return;
		}
		Rect rect;
		rect..ctor((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
		Rect rect2;
		rect2..ctor((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
		rect = NGUIMath.ConvertToTexCoords(rect, mainTexture.width, mainTexture.height);
		rect2 = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
		int size = verts.size;
		base.Fill(verts, uvs, cols, rect, rect2);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, size, verts, uvs, cols);
		}
	}

	// Token: 0x040007D6 RID: 2006
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x040007D7 RID: 2007
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x040007D8 RID: 2008
	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	// Token: 0x040007D9 RID: 2009
	[NonSerialized]
	protected UISpriteData mSprite;

	// Token: 0x040007DA RID: 2010
	[NonSerialized]
	private bool mSpriteSet;
}
