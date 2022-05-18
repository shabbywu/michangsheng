using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000B752 File Offset: 0x00009952
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

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000921 RID: 2337 RVA: 0x0000B774 File Offset: 0x00009974
	// (set) Token: 0x06000922 RID: 2338 RVA: 0x0000B77C File Offset: 0x0000997C
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

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000923 RID: 2339 RVA: 0x0000B785 File Offset: 0x00009985
	// (set) Token: 0x06000924 RID: 2340 RVA: 0x00086770 File Offset: 0x00084970
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

	// Token: 0x06000925 RID: 2341 RVA: 0x0000B7A6 File Offset: 0x000099A6
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0000B7B4 File Offset: 0x000099B4
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x000867EC File Offset: 0x000849EC
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

	// Token: 0x06000928 RID: 2344 RVA: 0x0000B7DF File Offset: 0x000099DF
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0000B7ED File Offset: 0x000099ED
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0000B7FB File Offset: 0x000099FB
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0000B809 File Offset: 0x00009A09
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x0400064F RID: 1615
	public Vector3 from;

	// Token: 0x04000650 RID: 1616
	public Vector3 to;

	// Token: 0x04000651 RID: 1617
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x04000652 RID: 1618
	private Transform mTrans;

	// Token: 0x04000653 RID: 1619
	private UIRect mRect;
}
