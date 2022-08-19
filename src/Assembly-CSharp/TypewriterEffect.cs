using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200004F RID: 79
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Interaction/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001873B File Offset: 0x0001693B
	public bool isActive
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00018743 File Offset: 0x00016943
	public void ResetToBeginning()
	{
		this.mReset = true;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001874C File Offset: 0x0001694C
	public void Finish()
	{
		if (this.mActive)
		{
			this.mActive = false;
			if (!this.mReset)
			{
				this.mCurrentOffset = this.mFullText.Length;
				this.mFade.Clear();
				this.mLabel.text = this.mFullText;
			}
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000187D5 File Offset: 0x000169D5
	private void OnEnable()
	{
		this.mReset = true;
		this.mActive = true;
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x000187E8 File Offset: 0x000169E8
	private void Update()
	{
		if (!this.mActive)
		{
			return;
		}
		if (this.mReset)
		{
			this.mCurrentOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mFullText = this.mLabel.processedText;
			this.mFade.Clear();
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
		}
		while (this.mCurrentOffset < this.mFullText.Length && this.mNextChar <= RealTime.time)
		{
			int num = this.mCurrentOffset;
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			while (NGUIText.ParseSymbol(this.mFullText, ref this.mCurrentOffset))
			{
			}
			this.mCurrentOffset++;
			float num2 = 1f / (float)this.charsPerSecond;
			char c = (num < this.mFullText.Length) ? this.mFullText[num] : '\n';
			if (c == '\n')
			{
				num2 += this.delayOnNewLine;
			}
			else if (num + 1 == this.mFullText.Length || this.mFullText[num + 1] <= ' ')
			{
				if (c == '.')
				{
					if (num + 2 < this.mFullText.Length && this.mFullText[num + 1] == '.' && this.mFullText[num + 2] == '.')
					{
						num2 += this.delayOnPeriod * 3f;
						num += 2;
					}
					else
					{
						num2 += this.delayOnPeriod;
					}
				}
				else if (c == '!' || c == '?')
				{
					num2 += this.delayOnPeriod;
				}
			}
			if (this.mNextChar == 0f)
			{
				this.mNextChar = RealTime.time + num2;
			}
			else
			{
				this.mNextChar += num2;
			}
			if (this.fadeInTime != 0f)
			{
				TypewriterEffect.FadeEntry item = default(TypewriterEffect.FadeEntry);
				item.index = num;
				item.alpha = 0f;
				item.text = this.mFullText.Substring(num, this.mCurrentOffset - num);
				this.mFade.Add(item);
			}
			else
			{
				this.mLabel.text = (this.keepFullDimensions ? (this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset)) : this.mFullText.Substring(0, this.mCurrentOffset));
				if (!this.keepFullDimensions && this.scrollView != null)
				{
					this.scrollView.UpdatePosition();
				}
			}
		}
		if (this.mFade.size == 0)
		{
			if (this.mCurrentOffset == this.mFullText.Length)
			{
				TypewriterEffect.current = this;
				EventDelegate.Execute(this.onFinished);
				TypewriterEffect.current = null;
				this.mActive = false;
			}
			return;
		}
		int i = 0;
		while (i < this.mFade.size)
		{
			TypewriterEffect.FadeEntry fadeEntry = this.mFade[i];
			fadeEntry.alpha += RealTime.deltaTime / this.fadeInTime;
			if (fadeEntry.alpha < 1f)
			{
				this.mFade[i] = fadeEntry;
				i++;
			}
			else
			{
				this.mFade.RemoveAt(i);
			}
		}
		if (this.mFade.size != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < this.mFade.size; j++)
			{
				TypewriterEffect.FadeEntry fadeEntry2 = this.mFade[j];
				if (j == 0)
				{
					stringBuilder.Append(this.mFullText.Substring(0, fadeEntry2.index));
				}
				stringBuilder.Append('[');
				stringBuilder.Append(NGUIText.EncodeAlpha(fadeEntry2.alpha));
				stringBuilder.Append(']');
				stringBuilder.Append(fadeEntry2.text);
			}
			if (this.keepFullDimensions)
			{
				stringBuilder.Append("[00]");
				stringBuilder.Append(this.mFullText.Substring(this.mCurrentOffset));
			}
			this.mLabel.text = stringBuilder.ToString();
			return;
		}
		if (this.keepFullDimensions)
		{
			this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset);
			return;
		}
		this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset);
	}

	// Token: 0x0400029A RID: 666
	public static TypewriterEffect current;

	// Token: 0x0400029B RID: 667
	public int charsPerSecond = 20;

	// Token: 0x0400029C RID: 668
	public float fadeInTime;

	// Token: 0x0400029D RID: 669
	public float delayOnPeriod;

	// Token: 0x0400029E RID: 670
	public float delayOnNewLine;

	// Token: 0x0400029F RID: 671
	public UIScrollView scrollView;

	// Token: 0x040002A0 RID: 672
	public bool keepFullDimensions;

	// Token: 0x040002A1 RID: 673
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040002A2 RID: 674
	private UILabel mLabel;

	// Token: 0x040002A3 RID: 675
	private string mFullText = "";

	// Token: 0x040002A4 RID: 676
	private int mCurrentOffset;

	// Token: 0x040002A5 RID: 677
	private float mNextChar;

	// Token: 0x040002A6 RID: 678
	private bool mReset = true;

	// Token: 0x040002A7 RID: 679
	private bool mActive;

	// Token: 0x040002A8 RID: 680
	private BetterList<TypewriterEffect.FadeEntry> mFade = new BetterList<TypewriterEffect.FadeEntry>();

	// Token: 0x020011DC RID: 4572
	private struct FadeEntry
	{
		// Token: 0x040063BF RID: 25535
		public int index;

		// Token: 0x040063C0 RID: 25536
		public string text;

		// Token: 0x040063C1 RID: 25537
		public float alpha;
	}
}
