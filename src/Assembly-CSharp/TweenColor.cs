using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x0600083A RID: 2106 RVA: 0x00031F08 File Offset: 0x00030108
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

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600083B RID: 2107 RVA: 0x00031F86 File Offset: 0x00030186
	// (set) Token: 0x0600083C RID: 2108 RVA: 0x00031F8E File Offset: 0x0003018E
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

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600083D RID: 2109 RVA: 0x00031F98 File Offset: 0x00030198
	// (set) Token: 0x0600083E RID: 2110 RVA: 0x00032008 File Offset: 0x00030208
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

	// Token: 0x0600083F RID: 2111 RVA: 0x00032097 File Offset: 0x00030297
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x000320B4 File Offset: 0x000302B4
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

	// Token: 0x06000841 RID: 2113 RVA: 0x000320F8 File Offset: 0x000302F8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00032106 File Offset: 0x00030306
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00032114 File Offset: 0x00030314
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00032122 File Offset: 0x00030322
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400051B RID: 1307
	public Color from = Color.white;

	// Token: 0x0400051C RID: 1308
	public Color to = Color.white;

	// Token: 0x0400051D RID: 1309
	private bool mCached;

	// Token: 0x0400051E RID: 1310
	private UIWidget mWidget;

	// Token: 0x0400051F RID: 1311
	private Material mMat;

	// Token: 0x04000520 RID: 1312
	private Light mLight;
}
