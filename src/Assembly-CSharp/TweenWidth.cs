using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000953 RID: 2387 RVA: 0x0000BA22 File Offset: 0x00009C22
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000954 RID: 2388 RVA: 0x0000BA44 File Offset: 0x00009C44
	// (set) Token: 0x06000955 RID: 2389 RVA: 0x0000BA4C File Offset: 0x00009C4C
	[Obsolete("Use 'value' instead")]
	public int width
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

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000956 RID: 2390 RVA: 0x0000BA55 File Offset: 0x00009C55
	// (set) Token: 0x06000957 RID: 2391 RVA: 0x0000BA62 File Offset: 0x00009C62
	public int value
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00086C68 File Offset: 0x00084E68
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00086CE4 File Offset: 0x00084EE4
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0000BA70 File Offset: 0x00009C70
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0000BA7E File Offset: 0x00009C7E
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0000BA8C File Offset: 0x00009C8C
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0000BA9A File Offset: 0x00009C9A
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000666 RID: 1638
	public int from = 100;

	// Token: 0x04000667 RID: 1639
	public int to = 100;

	// Token: 0x04000668 RID: 1640
	public bool updateTable;

	// Token: 0x04000669 RID: 1641
	private UIWidget mWidget;

	// Token: 0x0400066A RID: 1642
	private UITable mTable;
}
