using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000881 RID: 2177 RVA: 0x000327BB File Offset: 0x000309BB
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

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000882 RID: 2178 RVA: 0x000327DD File Offset: 0x000309DD
	// (set) Token: 0x06000883 RID: 2179 RVA: 0x000327EA File Offset: 0x000309EA
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

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000884 RID: 2180 RVA: 0x000327F8 File Offset: 0x000309F8
	// (set) Token: 0x06000885 RID: 2181 RVA: 0x00032800 File Offset: 0x00030A00
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

	// Token: 0x06000886 RID: 2182 RVA: 0x0003280C File Offset: 0x00030A0C
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

	// Token: 0x06000887 RID: 2183 RVA: 0x0003288C File Offset: 0x00030A8C
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

	// Token: 0x06000888 RID: 2184 RVA: 0x000328D0 File Offset: 0x00030AD0
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000328DE File Offset: 0x00030ADE
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x000328EC File Offset: 0x00030AEC
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x000328FA File Offset: 0x00030AFA
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000534 RID: 1332
	public Vector3 from = Vector3.one;

	// Token: 0x04000535 RID: 1333
	public Vector3 to = Vector3.one;

	// Token: 0x04000536 RID: 1334
	public bool updateTable;

	// Token: 0x04000537 RID: 1335
	private Transform mTrans;

	// Token: 0x04000538 RID: 1336
	private UITable mTable;
}
