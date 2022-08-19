using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x0003214E File Offset: 0x0003034E
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

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000847 RID: 2119 RVA: 0x00032170 File Offset: 0x00030370
	// (set) Token: 0x06000848 RID: 2120 RVA: 0x00032178 File Offset: 0x00030378
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

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000849 RID: 2121 RVA: 0x00032181 File Offset: 0x00030381
	// (set) Token: 0x0600084A RID: 2122 RVA: 0x0003218E File Offset: 0x0003038E
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

	// Token: 0x0600084B RID: 2123 RVA: 0x0003219C File Offset: 0x0003039C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000321BC File Offset: 0x000303BC
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

	// Token: 0x0600084D RID: 2125 RVA: 0x00032200 File Offset: 0x00030400
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0003220E File Offset: 0x0003040E
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0003221C File Offset: 0x0003041C
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0003222A File Offset: 0x0003042A
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000521 RID: 1313
	public float from = 45f;

	// Token: 0x04000522 RID: 1314
	public float to = 45f;

	// Token: 0x04000523 RID: 1315
	private Camera mCam;
}
