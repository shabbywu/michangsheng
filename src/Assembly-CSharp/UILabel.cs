using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000975 RID: 2421 RVA: 0x0003970E File Offset: 0x0003790E
	// (set) Token: 0x06000976 RID: 2422 RVA: 0x00039716 File Offset: 0x00037916
	private bool shouldBeProcessed
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
				return;
			}
			this.mShouldBeProcessed = false;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000977 RID: 2423 RVA: 0x00039731 File Offset: 0x00037931
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06000978 RID: 2424 RVA: 0x00039746 File Offset: 0x00037946
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x00039764 File Offset: 0x00037964
	// (set) Token: 0x0600097A RID: 2426 RVA: 0x000397BB File Offset: 0x000379BB
	public override Material material
	{
		get
		{
			if (this.mMaterial != null)
			{
				return this.mMaterial;
			}
			if (this.mFont != null)
			{
				return this.mFont.material;
			}
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.MarkAsChanged();
				this.mMaterial = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x0600097B RID: 2427 RVA: 0x000397DE File Offset: 0x000379DE
	// (set) Token: 0x0600097C RID: 2428 RVA: 0x000397E6 File Offset: 0x000379E6
	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return this.bitmapFont;
		}
		set
		{
			this.bitmapFont = value;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600097D RID: 2429 RVA: 0x000397EF File Offset: 0x000379EF
	// (set) Token: 0x0600097E RID: 2430 RVA: 0x000397F7 File Offset: 0x000379F7
	public UIFont bitmapFont
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				base.RemoveFromPanel();
				this.mFont = value;
				this.mTrueTypeFont = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600097F RID: 2431 RVA: 0x00039821 File Offset: 0x00037A21
	// (set) Token: 0x06000980 RID: 2432 RVA: 0x00039854 File Offset: 0x00037A54
	public Font trueTypeFont
	{
		get
		{
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont;
			}
			if (!(this.mFont != null))
			{
				return null;
			}
			return this.mFont.dynamicFont;
		}
		set
		{
			if (this.mTrueTypeFont != value)
			{
				this.SetActiveFont(null);
				base.RemoveFromPanel();
				this.mTrueTypeFont = value;
				this.shouldBeProcessed = true;
				this.mFont = null;
				this.SetActiveFont(value);
				this.ProcessAndRequest();
				if (this.mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06000981 RID: 2433 RVA: 0x000398B2 File Offset: 0x00037AB2
	// (set) Token: 0x06000982 RID: 2434 RVA: 0x000398D0 File Offset: 0x00037AD0
	public Object ambigiousFont
	{
		get
		{
			if (!(this.mFont != null))
			{
				return this.mTrueTypeFont;
			}
			return this.mFont;
		}
		set
		{
			UIFont uifont = value as UIFont;
			if (uifont != null)
			{
				this.bitmapFont = uifont;
				return;
			}
			this.trueTypeFont = (value as Font);
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x00039901 File Offset: 0x00037B01
	// (set) Token: 0x06000984 RID: 2436 RVA: 0x0003990C File Offset: 0x00037B0C
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = "";
					this.MarkAsChanged();
					this.ProcessAndRequest();
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
			}
			if (this.autoResizeBoxCollider)
			{
				base.ResizeCollider();
			}
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000985 RID: 2437 RVA: 0x00039985 File Offset: 0x00037B85
	public int defaultFontSize
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.mFontSize;
			}
			if (!(this.mFont != null))
			{
				return 16;
			}
			return this.mFont.defaultSize;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000986 RID: 2438 RVA: 0x000399B8 File Offset: 0x00037BB8
	// (set) Token: 0x06000987 RID: 2439 RVA: 0x000399C0 File Offset: 0x00037BC0
	public int fontSize
	{
		get
		{
			return this.mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (this.mFontSize != value)
			{
				this.mFontSize = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000988 RID: 2440 RVA: 0x000399ED File Offset: 0x00037BED
	// (set) Token: 0x06000989 RID: 2441 RVA: 0x000399F5 File Offset: 0x00037BF5
	public FontStyle fontStyle
	{
		get
		{
			return this.mFontStyle;
		}
		set
		{
			if (this.mFontStyle != value)
			{
				this.mFontStyle = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x0600098A RID: 2442 RVA: 0x00039A14 File Offset: 0x00037C14
	// (set) Token: 0x0600098B RID: 2443 RVA: 0x00039A1C File Offset: 0x00037C1C
	public NGUIText.Alignment alignment
	{
		get
		{
			return this.mAlignment;
		}
		set
		{
			if (this.mAlignment != value)
			{
				this.mAlignment = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x0600098C RID: 2444 RVA: 0x00039A3B File Offset: 0x00037C3B
	// (set) Token: 0x0600098D RID: 2445 RVA: 0x00039A43 File Offset: 0x00037C43
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600098E RID: 2446 RVA: 0x00039A5B File Offset: 0x00037C5B
	// (set) Token: 0x0600098F RID: 2447 RVA: 0x00039A63 File Offset: 0x00037C63
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000990 RID: 2448 RVA: 0x00039A88 File Offset: 0x00037C88
	// (set) Token: 0x06000991 RID: 2449 RVA: 0x00039A90 File Offset: 0x00037C90
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000992 RID: 2450 RVA: 0x00039AB5 File Offset: 0x00037CB5
	// (set) Token: 0x06000993 RID: 2451 RVA: 0x00039ABD File Offset: 0x00037CBD
	public int spacingX
	{
		get
		{
			return this.mSpacingX;
		}
		set
		{
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000994 RID: 2452 RVA: 0x00039AD5 File Offset: 0x00037CD5
	// (set) Token: 0x06000995 RID: 2453 RVA: 0x00039ADD File Offset: 0x00037CDD
	public int spacingY
	{
		get
		{
			return this.mSpacingY;
		}
		set
		{
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000996 RID: 2454 RVA: 0x00039AF5 File Offset: 0x00037CF5
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000997 RID: 2455 RVA: 0x00039B10 File Offset: 0x00037D10
	// (set) Token: 0x06000998 RID: 2456 RVA: 0x00039B18 File Offset: 0x00037D18
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000999 RID: 2457 RVA: 0x00039B31 File Offset: 0x00037D31
	// (set) Token: 0x0600099A RID: 2458 RVA: 0x00039B39 File Offset: 0x00037D39
	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x0600099B RID: 2459 RVA: 0x00039B52 File Offset: 0x00037D52
	// (set) Token: 0x0600099C RID: 2460 RVA: 0x00039B5A File Offset: 0x00037D5A
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x0600099D RID: 2461 RVA: 0x00039B73 File Offset: 0x00037D73
	// (set) Token: 0x0600099E RID: 2462 RVA: 0x00039B7B File Offset: 0x00037D7B
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

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x0600099F RID: 2463 RVA: 0x00039B84 File Offset: 0x00037D84
	// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00039B8C File Offset: 0x00037D8C
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

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00039B95 File Offset: 0x00037D95
	// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00039BA3 File Offset: 0x00037DA3
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = (value ? 0 : 1);
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00039BC8 File Offset: 0x00037DC8
	public override Vector3[] localCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.localCorners;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00039BDE File Offset: 0x00037DDE
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.worldCorners;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00039BF4 File Offset: 0x00037DF4
	public override Vector4 drawingDimensions
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.drawingDimensions;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00039C0A File Offset: 0x00037E0A
	// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00039C12 File Offset: 0x00037E12
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.shouldBeProcessed = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00039C3F File Offset: 0x00037E3F
	// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00039C47 File Offset: 0x00037E47
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060009AA RID: 2474 RVA: 0x00039C60 File Offset: 0x00037E60
	// (set) Token: 0x060009AB RID: 2475 RVA: 0x00039C68 File Offset: 0x00037E68
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.shouldBeProcessed = true;
				}
			}
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x00039C8E File Offset: 0x00037E8E
	// (set) Token: 0x060009AD RID: 2477 RVA: 0x00039C96 File Offset: 0x00037E96
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x00039CB4 File Offset: 0x00037EB4
	// (set) Token: 0x060009AF RID: 2479 RVA: 0x00039CBF File Offset: 0x00037EBF
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00039CCC File Offset: 0x00037ECC
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00039D28 File Offset: 0x00037F28
	public Vector2 printedSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return this.mCalculatedSize;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00039D3E File Offset: 0x00037F3E
	public override Vector2 localSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText();
			}
			return base.localSize;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00039D54 File Offset: 0x00037F54
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x00039D72 File Offset: 0x00037F72
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00039D91 File Offset: 0x00037F91
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00039DAC File Offset: 0x00037FAC
	protected void SetActiveFont(Font fnt)
	{
		if (this.mActiveTTF != fnt)
		{
			if (this.mActiveTTF != null)
			{
				int num;
				if (UILabel.mFontUsage.TryGetValue(this.mActiveTTF, out num))
				{
					num = Mathf.Max(0, --num);
					if (num == 0)
					{
						this.mActiveTTF.textureRebuildCallback = null;
						UILabel.mFontUsage.Remove(this.mActiveTTF);
					}
					else
					{
						UILabel.mFontUsage[this.mActiveTTF] = num;
					}
				}
				else
				{
					this.mActiveTTF.textureRebuildCallback = null;
				}
			}
			this.mActiveTTF = fnt;
			if (this.mActiveTTF != null)
			{
				int num2 = 0;
				if (!UILabel.mFontUsage.TryGetValue(this.mActiveTTF, out num2))
				{
					this.mActiveTTF.textureRebuildCallback = new Font.FontTextureRebuildCallback(UILabel.OnFontTextureChanged);
				}
				num2 = (UILabel.mFontUsage[this.mActiveTTF] = num2 + 1);
			}
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00039E90 File Offset: 0x00038090
	private static void OnFontTextureChanged()
	{
		for (int i = 0; i < UILabel.mList.size; i++)
		{
			UILabel uilabel = UILabel.mList[i];
			if (uilabel != null)
			{
				Font trueTypeFont = uilabel.trueTypeFont;
				if (trueTypeFont != null)
				{
					trueTypeFont.RequestCharactersInTexture(uilabel.mText, uilabel.mPrintedSize, uilabel.mFontStyle);
					uilabel.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00039EF5 File Offset: 0x000380F5
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText();
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00039F0C File Offset: 0x0003810C
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true, true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount > 0) ? UILabel.Overflow.ResizeHeight : UILabel.Overflow.ShrinkContent);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int defaultSize = this.mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00039FC8 File Offset: 0x000381C8
	protected override void OnAnchor()
	{
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				this.mOverflow = UILabel.Overflow.ShrinkContent;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeHeight && this.topAnchor.target != null && this.bottomAnchor.target != null)
		{
			this.mOverflow = UILabel.Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0003A02B File Offset: 0x0003822B
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText();
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0003A044 File Offset: 0x00038244
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.material != null && this.material.shader != null && this.material.shader.name.Contains("Premultiplied"));
		this.ProcessAndRequest();
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0003A0E0 File Offset: 0x000382E0
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0003A0EF File Offset: 0x000382EF
	public void ProcessText()
	{
		this.ProcessText(false, true);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003A0FC File Offset: 0x000382FC
	private void ProcessText(bool legacyMode, bool full)
	{
		if (!this.isValid)
		{
			return;
		}
		this.mChanged = true;
		this.shouldBeProcessed = false;
		NGUIText.rectWidth = (legacyMode ? ((this.mMaxLineWidth != 0) ? this.mMaxLineWidth : 1000000) : base.width);
		NGUIText.rectHeight = (legacyMode ? ((this.mMaxLineHeight != 0) ? this.mMaxLineHeight : 1000000) : base.height);
		this.mPrintedSize = Mathf.Abs(legacyMode ? Mathf.RoundToInt(base.cachedTransform.localScale.x) : this.defaultFontSize);
		this.mScale = 1f;
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 0)
		{
			this.mProcessedText = "";
			return;
		}
		bool flag = this.trueTypeFont != null;
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				this.mDensity = ((root != null) ? root.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			this.mDensity = 1f;
		}
		if (full)
		{
			this.UpdateNGUIText();
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			NGUIText.rectWidth = 1000000;
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
		}
		if (this.mPrintedSize > 0)
		{
			bool keepCrisp = this.keepCrisp;
			int i = this.mPrintedSize;
			while (i > 0)
			{
				if (keepCrisp)
				{
					this.mPrintedSize = i;
					NGUIText.fontSize = this.mPrintedSize;
				}
				else
				{
					this.mScale = (float)i / (float)this.mPrintedSize;
					NGUIText.fontScale = (flag ? this.mScale : ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale));
				}
				NGUIText.Update(false);
				bool flag2 = NGUIText.WrapText(this.mText, out this.mProcessedText, true);
				if (this.mOverflow == UILabel.Overflow.ShrinkContent && !flag2)
				{
					if (--i <= 1)
					{
						break;
					}
					i--;
				}
				else
				{
					if (this.mOverflow == UILabel.Overflow.ResizeFreely)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						this.mWidth = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
						this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if ((this.mWidth & 1) == 1)
						{
							this.mWidth++;
						}
						if ((this.mHeight & 1) == 1)
						{
							this.mHeight++;
						}
					}
					else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if ((this.mHeight & 1) == 1)
						{
							this.mHeight++;
						}
					}
					else
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
					}
					if (legacyMode)
					{
						base.width = Mathf.RoundToInt(this.mCalculatedSize.x);
						base.height = Mathf.RoundToInt(this.mCalculatedSize.y);
						base.cachedTransform.localScale = Vector3.one;
						break;
					}
					break;
				}
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = "";
			this.mScale = 1f;
		}
		if (full)
		{
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0003A46C File Offset: 0x0003866C
	public override void MakePixelPerfect()
	{
		if (!(this.ambigiousFont != null))
		{
			base.MakePixelPerfect();
			return;
		}
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
		localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		base.cachedTransform.localPosition = localPosition;
		base.cachedTransform.localScale = Vector3.one;
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			this.AssumeNaturalSize();
			return;
		}
		int width = base.width;
		int height = base.height;
		UILabel.Overflow overflow = this.mOverflow;
		if (overflow != UILabel.Overflow.ResizeHeight)
		{
			this.mWidth = 100000;
		}
		this.mHeight = 100000;
		this.mOverflow = UILabel.Overflow.ShrinkContent;
		this.ProcessText(false, true);
		this.mOverflow = overflow;
		int num = Mathf.RoundToInt(this.mCalculatedSize.x);
		int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
		num = Mathf.Max(num, base.minWidth);
		num2 = Mathf.Max(num2, base.minHeight);
		this.mWidth = Mathf.Max(width, num);
		this.mHeight = Mathf.Max(height, num2);
		this.MarkAsChanged();
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0003A5AC File Offset: 0x000387AC
	public void AssumeNaturalSize()
	{
		if (this.ambigiousFont != null)
		{
			this.mWidth = 100000;
			this.mHeight = 100000;
			this.ProcessText(false, true);
			this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
			this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
			if ((this.mWidth & 1) == 1)
			{
				this.mWidth++;
			}
			if ((this.mHeight & 1) == 1)
			{
				this.mHeight++;
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003A64C File Offset: 0x0003884C
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003A655 File Offset: 0x00038855
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0003A660 File Offset: 0x00038860
	public int GetCharacterIndexAtPosition(Vector3 worldPos)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003A688 File Offset: 0x00038888
	public int GetCharacterIndexAtPosition(Vector2 localPos)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			this.UpdateNGUIText();
			NGUIText.PrintCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.size > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int num = NGUIText.GetClosestCharacter(UILabel.mTempVerts, localPos);
				num = UILabel.mTempIndices[num];
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return num;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003A728 File Offset: 0x00038928
	public string GetWordAtPosition(Vector3 worldPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0003A737 File Offset: 0x00038937
	public string GetWordAtPosition(Vector2 localPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0003A748 File Offset: 0x00038948
	public string GetWordAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < this.mText.Length)
		{
			int num = this.mText.LastIndexOf(' ', characterIndex) + 1;
			int num2 = this.mText.IndexOf(' ', characterIndex);
			if (num2 == -1)
			{
				num2 = this.mText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					return NGUIText.StripSymbols(this.mText.Substring(num, num3));
				}
			}
		}
		return null;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003A7B7 File Offset: 0x000389B7
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003A7C6 File Offset: 0x000389C6
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0003A7D8 File Offset: 0x000389D8
	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < this.mText.Length)
		{
			int num = this.mText.LastIndexOf("[url=", characterIndex);
			if (num != -1)
			{
				num += 5;
				int num2 = this.mText.IndexOf("]", num);
				if (num2 != -1)
				{
					return this.mText.Substring(num, num2 - num);
				}
			}
		}
		return null;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0003A838 File Offset: 0x00038A38
	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			this.UpdateNGUIText();
			NGUIText.PrintCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.size > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int i = 0;
				while (i < UILabel.mTempIndices.size)
				{
					if (UILabel.mTempIndices[i] == currentIndex)
					{
						Vector2 pos = UILabel.mTempVerts[i];
						if (key == 273)
						{
							pos.y += (float)(defaultFontSize + this.spacingY);
						}
						else if (key == 274)
						{
							pos.y -= (float)(defaultFontSize + this.spacingY);
						}
						else if (key == 278)
						{
							pos.x -= 1000f;
						}
						else if (key == 279)
						{
							pos.x += 1000f;
						}
						int num = NGUIText.GetClosestCharacter(UILabel.mTempVerts, pos);
						num = UILabel.mTempIndices[num];
						if (num != currentIndex)
						{
							UILabel.mTempVerts.Clear();
							UILabel.mTempIndices.Clear();
							return num;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if (key == 273 || key == 278)
			{
				return 0;
			}
			if (key == 274 || key == 279)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003A9C8 File Offset: 0x00038BC8
	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		if (caret != null)
		{
			caret.Clear();
		}
		if (highlight != null)
		{
			highlight.Clear();
		}
		if (!this.isValid)
		{
			return;
		}
		string processedText = this.processedText;
		this.UpdateNGUIText();
		int size = caret.verts.size;
		Vector2 item;
		item..ctor(0.5f, 0.5f);
		float finalAlpha = this.finalAlpha;
		if (highlight != null && start != end)
		{
			int size2 = highlight.verts.size;
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
			if (highlight.verts.size > size2)
			{
				this.ApplyOffset(highlight.verts, size2);
				Color32 item2 = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha);
				for (int i = size2; i < highlight.verts.size; i++)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
		}
		this.ApplyOffset(caret.verts, size);
		Color32 item3 = new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha);
		for (int j = size; j < caret.verts.size; j++)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
		}
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0003AB60 File Offset: 0x00038D60
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (!this.isValid)
		{
			return;
		}
		int num = verts.size;
		Color color = base.color;
		color.a = this.finalAlpha;
		if (this.mFont != null && this.mFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string processedText = this.processedText;
		int size = verts.size;
		this.UpdateNGUIText();
		NGUIText.tint = color;
		NGUIText.Print(processedText, verts, uvs, cols);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		Vector2 vector = this.ApplyOffset(verts, size);
		if (this.mFont != null && this.mFont.packedFontShader)
		{
			return;
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int size2 = verts.size;
			vector.x = this.mEffectDistance.x;
			vector.y = this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, num, size2, vector.x, -vector.y);
			if (this.effectStyle == UILabel.Effect.Outline)
			{
				num = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, size2, -vector.x, vector.y);
				num = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, size2, vector.x, vector.y);
				num = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, num, size2, -vector.x, -vector.y);
			}
		}
		if (this.onPostFill != null)
		{
			this.onPostFill(this, num, verts, uvs, cols);
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0003ACE4 File Offset: 0x00038EE4
	public Vector2 ApplyOffset(BetterList<Vector3> verts, int start)
	{
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float)this.mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		for (int i = start; i < verts.size; i++)
		{
			Vector3[] buffer = verts.buffer;
			int num3 = i;
			buffer[num3].x = buffer[num3].x + num;
			Vector3[] buffer2 = verts.buffer;
			int num4 = i;
			buffer2[num4].y = buffer2[num4].y + num2;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0003ADA0 File Offset: 0x00038FA0
	public void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= this.finalAlpha;
		Color32 color2 = (this.bitmapFont != null && this.bitmapFont.premultipliedAlphaShader) ? NGUITools.ApplyPMA(color) : color;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			Color32 color3 = cols.buffer[i];
			if (color3.a == 255)
			{
				cols.buffer[i] = color2;
			}
			else
			{
				Color color4 = color;
				color4.a = (float)color3.a / 255f * color.a;
				cols.buffer[i] = ((this.bitmapFont != null && this.bitmapFont.premultipliedAlphaShader) ? NGUITools.ApplyPMA(color4) : color4);
			}
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0003AEEE File Offset: 0x000390EE
	public int CalculateOffsetToFit(string text)
	{
		this.UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0003AF14 File Offset: 0x00039114
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0003AF4B File Offset: 0x0003914B
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0003AF84 File Offset: 0x00039184
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = (UIPopupList.current.isLocalized ? Localization.Get(UIPopupList.current.value) : UIPopupList.current.value);
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0003AFC0 File Offset: 0x000391C0
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0003AFCF File Offset: 0x000391CF
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		NGUIText.rectHeight = height;
		bool result = NGUIText.WrapText(text, out final);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0003AFF0 File Offset: 0x000391F0
	public void UpdateNGUIText()
	{
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		NGUIText.fontSize = this.mPrintedSize;
		NGUIText.fontStyle = this.mFontStyle;
		NGUIText.rectWidth = this.mWidth;
		NGUIText.rectHeight = this.mHeight;
		NGUIText.gradient = (this.mApplyGradient && (this.mFont == null || !this.mFont.packedFontShader));
		NGUIText.gradientTop = this.mGradientTop;
		NGUIText.gradientBottom = this.mGradientBottom;
		NGUIText.encoding = this.mEncoding;
		NGUIText.premultiply = this.mPremultiply;
		NGUIText.symbolStyle = this.mSymbols;
		NGUIText.maxLines = this.mMaxLineCount;
		NGUIText.spacingX = (float)this.mSpacingX;
		NGUIText.spacingY = (float)this.mSpacingY;
		NGUIText.fontScale = (flag ? this.mScale : ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale));
		if (this.mFont != null)
		{
			NGUIText.bitmapFont = this.mFont;
			for (;;)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if (replacement == null)
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
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((root != null) ? root.pixelSizeAdjustment : 1f);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (this.mDensity != NGUIText.pixelDensity)
		{
			this.ProcessText(false, false);
			NGUIText.rectWidth = this.mWidth;
			NGUIText.rectHeight = this.mHeight;
		}
		if (this.alignment == NGUIText.Alignment.Automatic)
		{
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
			{
				NGUIText.alignment = NGUIText.Alignment.Left;
			}
			else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				NGUIText.alignment = NGUIText.Alignment.Right;
			}
			else
			{
				NGUIText.alignment = NGUIText.Alignment.Center;
			}
		}
		else
		{
			NGUIText.alignment = this.alignment;
		}
		NGUIText.Update();
	}

	// Token: 0x040005E4 RID: 1508
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x040005E5 RID: 1509
	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	// Token: 0x040005E6 RID: 1510
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x040005E7 RID: 1511
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = "";

	// Token: 0x040005E8 RID: 1512
	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	// Token: 0x040005E9 RID: 1513
	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	// Token: 0x040005EA RID: 1514
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x040005EB RID: 1515
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x040005EC RID: 1516
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x040005ED RID: 1517
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x040005EE RID: 1518
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x040005EF RID: 1519
	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x040005F0 RID: 1520
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x040005F1 RID: 1521
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x040005F2 RID: 1522
	[HideInInspector]
	[SerializeField]
	private Material mMaterial;

	// Token: 0x040005F3 RID: 1523
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x040005F4 RID: 1524
	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	// Token: 0x040005F5 RID: 1525
	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x040005F6 RID: 1526
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x040005F7 RID: 1527
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x040005F8 RID: 1528
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x040005F9 RID: 1529
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x040005FA RID: 1530
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x040005FB RID: 1531
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x040005FC RID: 1532
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x040005FD RID: 1533
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x040005FE RID: 1534
	private float mDensity = 1f;

	// Token: 0x040005FF RID: 1535
	private bool mShouldBeProcessed = true;

	// Token: 0x04000600 RID: 1536
	private string mProcessedText;

	// Token: 0x04000601 RID: 1537
	private bool mPremultiply;

	// Token: 0x04000602 RID: 1538
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x04000603 RID: 1539
	private float mScale = 1f;

	// Token: 0x04000604 RID: 1540
	private int mPrintedSize;

	// Token: 0x04000605 RID: 1541
	private int mLastWidth;

	// Token: 0x04000606 RID: 1542
	private int mLastHeight;

	// Token: 0x04000607 RID: 1543
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x04000608 RID: 1544
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x04000609 RID: 1545
	private static BetterList<Vector3> mTempVerts = new BetterList<Vector3>();

	// Token: 0x0400060A RID: 1546
	private static BetterList<int> mTempIndices = new BetterList<int>();

	// Token: 0x02001229 RID: 4649
	public enum Effect
	{
		// Token: 0x040064D2 RID: 25810
		None,
		// Token: 0x040064D3 RID: 25811
		Shadow,
		// Token: 0x040064D4 RID: 25812
		Outline
	}

	// Token: 0x0200122A RID: 4650
	public enum Overflow
	{
		// Token: 0x040064D6 RID: 25814
		ShrinkContent,
		// Token: 0x040064D7 RID: 25815
		ClampContent,
		// Token: 0x040064D8 RID: 25816
		ResizeFreely,
		// Token: 0x040064D9 RID: 25817
		ResizeHeight
	}

	// Token: 0x0200122B RID: 4651
	public enum Crispness
	{
		// Token: 0x040064DB RID: 25819
		Never,
		// Token: 0x040064DC RID: 25820
		OnDesktop,
		// Token: 0x040064DD RID: 25821
		Always
	}
}
