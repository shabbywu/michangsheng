using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600085E RID: 2142 RVA: 0x000323B9 File Offset: 0x000305B9
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

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600085F RID: 2143 RVA: 0x000323DB File Offset: 0x000305DB
	// (set) Token: 0x06000860 RID: 2144 RVA: 0x000323E3 File Offset: 0x000305E3
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

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000861 RID: 2145 RVA: 0x000323EC File Offset: 0x000305EC
	// (set) Token: 0x06000862 RID: 2146 RVA: 0x000323F9 File Offset: 0x000305F9
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

	// Token: 0x06000863 RID: 2147 RVA: 0x00032407 File Offset: 0x00030607
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00032428 File Offset: 0x00030628
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

	// Token: 0x06000865 RID: 2149 RVA: 0x0003246C File Offset: 0x0003066C
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003247A File Offset: 0x0003067A
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000529 RID: 1321
	public float from = 1f;

	// Token: 0x0400052A RID: 1322
	public float to = 1f;

	// Token: 0x0400052B RID: 1323
	private Camera mCam;
}
