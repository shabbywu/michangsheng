using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x0008645C File Offset: 0x0008465C
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
		}
		this.mLight = base.GetComponent<Light>();
		if (this.mWidget == null && this.mMat == null && this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000B4C9 File Offset: 0x000096C9
	// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0000B4D1 File Offset: 0x000096D1
	[Obsolete("Use 'value' instead")]
	public Color color
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

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000864DC File Offset: 0x000846DC
	// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0008654C File Offset: 0x0008474C
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0000B4DA File Offset: 0x000096DA
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x000865DC File Offset: 0x000847DC
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0000B4F4 File Offset: 0x000096F4
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0000B502 File Offset: 0x00009702
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0000B510 File Offset: 0x00009710
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0000B51E File Offset: 0x0000971E
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400063E RID: 1598
	public Color from = Color.white;

	// Token: 0x0400063F RID: 1599
	public Color to = Color.white;

	// Token: 0x04000640 RID: 1600
	private bool mCached;

	// Token: 0x04000641 RID: 1601
	private UIWidget mWidget;

	// Token: 0x04000642 RID: 1602
	private Material mMat;

	// Token: 0x04000643 RID: 1603
	private Light mLight;
}
