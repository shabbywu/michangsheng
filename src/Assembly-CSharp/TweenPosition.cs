using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000868 RID: 2152 RVA: 0x000324A6 File Offset: 0x000306A6
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

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000869 RID: 2153 RVA: 0x000324C8 File Offset: 0x000306C8
	// (set) Token: 0x0600086A RID: 2154 RVA: 0x000324D0 File Offset: 0x000306D0
	[Obsolete("Use 'value' instead")]
	public Vector3 position
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

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x0600086B RID: 2155 RVA: 0x000324D9 File Offset: 0x000306D9
	// (set) Token: 0x0600086C RID: 2156 RVA: 0x000324FC File Offset: 0x000306FC
	public Vector3 value
	{
		get
		{
			if (!this.worldSpace)
			{
				return this.cachedTransform.localPosition;
			}
			return this.cachedTransform.position;
		}
		set
		{
			if (!(this.mRect == null) && this.mRect.isAnchored && !this.worldSpace)
			{
				value -= this.cachedTransform.localPosition;
				NGUIMath.MoveRect(this.mRect, value.x, value.y);
				return;
			}
			if (this.worldSpace)
			{
				this.cachedTransform.position = value;
				return;
			}
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00032578 File Offset: 0x00030778
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00032586 File Offset: 0x00030786
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000325B4 File Offset: 0x000307B4
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000325F8 File Offset: 0x000307F8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00032606 File Offset: 0x00030806
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00032614 File Offset: 0x00030814
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00032622 File Offset: 0x00030822
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400052C RID: 1324
	public Vector3 from;

	// Token: 0x0400052D RID: 1325
	public Vector3 to;

	// Token: 0x0400052E RID: 1326
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x0400052F RID: 1327
	private Transform mTrans;

	// Token: 0x04000530 RID: 1328
	private UIRect mRect;
}
