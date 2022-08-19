using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00032C77 File Offset: 0x00030E77
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

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x0600089C RID: 2204 RVA: 0x00032C99 File Offset: 0x00030E99
	// (set) Token: 0x0600089D RID: 2205 RVA: 0x00032CA1 File Offset: 0x00030EA1
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

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600089E RID: 2206 RVA: 0x00032CAA File Offset: 0x00030EAA
	// (set) Token: 0x0600089F RID: 2207 RVA: 0x00032CB7 File Offset: 0x00030EB7
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

	// Token: 0x060008A0 RID: 2208 RVA: 0x00032CC8 File Offset: 0x00030EC8
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

	// Token: 0x060008A1 RID: 2209 RVA: 0x00032D44 File Offset: 0x00030F44
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

	// Token: 0x060008A2 RID: 2210 RVA: 0x00032D8D File Offset: 0x00030F8D
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00032D9B File Offset: 0x00030F9B
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00032DA9 File Offset: 0x00030FA9
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00032DB7 File Offset: 0x00030FB7
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000543 RID: 1347
	public int from = 100;

	// Token: 0x04000544 RID: 1348
	public int to = 100;

	// Token: 0x04000545 RID: 1349
	public bool updateTable;

	// Token: 0x04000546 RID: 1350
	private UIWidget mWidget;

	// Token: 0x04000547 RID: 1351
	private UITable mTable;
}
