using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000891 RID: 2193 RVA: 0x00032B30 File Offset: 0x00030D30
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

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000892 RID: 2194 RVA: 0x00032B97 File Offset: 0x00030D97
	// (set) Token: 0x06000893 RID: 2195 RVA: 0x00032B9F File Offset: 0x00030D9F
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

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000894 RID: 2196 RVA: 0x00032BA8 File Offset: 0x00030DA8
	// (set) Token: 0x06000895 RID: 2197 RVA: 0x00032BC9 File Offset: 0x00030DC9
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

	// Token: 0x06000896 RID: 2198 RVA: 0x00032BE5 File Offset: 0x00030DE5
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00032C21 File Offset: 0x00030E21
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00032C3D File Offset: 0x00030E3D
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00032C4B File Offset: 0x00030E4B
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000540 RID: 1344
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000541 RID: 1345
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000542 RID: 1346
	private AudioSource mSource;
}
