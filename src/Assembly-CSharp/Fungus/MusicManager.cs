using System;
using UnityEngine;

namespace Fungus;

public class MusicManager : MonoBehaviour
{
	protected AudioSource audioSourceMusic;

	protected AudioSource audioSourceAmbiance;

	protected AudioSource audioSourceSoundEffect;

	private void Reset()
	{
		int num = ((Component)this).GetComponents<AudioSource>().Length;
		for (int i = 0; i < 3 - num; i++)
		{
			((Component)this).gameObject.AddComponent<AudioSource>();
		}
	}

	protected virtual void Awake()
	{
		Reset();
		AudioSource[] components = ((Component)this).GetComponents<AudioSource>();
		audioSourceMusic = components[0];
		audioSourceAmbiance = components[1];
		audioSourceSoundEffect = components[2];
	}

	protected virtual void Start()
	{
		audioSourceMusic.playOnAwake = false;
		audioSourceMusic.loop = true;
	}

	public void PlayMusic(AudioClip musicClip, bool loop, float fadeDuration, float atTime)
	{
		if ((Object)(object)audioSourceMusic == (Object)null || (Object)(object)audioSourceMusic.clip == (Object)(object)musicClip)
		{
			return;
		}
		if (Mathf.Approximately(fadeDuration, 0f))
		{
			audioSourceMusic.clip = musicClip;
			audioSourceMusic.loop = loop;
			audioSourceMusic.time = atTime;
			audioSourceMusic.Play();
			return;
		}
		float startVolume = audioSourceMusic.volume;
		LeanTween.value(((Component)this).gameObject, startVolume, 0f, fadeDuration).setOnUpdate(delegate(float v)
		{
			audioSourceMusic.volume = v;
		}).setOnComplete((Action)delegate
		{
			audioSourceMusic.volume = startVolume;
			audioSourceMusic.clip = musicClip;
			audioSourceMusic.loop = loop;
			audioSourceMusic.time = atTime;
			audioSourceMusic.Play();
		});
	}

	public virtual void PlaySound(AudioClip soundClip, float volume)
	{
		audioSourceSoundEffect.PlayOneShot(soundClip, volume);
	}

	public virtual void PlayAmbianceSound(AudioClip soundClip, bool loop, float volume)
	{
		audioSourceAmbiance.loop = loop;
		audioSourceAmbiance.clip = soundClip;
		audioSourceAmbiance.volume = volume;
		audioSourceAmbiance.Play();
	}

	public virtual void SetAudioPitch(float pitch, float duration, Action onComplete)
	{
		if (Mathf.Approximately(duration, 0f))
		{
			audioSourceMusic.pitch = pitch;
			audioSourceAmbiance.pitch = pitch;
			if (onComplete != null)
			{
				onComplete();
			}
			return;
		}
		LeanTween.value(((Component)this).gameObject, audioSourceMusic.pitch, pitch, duration).setOnUpdate(delegate(float p)
		{
			audioSourceMusic.pitch = p;
			audioSourceAmbiance.pitch = p;
		}).setOnComplete((Action)delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}

	public virtual void SetAudioVolume(float volume, float duration, Action onComplete)
	{
		if (Mathf.Approximately(duration, 0f))
		{
			if (onComplete != null)
			{
				onComplete();
			}
			audioSourceMusic.volume = volume;
			audioSourceAmbiance.volume = volume;
			return;
		}
		LeanTween.value(((Component)this).gameObject, audioSourceMusic.volume, volume, duration).setOnUpdate(delegate(float v)
		{
			audioSourceMusic.volume = v;
			audioSourceAmbiance.volume = v;
		}).setOnComplete((Action)delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}

	public virtual void StopMusic()
	{
		audioSourceMusic.Stop();
		audioSourceMusic.clip = null;
	}

	public virtual void StopAmbiance()
	{
		audioSourceAmbiance.Stop();
		audioSourceAmbiance.clip = null;
	}
}
