using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0000B40D File Offset: 0x0000960D
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

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0000B449 File Offset: 0x00009649
	// (set) Token: 0x060008EA RID: 2282 RVA: 0x0000B451 File Offset: 0x00009651
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

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060008EB RID: 2283 RVA: 0x0000B45A File Offset: 0x0000965A
	// (set) Token: 0x060008EC RID: 2284 RVA: 0x0000B467 File Offset: 0x00009667
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

	// Token: 0x060008ED RID: 2285 RVA: 0x0000B475 File Offset: 0x00009675
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00086418 File Offset: 0x00084618
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

	// Token: 0x060008EF RID: 2287 RVA: 0x0000B48F File Offset: 0x0000968F
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0000B49D File Offset: 0x0000969D
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0400063B RID: 1595
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x0400063C RID: 1596
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x0400063D RID: 1597
	private UIRect mRect;
}
