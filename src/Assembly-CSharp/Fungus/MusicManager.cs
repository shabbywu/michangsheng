using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012DB RID: 4827
	public class MusicManager : MonoBehaviour
	{
		// Token: 0x06007595 RID: 30101 RVA: 0x002B08A8 File Offset: 0x002AEAA8
		private void Reset()
		{
			int num = base.GetComponents<AudioSource>().Length;
			for (int i = 0; i < 3 - num; i++)
			{
				base.gameObject.AddComponent<AudioSource>();
			}
		}

		// Token: 0x06007596 RID: 30102 RVA: 0x002B08D8 File Offset: 0x002AEAD8
		protected virtual void Awake()
		{
			this.Reset();
			AudioSource[] components = base.GetComponents<AudioSource>();
			this.audioSourceMusic = components[0];
			this.audioSourceAmbiance = components[1];
			this.audioSourceSoundEffect = components[2];
		}

		// Token: 0x06007597 RID: 30103 RVA: 0x000501C7 File Offset: 0x0004E3C7
		protected virtual void Start()
		{
			this.audioSourceMusic.playOnAwake = false;
			this.audioSourceMusic.loop = true;
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x002B0910 File Offset: 0x002AEB10
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

		// Token: 0x06007599 RID: 30105 RVA: 0x000501E1 File Offset: 0x0004E3E1
		public virtual void PlaySound(AudioClip soundClip, float volume)
		{
			this.audioSourceSoundEffect.PlayOneShot(soundClip, volume);
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x000501F0 File Offset: 0x0004E3F0
		public virtual void PlayAmbianceSound(AudioClip soundClip, bool loop, float volume)
		{
			this.audioSourceAmbiance.loop = loop;
			this.audioSourceAmbiance.clip = soundClip;
			this.audioSourceAmbiance.volume = volume;
			this.audioSourceAmbiance.Play();
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x002B0A10 File Offset: 0x002AEC10
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

		// Token: 0x0600759C RID: 30108 RVA: 0x002B0AA8 File Offset: 0x002AECA8
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

		// Token: 0x0600759D RID: 30109 RVA: 0x00050221 File Offset: 0x0004E421
		public virtual void StopMusic()
		{
			this.audioSourceMusic.Stop();
			this.audioSourceMusic.clip = null;
		}

		// Token: 0x0600759E RID: 30110 RVA: 0x0005023A File Offset: 0x0004E43A
		public virtual void StopAmbiance()
		{
			this.audioSourceAmbiance.Stop();
			this.audioSourceAmbiance.clip = null;
		}

		// Token: 0x040066B2 RID: 26290
		protected AudioSource audioSourceMusic;

		// Token: 0x040066B3 RID: 26291
		protected AudioSource audioSourceAmbiance;

		// Token: 0x040066B4 RID: 26292
		protected AudioSource audioSourceSoundEffect;
	}
}
