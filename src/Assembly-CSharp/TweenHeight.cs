using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600090A RID: 2314 RVA: 0x0000B60D File Offset: 0x0000980D
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

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x0000B62F File Offset: 0x0000982F
	// (set) Token: 0x0600090C RID: 2316 RVA: 0x0000B637 File Offset: 0x00009837
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

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x0600090D RID: 2317 RVA: 0x0000B640 File Offset: 0x00009840
	// (set) Token: 0x0600090E RID: 2318 RVA: 0x0000B64D File Offset: 0x0000984D
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

	// Token: 0x0600090F RID: 2319 RVA: 0x00086664 File Offset: 0x00084864
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

	// Token: 0x06000910 RID: 2320 RVA: 0x000866E0 File Offset: 0x000848E0
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

	// Token: 0x06000911 RID: 2321 RVA: 0x0000B65B File Offset: 0x0000985B
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0000B669 File Offset: 0x00009869
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0000B677 File Offset: 0x00009877
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0000B685 File Offset: 0x00009885
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000647 RID: 1607
	public int from = 100;

	// Token: 0x04000648 RID: 1608
	public int to = 100;

	// Token: 0x04000649 RID: 1609
	public bool updateTable;

	// Token: 0x0400064A RID: 1610
	private UIWidget mWidget;

	// Token: 0x0400064B RID: 1611
	private UITable mTable;
}
