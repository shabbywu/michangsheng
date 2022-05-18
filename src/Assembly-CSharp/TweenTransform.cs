using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x06000945 RID: 2373 RVA: 0x00086A04 File Offset: 0x00084C04
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0000B937 File Offset: 0x00009B37
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00086BC0 File Offset: 0x00084DC0
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x0400065C RID: 1628
	public Transform from;

	// Token: 0x0400065D RID: 1629
	public Transform to;

	// Token: 0x0400065E RID: 1630
	public bool parentWhenFinished;

	// Token: 0x0400065F RID: 1631
	private Transform mTrans;

	// Token: 0x04000660 RID: 1632
	private Vector3 mPos;

	// Token: 0x04000661 RID: 1633
	private Quaternion mRot;

	// Token: 0x04000662 RID: 1634
	private Vector3 mScale;
}
