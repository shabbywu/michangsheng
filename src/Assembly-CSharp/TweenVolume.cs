using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000949 RID: 2377 RVA: 0x00086C00 File Offset: 0x00084E00
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.GetComponent<AudioSource>();
				if (this.mSource == null)
				{
					this.mSource = base.GetComponent<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600094A RID: 2378 RVA: 0x0000B942 File Offset: 0x00009B42
	// (set) Token: 0x0600094B RID: 2379 RVA: 0x0000B94A File Offset: 0x00009B4A
	[Obsolete("Use 'value' instead")]
	public float volume
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

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000B953 File Offset: 0x00009B53
	// (set) Token: 0x0600094D RID: 2381 RVA: 0x0000B974 File Offset: 0x00009B74
	public float value
	{
		get
		{
			if (!(this.audioSource != null))
			{
				return 0f;
			}
			return this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0000B990 File Offset: 0x00009B90
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0000B9CC File Offset: 0x00009BCC
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0000B9E8 File Offset: 0x00009BE8
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0000B9F6 File Offset: 0x00009BF6
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000663 RID: 1635
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000664 RID: 1636
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000665 RID: 1637
	private AudioSource mSource;
}
