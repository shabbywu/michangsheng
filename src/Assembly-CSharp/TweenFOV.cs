using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x1700012F RID: 303
	// (get) Token: 0x060008FE RID: 2302 RVA: 0x0000B54A File Offset: 0x0000974A
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x060008FF RID: 2303 RVA: 0x0000B56C File Offset: 0x0000976C
	// (set) Token: 0x06000900 RID: 2304 RVA: 0x0000B574 File Offset: 0x00009774
	[Obsolete("Use 'value' instead")]
	public float fov
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

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000901 RID: 2305 RVA: 0x0000B57D File Offset: 0x0000977D
	// (set) Token: 0x06000902 RID: 2306 RVA: 0x0000B58A File Offset: 0x0000978A
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0000B598 File Offset: 0x00009798
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00086620 File Offset: 0x00084820
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0000B5B7 File Offset: 0x000097B7
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0000B5C5 File Offset: 0x000097C5
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0000B5D3 File Offset: 0x000097D3
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0000B5E1 File Offset: 0x000097E1
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000644 RID: 1604
	public float from = 45f;

	// Token: 0x04000645 RID: 1605
	public float to = 45f;

	// Token: 0x04000646 RID: 1606
	private Camera mCam;
}
