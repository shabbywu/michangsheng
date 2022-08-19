using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[Serializable]
public class BMSymbol
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000646 RID: 1606 RVA: 0x0002384B File Offset: 0x00021A4B
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

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000647 RID: 1607 RVA: 0x0002386C File Offset: 0x00021A6C
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x00023874 File Offset: 0x00021A74
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000649 RID: 1609 RVA: 0x0002387C File Offset: 0x00021A7C
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600064A RID: 1610 RVA: 0x00023884 File Offset: 0x00021A84
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x0002388C File Offset: 0x00021A8C
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600064C RID: 1612 RVA: 0x00023894 File Offset: 0x00021A94
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0002389C File Offset: 0x00021A9C
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x000238A8 File Offset: 0x00021AA8
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

	// Token: 0x04000439 RID: 1081
	public string sequence;

	// Token: 0x0400043A RID: 1082
	public string spriteName;

	// Token: 0x0400043B RID: 1083
	private UISpriteData mSprite;

	// Token: 0x0400043C RID: 1084
	private bool mIsValid;

	// Token: 0x0400043D RID: 1085
	private int mLength;

	// Token: 0x0400043E RID: 1086
	private int mOffsetX;

	// Token: 0x0400043F RID: 1087
	private int mOffsetY;

	// Token: 0x04000440 RID: 1088
	private int mWidth;

	// Token: 0x04000441 RID: 1089
	private int mHeight;

	// Token: 0x04000442 RID: 1090
	private int mAdvance;

	// Token: 0x04000443 RID: 1091
	private Rect mUV;
}
