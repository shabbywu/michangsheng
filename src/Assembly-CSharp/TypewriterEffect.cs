using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000067 RID: 103
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Interaction/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000827B File Offset: 0x0000647B
	public bool isActive
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00008283 File Offset: 0x00006483
	public void ResetToBeginning()
	{
		this.mReset = true;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0006FA34 File Offset: 0x0006DC34
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

	// Token: 0x060004C3 RID: 1219 RVA: 0x0000828C File Offset: 0x0000648C
	private void OnEnable()
	{
		this.mReset = true;
		this.mActive = true;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0006FAC0 File Offset: 0x0006DCC0
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

	// Token: 0x0400030A RID: 778
	public static TypewriterEffect current;

	// Token: 0x0400030B RID: 779
	public int charsPerSecond = 20;

	// Token: 0x0400030C RID: 780
	public float fadeInTime;

	// Token: 0x0400030D RID: 781
	public float delayOnPeriod;

	// Token: 0x0400030E RID: 782
	public float delayOnNewLine;

	// Token: 0x0400030F RID: 783
	public UIScrollView scrollView;

	// Token: 0x04000310 RID: 784
	public bool keepFullDimensions;

	// Token: 0x04000311 RID: 785
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000312 RID: 786
	private UILabel mLabel;

	// Token: 0x04000313 RID: 787
	private string mFullText = "";

	// Token: 0x04000314 RID: 788
	private int mCurrentOffset;

	// Token: 0x04000315 RID: 789
	private float mNextChar;

	// Token: 0x04000316 RID: 790
	private bool mReset = true;

	// Token: 0x04000317 RID: 791
	private bool mActive;

	// Token: 0x04000318 RID: 792
	private BetterList<TypewriterEffect.FadeEntry> mFade = new BetterList<TypewriterEffect.FadeEntry>();

	// Token: 0x02000068 RID: 104
	private struct FadeEntry
	{
		// Token: 0x04000319 RID: 793
		public int index;

		// Token: 0x0400031A RID: 794
		public string text;

		// Token: 0x0400031B RID: 795
		public float alpha;
	}
}
