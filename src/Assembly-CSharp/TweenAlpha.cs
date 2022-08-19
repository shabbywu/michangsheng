using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x00031E06 File Offset: 0x00030006
	public UIRect cachedRect
	{
		get
		{
			if (this.mRect == null)
			{
				this.mRect = base.GetComponent<UIRect>();
				if (this.mRect == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
				}
			}
			return this.mRect;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000831 RID: 2097 RVA: 0x00031E42 File Offset: 0x00030042
	// (set) Token: 0x06000832 RID: 2098 RVA: 0x00031E4A File Offset: 0x0003004A
	[Obsolete("Use 'value' instead")]
	public float alpha
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000833 RID: 2099 RVA: 0x00031E53 File Offset: 0x00030053
	// (set) Token: 0x06000834 RID: 2100 RVA: 0x00031E60 File Offset: 0x00030060
	public float value
	{
		get
		{
			return this.cachedRect.alpha;
		}
		set
		{
			this.cachedRect.alpha = value;
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00031E6E File Offset: 0x0003006E
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00031E88 File Offset: 0x00030088
	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00031ECC File Offset: 0x000300CC
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00031EDA File Offset: 0x000300DA
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000518 RID: 1304
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000519 RID: 1305
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x0400051A RID: 1306
	private UIRect mRect;
}
