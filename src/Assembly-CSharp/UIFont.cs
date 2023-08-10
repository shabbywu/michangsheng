using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Font")]
public class UIFont : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	[HideInInspector]
	[SerializeField]
	private int mDynamicFontSize = 16;

	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	[NonSerialized]
	private UISpriteData mSprite;

	private int mPMA = -1;

	private int mPacked = -1;

	public BMFont bmFont
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mFont;
			}
			return mReplacement.bmFont;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.bmFont = value;
			}
			else
			{
				mFont = value;
			}
		}
	}

	public int texWidth
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				if (mFont == null)
				{
					return 1;
				}
				return mFont.texWidth;
			}
			return mReplacement.texWidth;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.texWidth = value;
			}
			else if (mFont != null)
			{
				mFont.texWidth = value;
			}
		}
	}

	public int texHeight
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				if (mFont == null)
				{
					return 1;
				}
				return mFont.texHeight;
			}
			return mReplacement.texHeight;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.texHeight = value;
			}
			else if (mFont != null)
			{
				mFont.texHeight = value;
			}
		}
	}

	public bool hasSymbols
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				if (mSymbols != null)
				{
					return mSymbols.Count != 0;
				}
				return false;
			}
			return mReplacement.hasSymbols;
		}
	}

	public List<BMSymbol> symbols
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mSymbols;
			}
			return mReplacement.symbols;
		}
	}

	public UIAtlas atlas
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mAtlas;
			}
			return mReplacement.atlas;
		}
		set
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.atlas = value;
			}
			else
			{
				if (!((Object)(object)mAtlas != (Object)(object)value))
				{
					return;
				}
				if ((Object)(object)value == (Object)null)
				{
					if ((Object)(object)mAtlas != (Object)null)
					{
						mMat = mAtlas.spriteMaterial;
					}
					if (sprite != null)
					{
						mUVRect = uvRect;
					}
				}
				mPMA = -1;
				mAtlas = value;
				MarkAsChanged();
			}
		}
	}

	public Material material
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.material;
			}
			if ((Object)(object)mAtlas != (Object)null)
			{
				return mAtlas.spriteMaterial;
			}
			if ((Object)(object)mMat != (Object)null)
			{
				if ((Object)(object)mDynamicFont != (Object)null && (Object)(object)mMat != (Object)(object)mDynamicFont.material)
				{
					mMat.mainTexture = mDynamicFont.material.mainTexture;
				}
				return mMat;
			}
			if ((Object)(object)mDynamicFont != (Object)null)
			{
				return mDynamicFont.material;
			}
			return null;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.material = value;
			}
			else if ((Object)(object)mMat != (Object)(object)value)
			{
				mPMA = -1;
				mMat = value;
				MarkAsChanged();
			}
		}
	}

	public bool premultipliedAlphaShader
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.premultipliedAlphaShader;
			}
			if ((Object)(object)mAtlas != (Object)null)
			{
				return mAtlas.premultipliedAlpha;
			}
			if (mPMA == -1)
			{
				Material val = material;
				mPMA = (((Object)(object)val != (Object)null && (Object)(object)val.shader != (Object)null && ((Object)val.shader).name.Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public bool packedFontShader
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.packedFontShader;
			}
			if ((Object)(object)mAtlas != (Object)null)
			{
				return false;
			}
			if (mPacked == -1)
			{
				Material val = material;
				mPacked = (((Object)(object)val != (Object)null && (Object)(object)val.shader != (Object)null && ((Object)val.shader).name.Contains("Packed")) ? 1 : 0);
			}
			return mPacked == 1;
		}
	}

	public Texture2D texture
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.texture;
			}
			Material val = material;
			if (!((Object)(object)val != (Object)null))
			{
				return null;
			}
			Texture mainTexture = val.mainTexture;
			return (Texture2D)(object)((mainTexture is Texture2D) ? mainTexture : null);
		}
	}

	public Rect uvRect
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.uvRect;
			}
			if (!((Object)(object)mAtlas != (Object)null) || sprite == null)
			{
				return new Rect(0f, 0f, 1f, 1f);
			}
			return mUVRect;
		}
		set
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.uvRect = value;
			}
			else if (sprite == null && mUVRect != value)
			{
				mUVRect = value;
				MarkAsChanged();
			}
		}
	}

	public string spriteName
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mFont.spriteName;
			}
			return mReplacement.spriteName;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.spriteName = value;
			}
			else if (mFont.spriteName != value)
			{
				mFont.spriteName = value;
				MarkAsChanged();
			}
		}
	}

	public bool isValid
	{
		get
		{
			if (!((Object)(object)mDynamicFont != (Object)null))
			{
				return mFont.isValid;
			}
			return true;
		}
	}

	[Obsolete("Use UIFont.defaultSize instead")]
	public int size
	{
		get
		{
			return defaultSize;
		}
		set
		{
			defaultSize = value;
		}
	}

	public int defaultSize
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.defaultSize;
			}
			if (isDynamic || mFont == null)
			{
				return mDynamicFontSize;
			}
			return mFont.charSize;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.defaultSize = value;
			}
			else
			{
				mDynamicFontSize = value;
			}
		}
	}

	public UISpriteData sprite
	{
		get
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				return mReplacement.sprite;
			}
			if (mSprite == null && (Object)(object)mAtlas != (Object)null && !string.IsNullOrEmpty(mFont.spriteName))
			{
				mSprite = mAtlas.GetSprite(mFont.spriteName);
				if (mSprite == null)
				{
					mSprite = mAtlas.GetSprite(((Object)this).name);
				}
				if (mSprite == null)
				{
					mFont.spriteName = null;
				}
				else
				{
					UpdateUVRect();
				}
				int i = 0;
				for (int count = mSymbols.Count; i < count; i++)
				{
					symbols[i].MarkAsChanged();
				}
			}
			return mSprite;
		}
	}

	public UIFont replacement
	{
		get
		{
			return mReplacement;
		}
		set
		{
			UIFont uIFont = value;
			if ((Object)(object)uIFont == (Object)(object)this)
			{
				uIFont = null;
			}
			if ((Object)(object)mReplacement != (Object)(object)uIFont)
			{
				if ((Object)(object)uIFont != (Object)null && (Object)(object)uIFont.replacement == (Object)(object)this)
				{
					uIFont.replacement = null;
				}
				if ((Object)(object)mReplacement != (Object)null)
				{
					MarkAsChanged();
				}
				mReplacement = uIFont;
				if ((Object)(object)uIFont != (Object)null)
				{
					mPMA = -1;
					mMat = null;
					mFont = null;
					mDynamicFont = null;
				}
				MarkAsChanged();
			}
		}
	}

	public bool isDynamic
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return (Object)(object)mDynamicFont != (Object)null;
			}
			return mReplacement.isDynamic;
		}
	}

	public Font dynamicFont
	{
		get
		{
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mDynamicFont;
			}
			return mReplacement.dynamicFont;
		}
		set
		{
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.dynamicFont = value;
			}
			else if ((Object)(object)mDynamicFont != (Object)(object)value)
			{
				if ((Object)(object)mDynamicFont != (Object)null)
				{
					material = null;
				}
				mDynamicFont = value;
				MarkAsChanged();
			}
		}
	}

	public FontStyle dynamicFontStyle
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)mReplacement != (Object)null))
			{
				return mDynamicFontStyle;
			}
			return mReplacement.dynamicFontStyle;
		}
		set
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)mReplacement != (Object)null)
			{
				mReplacement.dynamicFontStyle = value;
			}
			else if (mDynamicFontStyle != value)
			{
				mDynamicFontStyle = value;
				MarkAsChanged();
			}
		}
	}

	private Texture dynamicTexture
	{
		get
		{
			if (Object.op_Implicit((Object)(object)mReplacement))
			{
				return mReplacement.dynamicTexture;
			}
			if (isDynamic)
			{
				return mDynamicFont.material.mainTexture;
			}
			return null;
		}
	}

	private void Trim()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mAtlas.texture != (Object)null && mSprite != null)
		{
			Rect val = NGUIMath.ConvertToPixels(mUVRect, ((Texture)texture).width, ((Texture)texture).height, round: true);
			Rect val2 = default(Rect);
			((Rect)(ref val2))._002Ector((float)mSprite.x, (float)mSprite.y, (float)mSprite.width, (float)mSprite.height);
			int xMin = Mathf.RoundToInt(((Rect)(ref val2)).xMin - ((Rect)(ref val)).xMin);
			int yMin = Mathf.RoundToInt(((Rect)(ref val2)).yMin - ((Rect)(ref val)).yMin);
			int xMax = Mathf.RoundToInt(((Rect)(ref val2)).xMax - ((Rect)(ref val)).xMin);
			int yMax = Mathf.RoundToInt(((Rect)(ref val2)).yMax - ((Rect)(ref val)).yMin);
			mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	private bool References(UIFont font)
	{
		if ((Object)(object)font == (Object)null)
		{
			return false;
		}
		if ((Object)(object)font == (Object)(object)this)
		{
			return true;
		}
		if (!((Object)(object)mReplacement != (Object)null))
		{
			return false;
		}
		return mReplacement.References(font);
	}

	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		if ((Object)(object)a == (Object)null || (Object)(object)b == (Object)null)
		{
			return false;
		}
		if (a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0])
		{
			return true;
		}
		if (!((Object)(object)a == (Object)(object)b) && !a.References(b))
		{
			return b.References(a);
		}
		return true;
	}

	public void MarkAsChanged()
	{
		if ((Object)(object)mReplacement != (Object)null)
		{
			mReplacement.MarkAsChanged();
		}
		mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			UILabel uILabel = array[i];
			if (((Behaviour)uILabel).enabled && NGUITools.GetActive(((Component)uILabel).gameObject) && CheckIfRelated(this, uILabel.bitmapFont))
			{
				UIFont bitmapFont = uILabel.bitmapFont;
				uILabel.bitmapFont = null;
				uILabel.bitmapFont = bitmapFont;
			}
		}
		int j = 0;
		for (int count = symbols.Count; j < count; j++)
		{
			symbols[j].MarkAsChanged();
		}
	}

	public void UpdateUVRect()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mAtlas == (Object)null)
		{
			return;
		}
		Texture val = mAtlas.texture;
		if ((Object)(object)val != (Object)null)
		{
			mUVRect = new Rect((float)(mSprite.x - mSprite.paddingLeft), (float)(mSprite.y - mSprite.paddingTop), (float)(mSprite.width + mSprite.paddingLeft + mSprite.paddingRight), (float)(mSprite.height + mSprite.paddingTop + mSprite.paddingBottom));
			mUVRect = NGUIMath.ConvertToTexCoords(mUVRect, val.width, val.height);
			if (mSprite.hasPadding)
			{
				Trim();
			}
		}
	}

	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		for (int count = mSymbols.Count; i < count; i++)
		{
			BMSymbol bMSymbol = mSymbols[i];
			if (bMSymbol.sequence == sequence)
			{
				return bMSymbol;
			}
		}
		if (createIfMissing)
		{
			BMSymbol bMSymbol2 = new BMSymbol();
			bMSymbol2.sequence = sequence;
			mSymbols.Add(bMSymbol2);
			return bMSymbol2;
		}
		return null;
	}

	public BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		int count = mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bMSymbol = mSymbols[i];
			int length = bMSymbol.length;
			if (length == 0 || textLength < length)
			{
				continue;
			}
			bool flag = true;
			for (int j = 0; j < length; j++)
			{
				if (text[offset + j] != bMSymbol.sequence[j])
				{
					flag = false;
					break;
				}
			}
			if (flag && bMSymbol.Validate(atlas))
			{
				return bMSymbol;
			}
		}
		return null;
	}

	public void AddSymbol(string sequence, string spriteName)
	{
		GetSymbol(sequence, createIfMissing: true).spriteName = spriteName;
		MarkAsChanged();
	}

	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = GetSymbol(sequence, createIfMissing: false);
		if (symbol != null)
		{
			symbols.Remove(symbol);
		}
		MarkAsChanged();
	}

	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = GetSymbol(before, createIfMissing: false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		MarkAsChanged();
	}

	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(spriteName))
			{
				return true;
			}
			int i = 0;
			for (int count = symbols.Count; i < count; i++)
			{
				BMSymbol bMSymbol = symbols[i];
				if (s.Equals(bMSymbol.spriteName))
				{
					return true;
				}
			}
		}
		return false;
	}
}
