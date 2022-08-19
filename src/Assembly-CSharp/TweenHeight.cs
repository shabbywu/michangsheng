using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x00032256 File Offset: 0x00030456
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

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000853 RID: 2131 RVA: 0x00032278 File Offset: 0x00030478
	// (set) Token: 0x06000854 RID: 2132 RVA: 0x00032280 File Offset: 0x00030480
	[Obsolete("Use 'value' instead")]
	public int height
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

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000855 RID: 2133 RVA: 0x00032289 File Offset: 0x00030489
	// (set) Token: 0x06000856 RID: 2134 RVA: 0x00032296 File Offset: 0x00030496
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x000322A4 File Offset: 0x000304A4
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

	// Token: 0x06000858 RID: 2136 RVA: 0x00032320 File Offset: 0x00030520
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00032369 File Offset: 0x00030569
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00032377 File Offset: 0x00030577
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00032385 File Offset: 0x00030585
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00032393 File Offset: 0x00030593
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000524 RID: 1316
	public int from = 100;

	// Token: 0x04000525 RID: 1317
	public int to = 100;

	// Token: 0x04000526 RID: 1318
	public bool updateTable;

	// Token: 0x04000527 RID: 1319
	private UIWidget mWidget;

	// Token: 0x04000528 RID: 1320
	private UITable mTable;
}
