using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000916 RID: 2326 RVA: 0x0000B6AB File Offset: 0x000098AB
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

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000917 RID: 2327 RVA: 0x0000B6CD File Offset: 0x000098CD
	// (set) Token: 0x06000918 RID: 2328 RVA: 0x0000B6D5 File Offset: 0x000098D5
	[Obsolete("Use 'value' instead")]
	public float orthoSize
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

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000919 RID: 2329 RVA: 0x0000B6DE File Offset: 0x000098DE
	// (set) Token: 0x0600091A RID: 2330 RVA: 0x0000B6EB File Offset: 0x000098EB
	public float value
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0000B6F9 File Offset: 0x000098F9
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0008672C File Offset: 0x0008492C
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
		tweenOrthoSize.from = tweenOrthoSize.value;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0000B718 File Offset: 0x00009918
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0000B726 File Offset: 0x00009926
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0400064C RID: 1612
	public float from = 1f;

	// Token: 0x0400064D RID: 1613
	public float to = 1f;

	// Token: 0x0400064E RID: 1614
	private Camera mCam;
}
