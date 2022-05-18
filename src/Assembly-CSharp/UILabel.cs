using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010E RID: 270
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0000C8FC File Offset: 0x0000AAFC
	// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0000C904 File Offset: 0x0000AB04
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

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0000C91F File Offset: 0x0000AB1F
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0000C934 File Offset: 0x0000AB34
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0008C9E4 File Offset: 0x0008ABE4
	// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0000C952 File Offset: 0x0000AB52
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

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0000C975 File Offset: 0x0000AB75
	// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0000C97D File Offset: 0x0000AB7D
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

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0000C986 File Offset: 0x0000AB86
	// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0000C98E File Offset: 0x0000AB8E
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

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
	// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0008CA3C File Offset: 0x0008AC3C
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

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0000C9EA File Offset: 0x0000ABEA
	// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0008CA9C File Offset: 0x0008AC9C
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

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0000CA07 File Offset: 0x0000AC07
	// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0008CAD0 File Offset: 0x0008ACD0
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

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0000CA0F File Offset: 0x0000AC0F
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

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0000CA42 File Offset: 0x0000AC42
	// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0000CA4A File Offset: 0x0000AC4A
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

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0000CA77 File Offset: 0x0000AC77
	// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0000CA7F File Offset: 0x0000AC7F
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

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0000CA9E File Offset: 0x0000AC9E
	// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0000CAA6 File Offset: 0x0000ACA6
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

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
	// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0000CACD File Offset: 0x0000ACCD
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

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
	// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0000CAED File Offset: 0x0000ACED
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

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0000CB12 File Offset: 0x0000AD12
	// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0000CB1A File Offset: 0x0000AD1A
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

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0000CB3F File Offset: 0x0000AD3F
	// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0000CB47 File Offset: 0x0000AD47
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

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0000CB5F File Offset: 0x0000AD5F
	// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0000CB67 File Offset: 0x0000AD67
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

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0000CB7F File Offset: 0x0000AD7F
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0000CB9A File Offset: 0x0000AD9A
	// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
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

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0000CBBB File Offset: 0x0000ADBB
	// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0000CBC3 File Offset: 0x0000ADC3
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

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0000CBDC File Offset: 0x0000ADDC
	// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
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

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0000CBFD File Offset: 0x0000ADFD
	// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0000CC05 File Offset: 0x0000AE05
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

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0000CC0E File Offset: 0x0000AE0E
	// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0000CC16 File Offset: 0x0000AE16
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

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0000CC1F File Offset: 0x0000AE1F
	// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0000CC2D File Offset: 0x0000AE2D
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

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0000CC52 File Offset: 0x0000AE52
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

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0000CC68 File Offset: 0x0000AE68
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

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0000CC7E File Offset: 0x0000AE7E
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

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0000CC94 File Offset: 0x0000AE94
	// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0000CC9C File Offset: 0x0000AE9C
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

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0000CCC9 File Offset: 0x0000AEC9
	// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0000CCD1 File Offset: 0x0000AED1
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

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0000CCEA File Offset: 0x0000AEEA
	// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
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

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0000CD18 File Offset: 0x0000AF18
	// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0000CD20 File Offset: 0x0000AF20
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

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0000CD3E File Offset: 0x0000AF3E
	// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0000CD49 File Offset: 0x0000AF49
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

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0008CB4C File Offset: 0x0008AD4C
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

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0000CD55 File Offset: 0x0000AF55
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

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0000CD6B File Offset: 0x0000AF6B
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

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0000CD81 File Offset: 0x0000AF81
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0000CD9F File Offset: 0x0000AF9F
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0000CDBE File Offset: 0x0000AFBE
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0008CBA8 File Offset: 0x0008ADA8
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

	// Token: 0x06000A8B RID: 2699 RVA: 0x0008CC8C File Offset: 0x0008AE8C
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

	// Token: 0x06000A8C RID: 2700 RVA: 0x0000CDD9 File Offset: 0x0000AFD9
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText();
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0008CCF4 File Offset: 0x0008AEF4
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

	// Token: 0x06000A8E RID: 2702 RVA: 0x0008CDB0 File Offset: 0x0008AFB0
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

	// Token: 0x06000A8F RID: 2703 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText();
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0008CE14 File Offset: 0x0008B014
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

	// Token: 0x06000A91 RID: 2705 RVA: 0x0000CE06 File Offset: 0x0000B006
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0000CE15 File Offset: 0x0000B015
	public void ProcessText()
	{
		this.ProcessText(false, true);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0008CEB0 File Offset: 0x0008B0B0
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

	// Token: 0x06000A94 RID: 2708 RVA: 0x0008D220 File Offset: 0x0008B420
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

	// Token: 0x06000A95 RID: 2709 RVA: 0x0008D360 File Offset: 0x0008B560
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

	// Token: 0x06000A96 RID: 2710 RVA: 0x0000CE1F File Offset: 0x0000B01F
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos);
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0000CE28 File Offset: 0x0000B028
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0008D400 File Offset: 0x0008B600
	public int GetCharacterIndexAtPosition(Vector3 worldPos)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0008D428 File Offset: 0x0008B628
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

	// Token: 0x06000A9A RID: 2714 RVA: 0x0000CE31 File Offset: 0x0000B031
	public string GetWordAtPosition(Vector3 worldPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0000CE40 File Offset: 0x0000B040
	public string GetWordAtPosition(Vector2 localPos)
	{
		return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0008D4C8 File Offset: 0x0008B6C8
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

	// Token: 0x06000A9D RID: 2717 RVA: 0x0000CE4F File Offset: 0x0000B04F
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos));
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0000CE5E File Offset: 0x0000B05E
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos));
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0008D538 File Offset: 0x0008B738
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

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0008D598 File Offset: 0x0008B798
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

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0008D728 File Offset: 0x0008B928
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

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0008D8C0 File Offset: 0x0008BAC0
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

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0008DA44 File Offset: 0x0008BC44
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

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0008DB00 File Offset: 0x0008BD00
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

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0000CE6D File Offset: 0x0000B06D
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

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0008DC50 File Offset: 0x0008BE50
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0000CE93 File Offset: 0x0000B093
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0000CECC File Offset: 0x0000B0CC
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = (UIPopupList.current.isLocalized ? Localization.Get(UIPopupList.current.value) : UIPopupList.current.value);
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0000CF08 File Offset: 0x0000B108
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0000CF17 File Offset: 0x0000B117
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		NGUIText.rectHeight = height;
		bool result = NGUIText.WrapText(text, out final);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0008DC88 File Offset: 0x0008BE88
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

	// Token: 0x04000766 RID: 1894
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x04000767 RID: 1895
	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	// Token: 0x04000768 RID: 1896
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x04000769 RID: 1897
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = "";

	// Token: 0x0400076A RID: 1898
	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	// Token: 0x0400076B RID: 1899
	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	// Token: 0x0400076C RID: 1900
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x0400076D RID: 1901
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x0400076E RID: 1902
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x0400076F RID: 1903
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x04000770 RID: 1904
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x04000771 RID: 1905
	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x04000772 RID: 1906
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x04000773 RID: 1907
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x04000774 RID: 1908
	[HideInInspector]
	[SerializeField]
	private Material mMaterial;

	// Token: 0x04000775 RID: 1909
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x04000776 RID: 1910
	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	// Token: 0x04000777 RID: 1911
	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x04000778 RID: 1912
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x04000779 RID: 1913
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x0400077A RID: 1914
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x0400077B RID: 1915
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x0400077C RID: 1916
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x0400077D RID: 1917
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x0400077E RID: 1918
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x0400077F RID: 1919
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x04000780 RID: 1920
	private float mDensity = 1f;

	// Token: 0x04000781 RID: 1921
	private bool mShouldBeProcessed = true;

	// Token: 0x04000782 RID: 1922
	private string mProcessedText;

	// Token: 0x04000783 RID: 1923
	private bool mPremultiply;

	// Token: 0x04000784 RID: 1924
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x04000785 RID: 1925
	private float mScale = 1f;

	// Token: 0x04000786 RID: 1926
	private int mPrintedSize;

	// Token: 0x04000787 RID: 1927
	private int mLastWidth;

	// Token: 0x04000788 RID: 1928
	private int mLastHeight;

	// Token: 0x04000789 RID: 1929
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x0400078A RID: 1930
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x0400078B RID: 1931
	private static BetterList<Vector3> mTempVerts = new BetterList<Vector3>();

	// Token: 0x0400078C RID: 1932
	private static BetterList<int> mTempIndices = new BetterList<int>();

	// Token: 0x0200010F RID: 271
	public enum Effect
	{
		// Token: 0x0400078E RID: 1934
		None,
		// Token: 0x0400078F RID: 1935
		Shadow,
		// Token: 0x04000790 RID: 1936
		Outline
	}

	// Token: 0x02000110 RID: 272
	public enum Overflow
	{
		// Token: 0x04000792 RID: 1938
		ShrinkContent,
		// Token: 0x04000793 RID: 1939
		ClampContent,
		// Token: 0x04000794 RID: 1940
		ResizeFreely,
		// Token: 0x04000795 RID: 1941
		ResizeHeight
	}

	// Token: 0x02000111 RID: 273
	public enum Crispness
	{
		// Token: 0x04000797 RID: 1943
		Never,
		// Token: 0x04000798 RID: 1944
		OnDesktop,
		// Token: 0x04000799 RID: 1945
		Always
	}
}
