using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x0600088D RID: 2189 RVA: 0x00032928 File Offset: 0x00030B28
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

	// Token: 0x0600088E RID: 2190 RVA: 0x00032AE3 File Offset: 0x00030CE3
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00032AF0 File Offset: 0x00030CF0
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

	// Token: 0x04000539 RID: 1337
	public Transform from;

	// Token: 0x0400053A RID: 1338
	public Transform to;

	// Token: 0x0400053B RID: 1339
	public bool parentWhenFinished;

	// Token: 0x0400053C RID: 1340
	private Transform mTrans;

	// Token: 0x0400053D RID: 1341
	private Vector3 mPos;

	// Token: 0x0400053E RID: 1342
	private Quaternion mRot;

	// Token: 0x0400053F RID: 1343
	private Vector3 mScale;
}
