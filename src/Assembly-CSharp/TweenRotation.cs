using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000875 RID: 2165 RVA: 0x00032638 File Offset: 0x00030838
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

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000876 RID: 2166 RVA: 0x0003265A File Offset: 0x0003085A
	// (set) Token: 0x06000877 RID: 2167 RVA: 0x00032662 File Offset: 0x00030862
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

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000878 RID: 2168 RVA: 0x0003266B File Offset: 0x0003086B
	// (set) Token: 0x06000879 RID: 2169 RVA: 0x00032678 File Offset: 0x00030878
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

	// Token: 0x0600087A RID: 2170 RVA: 0x00032688 File Offset: 0x00030888
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor)));
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000326FC File Offset: 0x000308FC
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

	// Token: 0x0600087C RID: 2172 RVA: 0x00032750 File Offset: 0x00030950
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00032774 File Offset: 0x00030974
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00032795 File Offset: 0x00030995
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000327A8 File Offset: 0x000309A8
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x04000531 RID: 1329
	public Vector3 from;

	// Token: 0x04000532 RID: 1330
	public Vector3 to;

	// Token: 0x04000533 RID: 1331
	private Transform mTrans;
}
