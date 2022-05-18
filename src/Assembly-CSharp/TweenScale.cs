using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000939 RID: 2361 RVA: 0x0000B893 File Offset: 0x00009A93
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600093A RID: 2362 RVA: 0x0000B8B5 File Offset: 0x00009AB5
	// (set) Token: 0x0600093B RID: 2363 RVA: 0x0000B8C2 File Offset: 0x00009AC2
	public Vector3 value
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600093C RID: 2364 RVA: 0x0000B8D0 File Offset: 0x00009AD0
	// (set) Token: 0x0600093D RID: 2365 RVA: 0x0000B8D8 File Offset: 0x00009AD8
	[Obsolete("Use 'value' instead")]
	public Vector3 scale
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

	// Token: 0x0600093E RID: 2366 RVA: 0x00086940 File Offset: 0x00084B40
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
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

	// Token: 0x0600093F RID: 2367 RVA: 0x000869C0 File Offset: 0x00084BC0
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0000B8E1 File Offset: 0x00009AE1
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0000B8EF File Offset: 0x00009AEF
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0000B8FD File Offset: 0x00009AFD
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0000B90B File Offset: 0x00009B0B
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000657 RID: 1623
	public Vector3 from = Vector3.one;

	// Token: 0x04000658 RID: 1624
	public Vector3 to = Vector3.one;

	// Token: 0x04000659 RID: 1625
	public bool updateTable;

	// Token: 0x0400065A RID: 1626
	private Transform mTrans;

	// Token: 0x0400065B RID: 1627
	private UITable mTable;
}
