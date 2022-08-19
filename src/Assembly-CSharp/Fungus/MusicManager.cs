using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E75 RID: 3701
	public class MusicManager : MonoBehaviour
	{
		// Token: 0x060068BC RID: 26812 RVA: 0x0028E164 File Offset: 0x0028C364
		private void Reset()
		{
			int num = base.GetComponents<AudioSource>().Length;
			for (int i = 0; i < 3 - num; i++)
			{
				base.gameObject.AddComponent<AudioSource>();
			}
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x0028E194 File Offset: 0x0028C394
		protected virtual void Awake()
		{
			this.Reset();
			AudioSource[] components = base.GetComponents<AudioSource>();
			this.audioSourceMusic = components[0];
			this.audioSourceAmbiance = components[1];
			this.audioSourceSoundEffect = components[2];
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0028E1C9 File Offset: 0x0028C3C9
		protected virtual void Start()
		{
			this.audioSourceMusic.playOnAwake = false;
			this.audioSourceMusic.loop = true;
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x0028E1E4 File Offset: 0x0028C3E4
		public void PlayMusic(AudioClip musicClip, bool loop, float fadeDuration, float atTime)
		{
			MusicManager.<>c__DisplayClass6_0 CS$<>8__locals1 = new MusicManager.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.musicClip = musicClip;
			CS$<>8__locals1.loop = loop;
			CS$<>8__locals1.atTime = atTime;
			if (this.audioSourceMusic == null || this.audioSourceMusic.clip == CS$<>8__locals1.musicClip)
			{
				return;
			}
			if (Mathf.Approximately(fadeDuration, 0f))
			{
				this.audioSourceMusic.clip = CS$<>8__locals1.musicClip;
				this.audioSourceMusic.loop = CS$<>8__locals1.loop;
				this.audioSourceMusic.time = CS$<>8__locals1.atTime;
				this.audioSourceMusic.Play();
				return;
			}
			float startVolume = this.audioSourceMusic.volume;
			LeanTween.value(base.gameObject, startVolume, 0f, fadeDuration).setOnUpdate(delegate(float v)
			{
				CS$<>8__locals1.<>4__this.audioSourceMusic.volume = v;
			}).setOnComplete(delegate()
			{
				CS$<>8__locals1.<>4__this.audioSourceMusic.volume = startVolume;
				CS$<>8__locals1.<>4__this.audioSourceMusic.clip = CS$<>8__locals1.musicClip;
				CS$<>8__locals1.<>4__this.audioSourceMusic.loop = CS$<>8__locals1.loop;
				CS$<>8__locals1.<>4__this.audioSourceMusic.time = CS$<>8__locals1.atTime;
				CS$<>8__locals1.<>4__this.audioSourceMusic.Play();
			});
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x0028E2E4 File Offset: 0x0028C4E4
		public virtual void PlaySound(AudioClip soundClip, float volume)
		{
			this.audioSourceSoundEffect.PlayOneShot(soundClip, volume);
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0028E2F3 File Offset: 0x0028C4F3
		public virtual void PlayAmbianceSound(AudioClip soundClip, bool loop, float volume)
		{
			this.audioSourceAmbiance.loop = loop;
			this.audioSourceAmbiance.clip = soundClip;
			this.audioSourceAmbiance.volume = volume;
			this.audioSourceAmbiance.Play();
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0028E324 File Offset: 0x0028C524
		public virtual void SetAudioPitch(float pitch, float duration, Action onComplete)
		{
			if (Mathf.Approximately(duration, 0f))
			{
				this.audioSourceMusic.pitch = pitch;
				this.audioSourceAmbiance.pitch = pitch;
				if (onComplete != null)
				{
					onComplete();
				}
				return;
			}
			LeanTween.value(base.gameObject, this.audioSourceMusic.pitch, pitch, duration).setOnUpdate(delegate(float p)
			{
				this.audioSourceMusic.pitch = p;
				this.audioSourceAmbiance.pitch = p;
			}).setOnComplete(delegate()
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x0028E3BC File Offset: 0x0028C5BC
		public virtual void SetAudioVolume(float volume, float duration, Action onComplete)
		{
			if (Mathf.Approximately(duration, 0f))
			{
				if (onComplete != null)
				{
					onComplete();
				}
				this.audioSourceMusic.volume = volume;
				this.audioSourceAmbiance.volume = volume;
				return;
			}
			LeanTween.value(base.gameObject, this.audioSourceMusic.volume, volume, duration).setOnUpdate(delegate(float v)
			{
				this.audioSourceMusic.volume = v;
				this.audioSourceAmbiance.volume = v;
			}).setOnComplete(delegate()
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0028E451 File Offset: 0x0028C651
		public virtual void StopMusic()
		{
			this.audioSourceMusic.Stop();
			this.audioSourceMusic.clip = null;
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x0028E46A File Offset: 0x0028C66A
		public virtual void StopAmbiance()
		{
			this.audioSourceAmbiance.Stop();
			this.audioSourceAmbiance.clip = null;
		}

		// Token: 0x040058F4 RID: 22772
		protected AudioSource audioSourceMusic;

		// Token: 0x040058F5 RID: 22773
		protected AudioSource audioSourceAmbiance;

		// Token: 0x040058F6 RID: 22774
		protected AudioSource audioSourceSoundEffect;
	}
}
