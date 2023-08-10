using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	[Range(0f, 1f)]
	public float from = 1f;

	[Range(0f, 1f)]
	public float to = 1f;

	private AudioSource mSource;

	public AudioSource audioSource
	{
		get
		{
			if ((Object)(object)mSource == (Object)null)
			{
				mSource = ((Component)this).GetComponent<AudioSource>();
				if ((Object)(object)mSource == (Object)null)
				{
					mSource = ((Component)this).GetComponent<AudioSource>();
					if ((Object)(object)mSource == (Object)null)
					{
						Debug.LogError((object)"TweenVolume needs an AudioSource to work with", (Object)(object)this);
						((Behaviour)this).enabled = false;
					}
				}
			}
			return mSource;
		}
	}

	[Obsolete("Use 'value' instead")]
	public float volume
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public float value
	{
		get
		{
			if (!((Object)(object)audioSource != (Object)null))
			{
				return 0f;
			}
			return mSource.volume;
		}
		set
		{
			if ((Object)(object)audioSource != (Object)null)
			{
				mSource.volume = value;
			}
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		value = from * (1f - factor) + to * factor;
		((Behaviour)mSource).enabled = mSource.volume > 0.01f;
	}

	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		return tweenVolume;
	}

	public override void SetStartToCurrentValue()
	{
		from = value;
	}

	public override void SetEndToCurrentValue()
	{
		to = value;
	}
}
