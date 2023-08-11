using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	public enum Effect
	{
		None,
		Shadow,
		Outline
	}

	public enum Overflow
	{
		ShrinkContent,
		ClampContent,
		ResizeFreely,
		ResizeHeight
	}

	public enum Crispness
	{
		Never,
		OnDesktop,
		Always
	}

	public Crispness keepCrispWhenShrunk = Crispness.OnDesktop;

	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = "";

	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	[HideInInspector]
	[SerializeField]
	private Effect mEffectStyle;

	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	[HideInInspector]
	[SerializeField]
	private Overflow mOverflow;

	[HideInInspector]
	[SerializeField]
	private Material mMaterial;

	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	[NonSerialized]
	private Font mActiveTTF;

	private float mDensity = 1f;

	private bool mShouldBeProcessed = true;

	private string mProcessedText;

	private bool mPremultiply;

	private Vector2 mCalculatedSize = Vector2.zero;

	private float mScale = 1f;

	private int mPrintedSize;

	private int mLastWidth;

	private int mLastHeight;

	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	private static BetterList<Vector3> mTempVerts = new BetterList<Vector3>();

	private static BetterList<int> mTempIndices = new BetterList<int>();

	private bool shouldBeProcessed
	{
		get
		{
			return mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				mChanged = true;
				mShouldBeProcessed = true;
			}
			else
			{
				mShouldBeProcessed = false;
			}
		}
	}

	public override bool isAnchoredHorizontally
	{
		get
		{
			if (!base.isAnchoredHorizontally)
			{
				return mOverflow == Overflow.ResizeFreely;
			}
			return true;
		}
	}

	public override bool isAnchoredVertically
	{
		get
		{
			if (!base.isAnchoredVertically && mOverflow != Overflow.ResizeFreely)
			{
				return mOverflow == Overflow.ResizeHeight;
			}
			return true;
		}
	}

	public override Material material
	{
		get
		{
			if ((Object)(object)mMaterial != (Object)null)
			{
				return mMaterial;
			}
			if ((Object)(object)mFont != (Object)null)
			{
				return mFont.material;
			}
			if ((Object)(object)mTrueTypeFont != (Object)null)
			{
				return mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			if ((Object)(object)mMaterial != (Object)(object)value)
			{
				MarkAsChanged();
				mMaterial = value;
				MarkAsChanged();
			}
		}
	}

	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return bitmapFont;
		}
		set
		{
			bitmapFont = value;
		}
	}

	public UIFont bitmapFont
	{
		get
		{
			return mFont;
		}
		set
		{
			if ((Object)(object)mFont != (Object)(object)value)
			{
				RemoveFromPanel();
				mFont = value;
				mTrueTypeFont = null;
				MarkAsChanged();
			}
		}
	}

	public Font trueTypeFont
	{
		get
		{
			if ((Object)(object)mTrueTypeFont != (Object)null)
			{
				return mTrueTypeFont;
			}
			if (!((Object)(object)mFont != (Object)null))
			{
				return null;
			}
			return mFont.dynamicFont;
		}
		set
		{
			if ((Object)(object)mTrueTypeFont != (Object)(object)value)
			{
				SetActiveFont(null);
				RemoveFromPanel();
				mTrueTypeFont = value;
				shouldBeProcessed = true;
				mFont = null;
				SetActiveFont(value);
				ProcessAndRequest();
				if ((Object)(object)mActiveTTF != (Object)null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	public Object ambigiousFont
	{
		get
		{
			if (!((Object)(object)mFont != (Object)null))
			{
				return (Object)(object)mTrueTypeFont;
			}
			return (Object)(object)mFont;
		}
		set
		{
			UIFont uIFont = value as UIFont;
			if ((Object)(object)uIFont != (Object)null)
			{
				bitmapFont = uIFont;
			}
			else
			{
				trueTypeFont = (Font)(object)((value is Font) ? value : null);
			}
		}
	}

	public string text
	{
		get
		{
			return mText;
		}
		set
		{
			if (mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(mText))
				{
					mText = "";
					MarkAsChanged();
					ProcessAndRequest();
				}
			}
			else if (mText != value)
			{
				mText = value;
				MarkAsChanged();
				ProcessAndRequest();
			}
			if (autoResizeBoxCollider)
			{
				ResizeCollider();
			}
		}
	}

	public int defaultFontSize
	{
		get
		{
			if (!((Object)(object)trueTypeFont != (Object)null))
			{
				if (!((Object)(object)mFont != (Object)null))
				{
					return 16;
				}
				return mFont.defaultSize;
			}
			return mFontSize;
		}
	}

	public int fontSize
	{
		get
		{
			return mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (mFontSize != value)
			{
				mFontSize = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public FontStyle fontStyle
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mFontStyle;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (mFontStyle != value)
			{
				mFontStyle = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public NGUIText.Alignment alignment
	{
		get
		{
			return mAlignment;
		}
		set
		{
			if (mAlignment != value)
			{
				mAlignment = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public bool applyGradient
	{
		get
		{
			return mApplyGradient;
		}
		set
		{
			if (mApplyGradient != value)
			{
				mApplyGradient = value;
				MarkAsChanged();
			}
		}
	}

	public Color gradientTop
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mGradientTop;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mGradientTop != value)
			{
				mGradientTop = value;
				if (mApplyGradient)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public Color gradientBottom
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mGradientBottom;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mGradientBottom != value)
			{
				mGradientBottom = value;
				if (mApplyGradient)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public int spacingX
	{
		get
		{
			return mSpacingX;
		}
		set
		{
			if (mSpacingX != value)
			{
				mSpacingX = value;
				MarkAsChanged();
			}
		}
	}

	public int spacingY
	{
		get
		{
			return mSpacingY;
		}
		set
		{
			if (mSpacingY != value)
			{
				mSpacingY = value;
				MarkAsChanged();
			}
		}
	}

	private bool keepCrisp
	{
		get
		{
			if ((Object)(object)trueTypeFont != (Object)null && keepCrispWhenShrunk != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool supportEncoding
	{
		get
		{
			return mEncoding;
		}
		set
		{
			if (mEncoding != value)
			{
				mEncoding = value;
				shouldBeProcessed = true;
			}
		}
	}

	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return mSymbols;
		}
		set
		{
			if (mSymbols != value)
			{
				mSymbols = value;
				shouldBeProcessed = true;
			}
		}
	}

	public Overflow overflowMethod
	{
		get
		{
			return mOverflow;
		}
		set
		{
			if (mOverflow != value)
			{
				mOverflow = value;
				shouldBeProcessed = true;
			}
		}
	}

	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	public bool multiLine
	{
		get
		{
			return mMaxLineCount != 1;
		}
		set
		{
			if (mMaxLineCount != 1 != value)
			{
				mMaxLineCount = ((!value) ? 1 : 0);
				shouldBeProcessed = true;
			}
		}
	}

	public override Vector3[] localCorners
	{
		get
		{
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.localCorners;
		}
	}

	public override Vector3[] worldCorners
	{
		get
		{
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.worldCorners;
		}
	}

	public override Vector4 drawingDimensions
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.drawingDimensions;
		}
	}

	public int maxLineCount
	{
		get
		{
			return mMaxLineCount;
		}
		set
		{
			if (mMaxLineCount != value)
			{
				mMaxLineCount = Mathf.Max(value, 0);
				shouldBeProcessed = true;
				if (overflowMethod == Overflow.ShrinkContent)
				{
					MakePixelPerfect();
				}
			}
		}
	}

	public Effect effectStyle
	{
		get
		{
			return mEffectStyle;
		}
		set
		{
			if (mEffectStyle != value)
			{
				mEffectStyle = value;
				shouldBeProcessed = true;
			}
		}
	}

	public Color effectColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mEffectColor;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mEffectColor != value)
			{
				mEffectColor = value;
				if (mEffectStyle != 0)
				{
					shouldBeProcessed = true;
				}
			}
		}
	}

	public Vector2 effectDistance
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mEffectDistance;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mEffectDistance != value)
			{
				mEffectDistance = value;
				shouldBeProcessed = true;
			}
		}
	}

	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return mOverflow == Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				overflowMethod = Overflow.ShrinkContent;
			}
		}
	}

	public string processedText
	{
		get
		{
			if (mLastWidth != mWidth || mLastHeight != mHeight)
			{
				mLastWidth = mWidth;
				mLastHeight = mHeight;
				mShouldBeProcessed = true;
			}
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return mProcessedText;
		}
	}

	public Vector2 printedSize
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return mCalculatedSize;
		}
	}

	public override Vector2 localSize
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.localSize;
		}
	}

	private bool isValid
	{
		get
		{
			if (!((Object)(object)mFont != (Object)null))
			{
				return (Object)(object)mTrueTypeFont != (Object)null;
			}
			return true;
		}
	}

	protected override void OnInit()
	{
		base.OnInit();
		mList.Add(this);
		SetActiveFont(trueTypeFont);
	}

	protected override void OnDisable()
	{
		SetActiveFont(null);
		mList.Remove(this);
		base.OnDisable();
	}

	protected void SetActiveFont(Font fnt)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		if (!((Object)(object)mActiveTTF != (Object)(object)fnt))
		{
			return;
		}
		if ((Object)(object)mActiveTTF != (Object)null)
		{
			if (mFontUsage.TryGetValue(mActiveTTF, out var value))
			{
				value = Mathf.Max(0, --value);
				if (value == 0)
				{
					mActiveTTF.textureRebuildCallback = null;
					mFontUsage.Remove(mActiveTTF);
				}
				else
				{
					mFontUsage[mActiveTTF] = value;
				}
			}
			else
			{
				mActiveTTF.textureRebuildCallback = null;
			}
		}
		mActiveTTF = fnt;
		if ((Object)(object)mActiveTTF != (Object)null)
		{
			int value2 = 0;
			if (!mFontUsage.TryGetValue(mActiveTTF, out value2))
			{
				mActiveTTF.textureRebuildCallback = new FontTextureRebuildCallback(OnFontTextureChanged);
			}
			value2 = (mFontUsage[mActiveTTF] = value2 + 1);
		}
	}

	private static void OnFontTextureChanged()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < mList.size; i++)
		{
			UILabel uILabel = mList[i];
			if ((Object)(object)uILabel != (Object)null)
			{
				Font val = uILabel.trueTypeFont;
				if ((Object)(object)val != (Object)null)
				{
					val.RequestCharactersInTexture(uILabel.mText, uILabel.mPrintedSize, uILabel.mFontStyle);
					uILabel.MarkAsChanged();
				}
			}
		}
	}

	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (shouldBeProcessed)
		{
			ProcessText();
		}
		return base.GetSides(relativeTo);
	}

	protected override void UpgradeFrom265()
	{
		ProcessText(legacyMode: true, full: true);
		if (mShrinkToFit)
		{
			overflowMethod = Overflow.ShrinkContent;
			mMaxLineCount = 0;
		}
		if (mMaxLineWidth != 0)
		{
			base.width = mMaxLineWidth;
			overflowMethod = ((mMaxLineCount > 0) ? Overflow.ResizeHeight : Overflow.ShrinkContent);
		}
		else
		{
			overflowMethod = Overflow.ResizeFreely;
		}
		if (mMaxLineHeight != 0)
		{
			base.height = mMaxLineHeight;
		}
		if ((Object)(object)mFont != (Object)null)
		{
			int defaultSize = mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
		}
		mMaxLineWidth = 0;
		mMaxLineHeight = 0;
		mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(((Component)this).gameObject, considerInactive: true);
	}

	protected override void OnAnchor()
	{
		if (mOverflow == Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				mOverflow = Overflow.ShrinkContent;
			}
		}
		else if (mOverflow == Overflow.ResizeHeight && (Object)(object)topAnchor.target != (Object)null && (Object)(object)bottomAnchor.target != (Object)null)
		{
			mOverflow = Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	private void ProcessAndRequest()
	{
		if (ambigiousFont != (Object)null)
		{
			ProcessText();
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
		if (mLineWidth > 0f)
		{
			mMaxLineWidth = Mathf.RoundToInt(mLineWidth);
			mLineWidth = 0f;
		}
		if (!mMultiline)
		{
			mMaxLineCount = 1;
			mMultiline = true;
		}
		mPremultiply = (Object)(object)material != (Object)null && (Object)(object)material.shader != (Object)null && ((Object)material.shader).name.Contains("Premultiplied");
		ProcessAndRequest();
	}

	public override void MarkAsChanged()
	{
		shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	public void ProcessText()
	{
		ProcessText(legacyMode: false, full: true);
	}

	private void ProcessText(bool legacyMode, bool full)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		if (!isValid)
		{
			return;
		}
		mChanged = true;
		shouldBeProcessed = false;
		NGUIText.rectWidth = ((!legacyMode) ? base.width : ((mMaxLineWidth != 0) ? mMaxLineWidth : 1000000));
		NGUIText.rectHeight = ((!legacyMode) ? base.height : ((mMaxLineHeight != 0) ? mMaxLineHeight : 1000000));
		mPrintedSize = Mathf.Abs(legacyMode ? Mathf.RoundToInt(base.cachedTransform.localScale.x) : defaultFontSize);
		mScale = 1f;
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 0)
		{
			mProcessedText = "";
			return;
		}
		bool flag = (Object)(object)trueTypeFont != (Object)null;
		if (flag && keepCrisp)
		{
			UIRoot uIRoot = base.root;
			if ((Object)(object)uIRoot != (Object)null)
			{
				mDensity = (((Object)(object)uIRoot != (Object)null) ? uIRoot.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			mDensity = 1f;
		}
		if (full)
		{
			UpdateNGUIText();
		}
		if (mOverflow == Overflow.ResizeFreely)
		{
			NGUIText.rectWidth = 1000000;
		}
		if (mOverflow == Overflow.ResizeFreely || mOverflow == Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
		}
		if (mPrintedSize > 0)
		{
			bool flag2 = keepCrisp;
			int num = mPrintedSize;
			while (num > 0)
			{
				if (flag2)
				{
					mPrintedSize = num;
					NGUIText.fontSize = mPrintedSize;
				}
				else
				{
					mScale = (float)num / (float)mPrintedSize;
					NGUIText.fontScale = (flag ? mScale : ((float)mFontSize / (float)mFont.defaultSize * mScale));
				}
				NGUIText.Update(request: false);
				bool flag3 = NGUIText.WrapText(mText, out mProcessedText, keepCharCount: true);
				if (mOverflow == Overflow.ShrinkContent && !flag3)
				{
					if (--num <= 1)
					{
						break;
					}
					num--;
					continue;
				}
				if (mOverflow == Overflow.ResizeFreely)
				{
					mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
					mWidth = Mathf.Max(minWidth, Mathf.RoundToInt(mCalculatedSize.x));
					mHeight = Mathf.Max(minHeight, Mathf.RoundToInt(mCalculatedSize.y));
					if ((mWidth & 1) == 1)
					{
						mWidth++;
					}
					if ((mHeight & 1) == 1)
					{
						mHeight++;
					}
				}
				else if (mOverflow == Overflow.ResizeHeight)
				{
					mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
					mHeight = Mathf.Max(minHeight, Mathf.RoundToInt(mCalculatedSize.y));
					if ((mHeight & 1) == 1)
					{
						mHeight++;
					}
				}
				else
				{
					mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
				}
				if (legacyMode)
				{
					base.width = Mathf.RoundToInt(mCalculatedSize.x);
					base.height = Mathf.RoundToInt(mCalculatedSize.y);
					base.cachedTransform.localScale = Vector3.one;
				}
				break;
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			mProcessedText = "";
			mScale = 1f;
		}
		if (full)
		{
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	public override void MakePixelPerfect()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (ambigiousFont != (Object)null)
		{
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = Mathf.RoundToInt(localPosition.x);
			localPosition.y = Mathf.RoundToInt(localPosition.y);
			localPosition.z = Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = Vector3.one;
			if (mOverflow == Overflow.ResizeFreely)
			{
				AssumeNaturalSize();
				return;
			}
			int num = base.width;
			int num2 = base.height;
			Overflow overflow = mOverflow;
			if (overflow != Overflow.ResizeHeight)
			{
				mWidth = 100000;
			}
			mHeight = 100000;
			mOverflow = Overflow.ShrinkContent;
			ProcessText(legacyMode: false, full: true);
			mOverflow = overflow;
			int num3 = Mathf.RoundToInt(mCalculatedSize.x);
			int num4 = Mathf.RoundToInt(mCalculatedSize.y);
			num3 = Mathf.Max(num3, base.minWidth);
			num4 = Mathf.Max(num4, base.minHeight);
			mWidth = Mathf.Max(num, num3);
			mHeight = Mathf.Max(num2, num4);
			MarkAsChanged();
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	public void AssumeNaturalSize()
	{
		if (ambigiousFont != (Object)null)
		{
			mWidth = 100000;
			mHeight = 100000;
			ProcessText(legacyMode: false, full: true);
			mWidth = Mathf.RoundToInt(mCalculatedSize.x);
			mHeight = Mathf.RoundToInt(mCalculatedSize.y);
			if ((mWidth & 1) == 1)
			{
				mWidth++;
			}
			if ((mHeight & 1) == 1)
			{
				mHeight++;
			}
			MarkAsChanged();
		}
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return GetCharacterIndexAtPosition(worldPos);
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return GetCharacterIndexAtPosition(localPos);
	}

	public int GetCharacterIndexAtPosition(Vector3 worldPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Vector2 localPos = Vector2.op_Implicit(base.cachedTransform.InverseTransformPoint(worldPos));
		return GetCharacterIndexAtPosition(localPos);
	}

	public int GetCharacterIndexAtPosition(Vector2 localPos)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (isValid)
		{
			string value = processedText;
			if (string.IsNullOrEmpty(value))
			{
				return 0;
			}
			UpdateNGUIText();
			NGUIText.PrintCharacterPositions(value, mTempVerts, mTempIndices);
			if (mTempVerts.size > 0)
			{
				ApplyOffset(mTempVerts, 0);
				int closestCharacter = NGUIText.GetClosestCharacter(mTempVerts, localPos);
				closestCharacter = mTempIndices[closestCharacter];
				mTempVerts.Clear();
				mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return closestCharacter;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	public string GetWordAtPosition(Vector3 worldPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetWordAtCharacterIndex(GetCharacterIndexAtPosition(worldPos));
	}

	public string GetWordAtPosition(Vector2 localPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetWordAtCharacterIndex(GetCharacterIndexAtPosition(localPos));
	}

	public string GetWordAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < mText.Length)
		{
			int num = mText.LastIndexOf(' ', characterIndex) + 1;
			int num2 = mText.IndexOf(' ', characterIndex);
			if (num2 == -1)
			{
				num2 = mText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					return NGUIText.StripSymbols(mText.Substring(num, num3));
				}
			}
		}
		return null;
	}

	public string GetUrlAtPosition(Vector3 worldPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(worldPos));
	}

	public string GetUrlAtPosition(Vector2 localPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(localPos));
	}

	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < mText.Length)
		{
			int num = mText.LastIndexOf("[url=", characterIndex);
			if (num != -1)
			{
				num += 5;
				int num2 = mText.IndexOf("]", num);
				if (num2 != -1)
				{
					return mText.Substring(num, num2 - num);
				}
			}
		}
		return null;
	}

	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Invalid comparison between Unknown and I4
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Invalid comparison between Unknown and I4
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Invalid comparison between Unknown and I4
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Invalid comparison between Unknown and I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Invalid comparison between Unknown and I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Invalid comparison between Unknown and I4
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Invalid comparison between Unknown and I4
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Invalid comparison between Unknown and I4
		if (isValid)
		{
			string text = processedText;
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			int num = defaultFontSize;
			UpdateNGUIText();
			NGUIText.PrintCharacterPositions(text, mTempVerts, mTempIndices);
			if (mTempVerts.size > 0)
			{
				ApplyOffset(mTempVerts, 0);
				for (int i = 0; i < mTempIndices.size; i++)
				{
					if (mTempIndices[i] == currentIndex)
					{
						Vector2 pos = Vector2.op_Implicit(mTempVerts[i]);
						if ((int)key == 273)
						{
							pos.y += num + spacingY;
						}
						else if ((int)key == 274)
						{
							pos.y -= num + spacingY;
						}
						else if ((int)key == 278)
						{
							pos.x -= 1000f;
						}
						else if ((int)key == 279)
						{
							pos.x += 1000f;
						}
						int closestCharacter = NGUIText.GetClosestCharacter(mTempVerts, pos);
						closestCharacter = mTempIndices[closestCharacter];
						if (closestCharacter == currentIndex)
						{
							break;
						}
						mTempVerts.Clear();
						mTempIndices.Clear();
						return closestCharacter;
					}
				}
				mTempVerts.Clear();
				mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if ((int)key == 273 || (int)key == 278)
			{
				return 0;
			}
			if ((int)key == 274 || (int)key == 279)
			{
				return text.Length;
			}
		}
		return currentIndex;
	}

	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		caret?.Clear();
		highlight?.Clear();
		if (!isValid)
		{
			return;
		}
		string text = processedText;
		UpdateNGUIText();
		int size = caret.verts.size;
		Vector2 item = default(Vector2);
		((Vector2)(ref item))._002Ector(0.5f, 0.5f);
		float num = finalAlpha;
		if (highlight != null && start != end)
		{
			int size2 = highlight.verts.size;
			NGUIText.PrintCaretAndSelection(text, start, end, caret.verts, highlight.verts);
			if (highlight.verts.size > size2)
			{
				ApplyOffset(highlight.verts, size2);
				Color32 item2 = Color32.op_Implicit(new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * num));
				for (int i = size2; i < highlight.verts.size; i++)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(text, start, end, caret.verts, null);
		}
		ApplyOffset(caret.verts, size);
		Color32 item3 = Color32.op_Implicit(new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * num));
		for (int j = size; j < caret.verts.size; j++)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
		}
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		if (!isValid)
		{
			return;
		}
		int num = verts.size;
		Color val = base.color;
		val.a = finalAlpha;
		if ((Object)(object)mFont != (Object)null && mFont.premultipliedAlphaShader)
		{
			val = NGUITools.ApplyPMA(val);
		}
		string obj = processedText;
		int size = verts.size;
		UpdateNGUIText();
		NGUIText.tint = val;
		NGUIText.Print(obj, verts, uvs, cols);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		Vector2 val2 = ApplyOffset(verts, size);
		if ((Object)(object)mFont != (Object)null && mFont.packedFontShader)
		{
			return;
		}
		if (effectStyle != 0)
		{
			int size2 = verts.size;
			val2.x = mEffectDistance.x;
			val2.y = mEffectDistance.y;
			ApplyShadow(verts, uvs, cols, num, size2, val2.x, 0f - val2.y);
			if (effectStyle == Effect.Outline)
			{
				num = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, num, size2, 0f - val2.x, val2.y);
				num = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, num, size2, val2.x, val2.y);
				num = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, num, size2, 0f - val2.x, 0f - val2.y);
			}
		}
		if (onPostFill != null)
		{
			onPostFill(this, num, verts, uvs, cols);
		}
	}

	public Vector2 ApplyOffset(BetterList<Vector3> verts, int start)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-mWidth), val.x);
		float num2 = Mathf.Lerp((float)mHeight, 0f, val.y) + Mathf.Lerp(mCalculatedSize.y - (float)mHeight, 0f, val.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		for (int i = start; i < verts.size; i++)
		{
			verts.buffer[i].x += num;
			verts.buffer[i].y += num2;
		}
		return new Vector2(num, num2);
	}

	public void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		Color val = mEffectColor;
		val.a *= finalAlpha;
		Color32 val2 = Color32.op_Implicit(((Object)(object)bitmapFont != (Object)null && bitmapFont.premultipliedAlphaShader) ? NGUITools.ApplyPMA(val) : val);
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 val3 = verts.buffer[i];
			val3.x += x;
			val3.y += y;
			verts.buffer[i] = val3;
			Color32 val4 = cols.buffer[i];
			if (val4.a == byte.MaxValue)
			{
				cols.buffer[i] = val2;
				continue;
			}
			Color val5 = val;
			val5.a = (float)(int)val4.a / 255f * val.a;
			cols.buffer[i] = Color32.op_Implicit(((Object)(object)bitmapFont != (Object)null && bitmapFont.premultipliedAlphaShader) ? NGUITools.ApplyPMA(val5) : val5);
		}
	}

	public int CalculateOffsetToFit(string text)
	{
		UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	public void SetCurrentProgress()
	{
		if ((Object)(object)UIProgressBar.current != (Object)null)
		{
			text = UIProgressBar.current.value.ToString("F");
		}
	}

	public void SetCurrentPercent()
	{
		if ((Object)(object)UIProgressBar.current != (Object)null)
		{
			text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	public void SetCurrentSelection()
	{
		if ((Object)(object)UIPopupList.current != (Object)null)
		{
			text = (UIPopupList.current.isLocalized ? Localization.Get(UIPopupList.current.value) : UIPopupList.current.value);
		}
	}

	public bool Wrap(string text, out string final)
	{
		return Wrap(text, out final, 1000000);
	}

	public bool Wrap(string text, out string final, int height)
	{
		UpdateNGUIText();
		NGUIText.rectHeight = height;
		bool result = NGUIText.WrapText(text, out final);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	public void UpdateNGUIText()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Font val = trueTypeFont;
		bool flag = (Object)(object)val != (Object)null;
		NGUIText.fontSize = mPrintedSize;
		NGUIText.fontStyle = mFontStyle;
		NGUIText.rectWidth = mWidth;
		NGUIText.rectHeight = mHeight;
		NGUIText.gradient = mApplyGradient && ((Object)(object)mFont == (Object)null || !mFont.packedFontShader);
		NGUIText.gradientTop = mGradientTop;
		NGUIText.gradientBottom = mGradientBottom;
		NGUIText.encoding = mEncoding;
		NGUIText.premultiply = mPremultiply;
		NGUIText.symbolStyle = mSymbols;
		NGUIText.maxLines = mMaxLineCount;
		NGUIText.spacingX = mSpacingX;
		NGUIText.spacingY = mSpacingY;
		NGUIText.fontScale = (flag ? mScale : ((float)mFontSize / (float)mFont.defaultSize * mScale));
		if ((Object)(object)mFont != (Object)null)
		{
			NGUIText.bitmapFont = mFont;
			while (true)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if ((Object)(object)replacement == (Object)null)
				{
					break;
				}
				NGUIText.bitmapFont = replacement;
			}
			if (NGUIText.bitmapFont.isDynamic)
			{
				NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
			}
		}
		else
		{
			NGUIText.dynamicFont = val;
			NGUIText.bitmapFont = null;
		}
		if (flag && keepCrisp)
		{
			UIRoot uIRoot = base.root;
			if ((Object)(object)uIRoot != (Object)null)
			{
				NGUIText.pixelDensity = (((Object)(object)uIRoot != (Object)null) ? uIRoot.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (mDensity != NGUIText.pixelDensity)
		{
			ProcessText(legacyMode: false, full: false);
			NGUIText.rectWidth = mWidth;
			NGUIText.rectHeight = mHeight;
		}
		if (alignment == NGUIText.Alignment.Automatic)
		{
			switch (base.pivot)
			{
			case Pivot.TopLeft:
			case Pivot.Left:
			case Pivot.BottomLeft:
				NGUIText.alignment = NGUIText.Alignment.Left;
				break;
			case Pivot.TopRight:
			case Pivot.Right:
			case Pivot.BottomRight:
				NGUIText.alignment = NGUIText.Alignment.Right;
				break;
			default:
				NGUIText.alignment = NGUIText.Alignment.Center;
				break;
			}
		}
		else
		{
			NGUIText.alignment = alignment;
		}
		NGUIText.Update();
	}
}
