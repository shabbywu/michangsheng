using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200130C RID: 4876
	public class WriterAudio : MonoBehaviour, IWriterListener
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060076EF RID: 30447 RVA: 0x00050E53 File Offset: 0x0004F053
		public bool IsPlayingVoiceOver
		{
			get
			{
				return this.playingVoiceover;
			}
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x00050E5B File Offset: 0x0004F05B
		public float GetSecondsRemaining()
		{
			if (this.IsPlayingVoiceOver)
			{
				return this.targetAudioSource.clip.length - this.targetAudioSource.time;
			}
			return 0f;
		}

		// Token: 0x060076F1 RID: 30449 RVA: 0x00050E87 File Offset: 0x0004F087
		protected virtual void SetAudioMode(AudioMode mode)
		{
			this.audioMode = mode;
		}

		// Token: 0x060076F2 RID: 30450 RVA: 0x002B4AD0 File Offset: 0x002B2CD0
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

		// Token: 0x060076F3 RID: 30451 RVA: 0x002B4B28 File Offset: 0x002B2D28
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

		// Token: 0x060076F4 RID: 30452 RVA: 0x00050E90 File Offset: 0x0004F090
		protected virtual void Pause()
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.targetVolume = 0f;
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x00050EAC File Offset: 0x0004F0AC
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

		// Token: 0x060076F6 RID: 30454 RVA: 0x00050EE2 File Offset: 0x0004F0E2
		protected virtual void Resume()
		{
			if (this.targetAudioSource == null)
			{
				return;
			}
			this.targetVolume = this.volume;
		}

		// Token: 0x060076F7 RID: 30455 RVA: 0x00050EFF File Offset: 0x0004F0FF
		protected virtual void Update()
		{
			this.targetAudioSource.volume = Mathf.MoveTowards(this.targetAudioSource.volume, this.targetVolume, Time.deltaTime * 5f);
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x00050F2D File Offset: 0x0004F12D
		public virtual void OnInput()
		{
			if (this.inputSound != null)
			{
				AudioSource.PlayClipAtPoint(this.inputSound, Vector3.zero);
			}
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x00050F4D File Offset: 0x0004F14D
		public virtual void OnStart(AudioClip audioClip)
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Play(audioClip);
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x00050F5F File Offset: 0x0004F15F
		public virtual void OnPause()
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Pause();
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x00050F70 File Offset: 0x0004F170
		public virtual void OnResume()
		{
			if (this.playingVoiceover)
			{
				return;
			}
			this.Resume();
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x00050F81 File Offset: 0x0004F181
		public virtual void OnEnd(bool stopAudio)
		{
			if (stopAudio)
			{
				this.Stop();
			}
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x002B4C3C File Offset: 0x002B2E3C
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

		// Token: 0x060076FE RID: 30462 RVA: 0x002B4D08 File Offset: 0x002B2F08
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

		// Token: 0x040067C4 RID: 26564
		[Tooltip("Volume level of writing sound effects")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x040067C5 RID: 26565
		[Tooltip("Loop the audio when in Sound Effect mode. Has no effect in Beeps mode.")]
		[SerializeField]
		protected bool loop = true;

		// Token: 0x040067C6 RID: 26566
		[Tooltip("AudioSource to use for playing sound effects. If none is selected then one will be created.")]
		[SerializeField]
		protected AudioSource targetAudioSource;

		// Token: 0x040067C7 RID: 26567
		[Tooltip("Type of sound effect to play when writing text")]
		[SerializeField]
		protected AudioMode audioMode;

		// Token: 0x040067C8 RID: 26568
		[Tooltip("List of beeps to randomly select when playing beep sound effects. Will play maximum of one beep per character, with only one beep playing at a time.")]
		[SerializeField]
		protected List<AudioClip> beepSounds = new List<AudioClip>();

		// Token: 0x040067C9 RID: 26569
		[Tooltip("Long playing sound effect to play when writing text")]
		[SerializeField]
		protected AudioClip soundEffect;

		// Token: 0x040067CA RID: 26570
		[Tooltip("Sound effect to play on user input (e.g. a click)")]
		[SerializeField]
		protected AudioClip inputSound;

		// Token: 0x040067CB RID: 26571
		protected float targetVolume;

		// Token: 0x040067CC RID: 26572
		protected bool playBeeps;

		// Token: 0x040067CD RID: 26573
		protected bool playingVoiceover;

		// Token: 0x040067CE RID: 26574
		protected float nextBeepTime;
	}
}
