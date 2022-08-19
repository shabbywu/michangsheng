using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Font")]
public class UIFont : MonoBehaviour
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000916 RID: 2326 RVA: 0x0003700F File Offset: 0x0003520F
	// (set) Token: 0x06000917 RID: 2327 RVA: 0x00037031 File Offset: 0x00035231
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

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000918 RID: 2328 RVA: 0x00037055 File Offset: 0x00035255
	// (set) Token: 0x06000919 RID: 2329 RVA: 0x00037086 File Offset: 0x00035286
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

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x0600091A RID: 2330 RVA: 0x000370B7 File Offset: 0x000352B7
	// (set) Token: 0x0600091B RID: 2331 RVA: 0x000370E8 File Offset: 0x000352E8
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

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x0600091C RID: 2332 RVA: 0x00037119 File Offset: 0x00035319
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

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600091D RID: 2333 RVA: 0x0003714D File Offset: 0x0003534D
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

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600091E RID: 2334 RVA: 0x0003716F File Offset: 0x0003536F
	// (set) Token: 0x0600091F RID: 2335 RVA: 0x00037194 File Offset: 0x00035394
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

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000920 RID: 2336 RVA: 0x0003721C File Offset: 0x0003541C
	// (set) Token: 0x06000921 RID: 2337 RVA: 0x000372CE File Offset: 0x000354CE
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

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000922 RID: 2338 RVA: 0x00037310 File Offset: 0x00035510
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

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000923 RID: 2339 RVA: 0x000373A4 File Offset: 0x000355A4
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

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000924 RID: 2340 RVA: 0x0003742C File Offset: 0x0003562C
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

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x00037470 File Offset: 0x00035670
	// (set) Token: 0x06000926 RID: 2342 RVA: 0x000374CD File Offset: 0x000356CD
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

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x0003750D File Offset: 0x0003570D
	// (set) Token: 0x06000928 RID: 2344 RVA: 0x00037534 File Offset: 0x00035734
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

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000929 RID: 2345 RVA: 0x00037581 File Offset: 0x00035781
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x0600092A RID: 2346 RVA: 0x0003759E File Offset: 0x0003579E
	// (set) Token: 0x0600092B RID: 2347 RVA: 0x000375A6 File Offset: 0x000357A6
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

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x0600092C RID: 2348 RVA: 0x000375AF File Offset: 0x000357AF
	// (set) Token: 0x0600092D RID: 2349 RVA: 0x000375ED File Offset: 0x000357ED
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

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x0600092E RID: 2350 RVA: 0x00037614 File Offset: 0x00035814
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

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x0600092F RID: 2351 RVA: 0x000376F2 File Offset: 0x000358F2
	// (set) Token: 0x06000930 RID: 2352 RVA: 0x000376FC File Offset: 0x000358FC
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

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000931 RID: 2353 RVA: 0x00037788 File Offset: 0x00035988
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

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06000932 RID: 2354 RVA: 0x000377B0 File Offset: 0x000359B0
	// (set) Token: 0x06000933 RID: 2355 RVA: 0x000377D4 File Offset: 0x000359D4
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

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000934 RID: 2356 RVA: 0x0003782C File Offset: 0x00035A2C
	// (set) Token: 0x06000935 RID: 2357 RVA: 0x0003784E File Offset: 0x00035A4E
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

	// Token: 0x06000936 RID: 2358 RVA: 0x00037884 File Offset: 0x00035A84
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

	// Token: 0x06000937 RID: 2359 RVA: 0x00037973 File Offset: 0x00035B73
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x000379A8 File Offset: 0x00035BA8
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && ((a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0]) || a == b || a.References(b) || b.References(a));
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06000939 RID: 2361 RVA: 0x00037A17 File Offset: 0x00035C17
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

	// Token: 0x0600093A RID: 2362 RVA: 0x00037A4C File Offset: 0x00035C4C
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

	// Token: 0x0600093B RID: 2363 RVA: 0x00037B00 File Offset: 0x00035D00
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

	// Token: 0x0600093C RID: 2364 RVA: 0x00037BE8 File Offset: 0x00035DE8
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

	// Token: 0x0600093D RID: 2365 RVA: 0x00037C4C File Offset: 0x00035E4C
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

	// Token: 0x0600093E RID: 2366 RVA: 0x00037CDC File Offset: 0x00035EDC
	public void AddSymbol(string sequence, string spriteName)
	{
		this.GetSymbol(sequence, true).spriteName = spriteName;
		this.MarkAsChanged();
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00037CF4 File Offset: 0x00035EF4
	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00037D20 File Offset: 0x00035F20
	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsChanged();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00037D48 File Offset: 0x00035F48
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

	// Token: 0x040005B5 RID: 1461
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x040005B6 RID: 1462
	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040005B7 RID: 1463
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x040005B8 RID: 1464
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x040005B9 RID: 1465
	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	// Token: 0x040005BA RID: 1466
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x040005BB RID: 1467
	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	// Token: 0x040005BC RID: 1468
	[HideInInspector]
	[SerializeField]
	private int mDynamicFontSize = 16;

	// Token: 0x040005BD RID: 1469
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x040005BE RID: 1470
	[NonSerialized]
	private UISpriteData mSprite;

	// Token: 0x040005BF RID: 1471
	private int mPMA = -1;

	// Token: 0x040005C0 RID: 1472
	private int mPacked = -1;
}
