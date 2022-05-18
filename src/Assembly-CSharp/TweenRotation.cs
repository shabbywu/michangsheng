using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000B81F File Offset: 0x00009A1F
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

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x0600092E RID: 2350 RVA: 0x0000B841 File Offset: 0x00009A41
	// (set) Token: 0x0600092F RID: 2351 RVA: 0x0000B849 File Offset: 0x00009A49
	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
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

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000930 RID: 2352 RVA: 0x0000B852 File Offset: 0x00009A52
	// (set) Token: 0x06000931 RID: 2353 RVA: 0x0000B85F File Offset: 0x00009A5F
	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00086830 File Offset: 0x00084A30
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor)));
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x000868A4 File Offset: 0x00084AA4
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x000868F8 File Offset: 0x00084AF8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0008691C File Offset: 0x00084B1C
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0000B86D File Offset: 0x00009A6D
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0000B880 File Offset: 0x00009A80
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x04000654 RID: 1620
	public Vector3 from;

	// Token: 0x04000655 RID: 1621
	public Vector3 to;

	// Token: 0x04000656 RID: 1622
	private Transform mTrans;
}
