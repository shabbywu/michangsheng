using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public class WriterAudio : MonoBehaviour, IWriterListener
{
	[Tooltip("Volume level of writing sound effects")]
	[Range(0f, 1f)]
	[SerializeField]
	protected float volume = 1f;

	[Tooltip("Loop the audio when in Sound Effect mode. Has no effect in Beeps mode.")]
	[SerializeField]
	protected bool loop = true;

	[Tooltip("AudioSource to use for playing sound effects. If none is selected then one will be created.")]
	[SerializeField]
	protected AudioSource targetAudioSource;

	[Tooltip("Type of sound effect to play when writing text")]
	[SerializeField]
	protected AudioMode audioMode;

	[Tooltip("List of beeps to randomly select when playing beep sound effects. Will play maximum of one beep per character, with only one beep playing at a time.")]
	[SerializeField]
	protected List<AudioClip> beepSounds = new List<AudioClip>();

	[Tooltip("Long playing sound effect to play when writing text")]
	[SerializeField]
	protected AudioClip soundEffect;

	[Tooltip("Sound effect to play on user input (e.g. a click)")]
	[SerializeField]
	protected AudioClip inputSound;

	protected float targetVolume;

	protected bool playBeeps;

	protected bool playingVoiceover;

	protected float nextBeepTime;

	public bool IsPlayingVoiceOver => playingVoiceover;

	public float GetSecondsRemaining()
	{
		if (IsPlayingVoiceOver)
		{
			return targetAudioSource.clip.length - targetAudioSource.time;
		}
		return 0f;
	}

	protected virtual void SetAudioMode(AudioMode mode)
	{
		audioMode = mode;
	}

	protected virtual void Awake()
	{
		if ((Object)(object)targetAudioSource == (Object)null)
		{
			targetAudioSource = ((Component)this).GetComponent<AudioSource>();
			if ((Object)(object)targetAudioSource == (Object)null)
			{
				targetAudioSource = ((Component)this).gameObject.AddComponent<AudioSource>();
			}
		}
		targetAudioSource.volume = 0f;
	}

	protected virtual void Play(AudioClip audioClip)
	{
		if (!((Object)(object)targetAudioSource == (Object)null) && (audioMode != AudioMode.SoundEffect || !((Object)(object)soundEffect == (Object)null) || !((Object)(object)audioClip == (Object)null)) && (audioMode != 0 || beepSounds.Count != 0))
		{
			playingVoiceover = false;
			targetAudioSource.volume = 0f;
			targetVolume = volume;
			if ((Object)(object)audioClip != (Object)null)
			{
				targetAudioSource.clip = audioClip;
				targetAudioSource.loop = loop;
				targetAudioSource.Play();
			}
			else if (audioMode == AudioMode.SoundEffect && (Object)(object)soundEffect != (Object)null)
			{
				targetAudioSource.clip = soundEffect;
				targetAudioSource.loop = loop;
				targetAudioSource.Play();
			}
			else if (audioMode == AudioMode.Beeps)
			{
				targetAudioSource.clip = null;
				targetAudioSource.loop = false;
				playBeeps = true;
			}
		}
	}

	protected virtual void Pause()
	{
		if (!((Object)(object)targetAudioSource == (Object)null))
		{
			targetVolume = 0f;
		}
	}

	protected virtual void Stop()
	{
		if (!((Object)(object)targetAudioSource == (Object)null))
		{
			targetVolume = 0f;
			targetAudioSource.loop = false;
			playBeeps = false;
			playingVoiceover = false;
		}
	}

	protected virtual void Resume()
	{
		if (!((Object)(object)targetAudioSource == (Object)null))
		{
			targetVolume = volume;
		}
	}

	protected virtual void Update()
	{
		targetAudioSource.volume = Mathf.MoveTowards(targetAudioSource.volume, targetVolume, Time.deltaTime * 5f);
	}

	public virtual void OnInput()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)inputSound != (Object)null)
		{
			AudioSource.PlayClipAtPoint(inputSound, Vector3.zero);
		}
	}

	public virtual void OnStart(AudioClip audioClip)
	{
		if (!playingVoiceover)
		{
			Play(audioClip);
		}
	}

	public virtual void OnPause()
	{
		if (!playingVoiceover)
		{
			Pause();
		}
	}

	public virtual void OnResume()
	{
		if (!playingVoiceover)
		{
			Resume();
		}
	}

	public virtual void OnEnd(bool stopAudio)
	{
		if (stopAudio)
		{
			Stop();
		}
	}

	public virtual void OnGlyph()
	{
		if (!playingVoiceover && playBeeps && beepSounds.Count > 0 && !targetAudioSource.isPlaying && nextBeepTime < Time.realtimeSinceStartup)
		{
			targetAudioSource.clip = beepSounds[Random.Range(0, beepSounds.Count)];
			if ((Object)(object)targetAudioSource.clip != (Object)null)
			{
				targetAudioSource.loop = false;
				targetVolume = volume;
				targetAudioSource.Play();
				float length = targetAudioSource.clip.length;
				nextBeepTime = Time.realtimeSinceStartup + length;
			}
		}
	}

	public virtual void OnVoiceover(AudioClip voiceOverClip)
	{
		if (!((Object)(object)targetAudioSource == (Object)null))
		{
			playingVoiceover = true;
			targetAudioSource.volume = volume;
			targetVolume = volume;
			targetAudioSource.loop = false;
			targetAudioSource.clip = voiceOverClip;
			targetAudioSource.Play();
		}
	}
}
