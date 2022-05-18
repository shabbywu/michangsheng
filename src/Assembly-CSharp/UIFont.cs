using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000107 RID: 263
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Font")]
public class UIFont : MonoBehaviour
{
	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0000C21D File Offset: 0x0000A41D
	// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0000C23F File Offset: 0x0000A43F
	public BMFont bmFont
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mFont;
			}
			return this.mReplacement.bmFont;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.bmFont = value;
				return;
			}
			this.mFont = value;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0000C263 File Offset: 0x0000A463
	// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0000C294 File Offset: 0x0000A494
	public int texWidth
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texWidth;
			}
			if (this.mFont == null)
			{
				return 1;
			}
			return this.mFont.texWidth;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.texWidth = value;
				return;
			}
			if (this.mFont != null)
			{
				this.mFont.texWidth = value;
			}
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x060009EA RID: 2538 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
	// (set) Token: 0x060009EB RID: 2539 RVA: 0x0000C2F6 File Offset: 0x0000A4F6
	public int texHeight
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texHeight;
			}
			if (this.mFont == null)
			{
				return 1;
			}
			return this.mFont.texHeight;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.texHeight = value;
				return;
			}
			if (this.mFont != null)
			{
				this.mFont.texHeight = value;
			}
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060009EC RID: 2540 RVA: 0x0000C327 File Offset: 0x0000A527
	public bool hasSymbols
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mSymbols != null && this.mSymbols.Count != 0;
			}
			return this.mReplacement.hasSymbols;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060009ED RID: 2541 RVA: 0x0000C35B File Offset: 0x0000A55B
	public List<BMSymbol> symbols
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mSymbols;
			}
			return this.mReplacement.symbols;
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060009EE RID: 2542 RVA: 0x0000C37D File Offset: 0x0000A57D
	// (set) Token: 0x060009EF RID: 2543 RVA: 0x0008A9CC File Offset: 0x00088BCC
	public UIAtlas atlas
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mAtlas;
			}
			return this.mReplacement.atlas;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.atlas = value;
				return;
			}
			if (this.mAtlas != value)
			{
				if (value == null)
				{
					if (this.mAtlas != null)
					{
						this.mMat = this.mAtlas.spriteMaterial;
					}
					if (this.sprite != null)
					{
						this.mUVRect = this.uvRect;
					}
				}
				this.mPMA = -1;
				this.mAtlas = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0008AA54 File Offset: 0x00088C54
	// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0000C39F File Offset: 0x0000A59F
	public Material material
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.material;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.spriteMaterial;
			}
			if (this.mMat != null)
			{
				if (this.mDynamicFont != null && this.mMat != this.mDynamicFont.material)
				{
					this.mMat.mainTexture = this.mDynamicFont.material.mainTexture;
				}
				return this.mMat;
			}
			if (this.mDynamicFont != null)
			{
				return this.mDynamicFont.material;
			}
			return null;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.material = value;
				return;
			}
			if (this.mMat != value)
			{
				this.mPMA = -1;
				this.mMat = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0008AB08 File Offset: 0x00088D08
	public bool premultipliedAlphaShader
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlphaShader;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0008AB9C File Offset: 0x00088D9C
	public bool packedFontShader
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.packedFontShader;
			}
			if (this.mAtlas != null)
			{
				return false;
			}
			if (this.mPacked == -1)
			{
				Material material = this.material;
				this.mPacked = ((material != null && material.shader != null && material.shader.name.Contains("Packed")) ? 1 : 0);
			}
			return this.mPacked == 1;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0008AC24 File Offset: 0x00088E24
	public Texture2D texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			Material material = this.material;
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture as Texture2D;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0008AC68 File Offset: 0x00088E68
	// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0000C3DE File Offset: 0x0000A5DE
	public Rect uvRect
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.uvRect;
			}
			if (!(this.mAtlas != null) || this.sprite == null)
			{
				return new Rect(0f, 0f, 1f, 1f);
			}
			return this.mUVRect;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.uvRect = value;
				return;
			}
			if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0000C41E File Offset: 0x0000A61E
	// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0008ACC8 File Offset: 0x00088EC8
	public string spriteName
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mFont.spriteName;
			}
			return this.mReplacement.spriteName;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteName = value;
				return;
			}
			if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000C445 File Offset: 0x0000A645
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x060009FA RID: 2554 RVA: 0x0000C462 File Offset: 0x0000A662
	// (set) Token: 0x060009FB RID: 2555 RVA: 0x0000C46A File Offset: 0x0000A66A
	[Obsolete("Use UIFont.defaultSize instead")]
	public int size
	{
		get
		{
			return this.defaultSize;
		}
		set
		{
			this.defaultSize = value;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x0000C473 File Offset: 0x0000A673
	// (set) Token: 0x060009FD RID: 2557 RVA: 0x0000C4B1 File Offset: 0x0000A6B1
	public int defaultSize
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.defaultSize;
			}
			if (this.isDynamic || this.mFont == null)
			{
				return this.mDynamicFontSize;
			}
			return this.mFont.charSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.defaultSize = value;
				return;
			}
			this.mDynamicFontSize = value;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x0008AD18 File Offset: 0x00088F18
	public UISpriteData sprite
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.sprite;
			}
			if (this.mSprite == null && this.mAtlas != null && !string.IsNullOrEmpty(this.mFont.spriteName))
			{
				this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
				if (this.mSprite == null)
				{
					this.mSprite = this.mAtlas.GetSprite(base.name);
				}
				if (this.mSprite == null)
				{
					this.mFont.spriteName = null;
				}
				else
				{
					this.UpdateUVRect();
				}
				int i = 0;
				int count = this.mSymbols.Count;
				while (i < count)
				{
					this.symbols[i].MarkAsChanged();
					i++;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
	// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0008ADF8 File Offset: 0x00088FF8
	public UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIFont uifont = value;
			if (uifont == this)
			{
				uifont = null;
			}
			if (this.mReplacement != uifont)
			{
				if (uifont != null && uifont.replacement == this)
				{
					uifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsChanged();
				}
				this.mReplacement = uifont;
				if (uifont != null)
				{
					this.mPMA = -1;
					this.mMat = null;
					this.mFont = null;
					this.mDynamicFont = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000C4DD File Offset: 0x0000A6DD
	public bool isDynamic
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mDynamicFont != null;
			}
			return this.mReplacement.isDynamic;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0000C505 File Offset: 0x0000A705
	// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0008AE84 File Offset: 0x00089084
	public Font dynamicFont
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mDynamicFont;
			}
			return this.mReplacement.dynamicFont;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFont = value;
				return;
			}
			if (this.mDynamicFont != value)
			{
				if (this.mDynamicFont != null)
				{
					this.material = null;
				}
				this.mDynamicFont = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0000C527 File Offset: 0x0000A727
	// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0000C549 File Offset: 0x0000A749
	public FontStyle dynamicFontStyle
	{
		get
		{
			if (!(this.mReplacement != null))
			{
				return this.mDynamicFontStyle;
			}
			return this.mReplacement.dynamicFontStyle;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFontStyle = value;
				return;
			}
			if (this.mDynamicFontStyle != value)
			{
				this.mDynamicFontStyle = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0008AEDC File Offset: 0x000890DC
	private void Trim()
	{
		if (this.mAtlas.texture != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2;
			rect2..ctor((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0000C57C File Offset: 0x0000A77C
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0008AFCC File Offset: 0x000891CC
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && ((a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0]) || a == b || a.References(b) || b.References(a));
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
	private Texture dynamicTexture
	{
		get
		{
			if (this.mReplacement)
			{
				return this.mReplacement.dynamicTexture;
			}
			if (this.isDynamic)
			{
				return this.mDynamicFont.material.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0008B03C File Offset: 0x0008923C
	public void MarkAsChanged()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsChanged();
		}
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && NGUITools.GetActive(uilabel.gameObject) && UIFont.CheckIfRelated(this, uilabel.bitmapFont))
			{
				UIFont bitmapFont = uilabel.bitmapFont;
				uilabel.bitmapFont = null;
				uilabel.bitmapFont = bitmapFont;
			}
			i++;
		}
		int j = 0;
		int count = this.symbols.Count;
		while (j < count)
		{
			this.symbols[j].MarkAsChanged();
			j++;
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0008B0F0 File Offset: 0x000892F0
	public void UpdateUVRect()
	{
		if (this.mAtlas == null)
		{
			return;
		}
		Texture texture = this.mAtlas.texture;
		if (texture != null)
		{
			this.mUVRect = new Rect((float)(this.mSprite.x - this.mSprite.paddingLeft), (float)(this.mSprite.y - this.mSprite.paddingTop), (float)(this.mSprite.width + this.mSprite.paddingLeft + this.mSprite.paddingRight), (float)(this.mSprite.height + this.mSprite.paddingTop + this.mSprite.paddingBottom));
			this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
			if (this.mSprite.hasPadding)
			{
				this.Trim();
			}
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0008B1D8 File Offset: 0x000893D8
	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			BMSymbol bmsymbol2 = new BMSymbol();
			bmsymbol2.sequence = sequence;
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0008B23C File Offset: 0x0008943C
	public BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		int count = this.mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			int length = bmsymbol.length;
			if (length != 0 && textLength >= length)
			{
				bool flag = true;
				for (int j = 0; j < length; j++)
				{
					if (text[offset + j] != bmsymbol.sequence[j])
					{
						flag = false;
						break;
					}
				}
				if (flag && bmsymbol.Validate(this.atlas))
				{
					return bmsymbol;
				}
			}
		}
		return null;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
	public void AddSymbol(string sequence, string spriteName)
	{
		this.GetSymbol(sequence, true).spriteName = spriteName;
		this.MarkAsChanged();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0008B2CC File Offset: 0x000894CC
	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0008B2F8 File Offset: 0x000894F8
	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0008B320 File Offset: 0x00089520
	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(this.spriteName))
			{
				return true;
			}
			int i = 0;
			int count = this.symbols.Count;
			while (i < count)
			{
				BMSymbol bmsymbol = this.symbols[i];
				if (s.Equals(bmsymbol.spriteName))
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0400071F RID: 1823
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x04000720 RID: 1824
	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04000721 RID: 1825
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x04000722 RID: 1826
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x04000723 RID: 1827
	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	// Token: 0x04000724 RID: 1828
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x04000725 RID: 1829
	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	// Token: 0x04000726 RID: 1830
	[HideInInspector]
	[SerializeField]
	private int mDynamicFontSize = 16;

	// Token: 0x04000727 RID: 1831
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x04000728 RID: 1832
	[NonSerialized]
	private UISpriteData mSprite;

	// Token: 0x04000729 RID: 1833
	private int mPMA = -1;

	// Token: 0x0400072A RID: 1834
	private int mPacked = -1;
}
