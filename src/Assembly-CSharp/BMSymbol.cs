using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[Serializable]
public class BMSymbol
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00009D70 File Offset: 0x00007F70
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00009D91 File Offset: 0x00007F91
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00009D99 File Offset: 0x00007F99
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00009DA1 File Offset: 0x00007FA1
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00009DA9 File Offset: 0x00007FA9
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00009DB1 File Offset: 0x00007FB1
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00009DB9 File Offset: 0x00007FB9
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00009DC1 File Offset: 0x00007FC1
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x000791C0 File Offset: 0x000773C0
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((atlas != null) ? atlas.GetSprite(this.spriteName) : null);
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x0400050D RID: 1293
	public string sequence;

	// Token: 0x0400050E RID: 1294
	public string spriteName;

	// Token: 0x0400050F RID: 1295
	private UISpriteData mSprite;

	// Token: 0x04000510 RID: 1296
	private bool mIsValid;

	// Token: 0x04000511 RID: 1297
	private int mLength;

	// Token: 0x04000512 RID: 1298
	private int mOffsetX;

	// Token: 0x04000513 RID: 1299
	private int mOffsetY;

	// Token: 0x04000514 RID: 1300
	private int mWidth;

	// Token: 0x04000515 RID: 1301
	private int mHeight;

	// Token: 0x04000516 RID: 1302
	private int mAdvance;

	// Token: 0x04000517 RID: 1303
	private Rect mUV;
}
