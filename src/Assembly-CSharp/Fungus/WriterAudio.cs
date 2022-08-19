using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E93 RID: 3731
	public class WriterAudio : MonoBehaviour, IWriterListener
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060069C4 RID: 27076 RVA: 0x00291D94 File Offset: 0x0028FF94
		public bool IsPlayingVoiceOver
		{
			get
			{
				return this.playingVoiceover;
			}
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x00291D9C File Offset: 0x0028FF9C
		public float GetSecondsRemaining()
		{
			if (this.IsPlayingVoiceOver)
			{
				return this.targetAudioSource.clip.length - this.targetAudioSource.time;
			}
			return 0f;
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x00291DC8 File Offset: 0x0028FFC8
		protected virtual void SetAudioMode(AudioMode mode)
		{
			this.audioMode = mode;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x00291DD4 File Offset: 0x0028FFD4
		protected virtual void Awake()
		{
			if (this.targetAudioSource == null)
			{
				this.targetAudioSource = base.GetComponent<AudioSource>();
				if (this.targetAudioSource == null)
				{
					this.targetAudioSource = base.gameObject.AddComponent<AudioSource>();
				}
			}
			this.targetAudioSource.volume = 0f;
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x00291E2C File Offset: 0x0029002C
		protected virtual void Play(AudioClip audioClip)
		{
			if (this.targetAudioSource == null || (this.audioMode == AudioMode.SoundEffect && this.soundEffect == null && audioClip == null) || (this.audioMode == AudioMode.Beeps && this.beepSounds.Count == 0))
			{
				return;
			}
			this.playingVoiceover = false;
			this.targetAudioSource.volume = 0f;
			this.targetVolume = this.volume;
			if (audioClip != null)
			{
				this.targetAudioSource.clip = audioClip;
				this.targetAudioSource.loop = this.loop;
				this.targetAudioSource.Play();
				return;
			}
			if (this.audioMode == AudioMode.SoundEffect && this.soundEffect != null)
			{
				this.targetAudioSource.clip = this.soundEffect;
				this.targetAudioSource.loop = this.loop;
				this.targetAudioSource.Play();
				return;
			}
			if (this.audioMode == AudioMode.Beeps)
			{
				this.targetAudioSource.clip = null;
				this.targetAudioSource.loop = false;
				this.playBeeps = true;
			}
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x00291F3E File Offset: 0x0029013E
		protected virtual void Pause()
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.targetVolume = 0f;
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x00291F5A File Offset: 0x0029015A
		protected virtual void Stop()
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.targetVolume = 0f;
			this.targetAudioSource.loop = false;
			this.playBeeps = false;
			this.playingVoiceover = false;
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x00291F90 File Offset: 0x00290190
		protected virtual void Resume()
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.targetVolume = this.volume;
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x00291FAD File Offset: 0x002901AD
		protected virtual void Update()
		{
			this.targetAudioSource.volume = Mathf.MoveTowards(this.targetAudioSource.volume, this.targetVolume, Time.deltaTime * 5f);
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x00291FDB File Offset: 0x002901DB
		public virtual void OnInput()
		{
			if (this.inputSound != null)
			{
				AudioSource.PlayClipAtPoint(this.inputSound, Vector3.zero);
			}
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x00291FFB File Offset: 0x002901FB
		public virtual void OnStart(AudioClip audioClip)
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Play(audioClip);
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0029200D File Offset: 0x0029020D
		public virtual void OnPause()
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Pause();
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0029201E File Offset: 0x0029021E
		public virtual void OnResume()
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Resume();
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x0029202F File Offset: 0x0029022F
		public virtual void OnEnd(bool stopAudio)
		{
			if (stopAudio)
			{
				this.Stop();
			}
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x0029203C File Offset: 0x0029023C
		public virtual void OnGlyph()
		{
			if (this.playingVoiceover)
			{
				return;
			}
			if (this.playBeeps && this.beepSounds.Count > 0 && !this.targetAudioSource.isPlaying && this.nextBeepTime < Time.realtimeSinceStartup)
			{
				this.targetAudioSource.clip = this.beepSounds[Random.Range(0, this.beepSounds.Count)];
				if (this.targetAudioSource.clip != null)
				{
					this.targetAudioSource.loop = false;
					this.targetVolume = this.volume;
					this.targetAudioSource.Play();
					float length = this.targetAudioSource.clip.length;
					this.nextBeepTime = Time.realtimeSinceStartup + length;
				}
			}
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x00292108 File Offset: 0x00290308
		public virtual void OnVoiceover(AudioClip voiceOverClip)
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.playingVoiceover = true;
			this.targetAudioSource.volume = this.volume;
			this.targetVolume = this.volume;
			this.targetAudioSource.loop = false;
			this.targetAudioSource.clip = voiceOverClip;
			this.targetAudioSource.Play();
		}

		// Token: 0x040059B5 RID: 22965
		[Tooltip("Volume level of writing sound effects")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x040059B6 RID: 22966
		[Tooltip("Loop the audio when in Sound Effect mode. Has no effect in Beeps mode.")]
		[SerializeField]
		protected bool loop = true;

		// Token: 0x040059B7 RID: 22967
		[Tooltip("AudioSource to use for playing sound effects. If none is selected then one will be created.")]
		[SerializeField]
		protected AudioSource targetAudioSource;

		// Token: 0x040059B8 RID: 22968
		[Tooltip("Type of sound effect to play when writing text")]
		[SerializeField]
		protected AudioMode audioMode;

		// Token: 0x040059B9 RID: 22969
		[Tooltip("List of beeps to randomly select when playing beep sound effects. Will play maximum of one beep per character, with only one beep playing at a time.")]
		[SerializeField]
		protected List<AudioClip> beepSounds = new List<AudioClip>();

		// Token: 0x040059BA RID: 22970
		[Tooltip("Long playing sound effect to play when writing text")]
		[SerializeField]
		protected AudioClip soundEffect;

		// Token: 0x040059BB RID: 22971
		[Tooltip("Sound effect to play on user input (e.g. a click)")]
		[SerializeField]
		protected AudioClip inputSound;

		// Token: 0x040059BC RID: 22972
		protected float targetVolume;

		// Token: 0x040059BD RID: 22973
		protected bool playBeeps;

		// Token: 0x040059BE RID: 22974
		protected bool playingVoiceover;

		// Token: 0x040059BF RID: 22975
		protected float nextBeepTime;
	}
}
