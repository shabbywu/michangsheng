using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DAF RID: 3503
	public class MusicMag : MonoBehaviour
	{
		// Token: 0x0600547B RID: 21627 RVA: 0x0003C787 File Offset: 0x0003A987
		private void Start()
		{
			MusicMag.instance = this;
			this.initAudio();
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0003C795 File Offset: 0x0003A995
		public void PlayEffectMusic(int index, float pitch = 1f)
		{
			this.audioSourceEffect.clip = this.EffectMusic[index - 1].audioClip;
			this.PlayEffectMusic(pitch);
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0003C7BC File Offset: 0x0003A9BC
		public void PlayEffectMusic(AudioClip audioClip, float pitch = 1f)
		{
			this.audioSourceEffect.clip = audioClip;
			this.PlayEffectMusic(pitch);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0003C7D1 File Offset: 0x0003A9D1
		public void PlayEffectMusic(float pitch = 1f)
		{
			this.audioSourceEffect.pitch = pitch;
			this.audioSourceEffect.Play();
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x00231D0C File Offset: 0x0022FF0C
		public void playMusic(int index)
		{
			if (this.nowBackgroundIndex == index && this.audioSource.volume > 0f)
			{
				return;
			}
			this.nowBackgroundIndex = index;
			base.StopCoroutine(this.JianBianStopMusic());
			base.StopCoroutine(this.RealizePlayeMusic(index));
			if (this.audioSource.isPlaying)
			{
				base.StartCoroutine(this.JianBianStopMusic());
			}
			base.StartCoroutine(this.RealizePlayeMusic(index));
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x00231D80 File Offset: 0x0022FF80
		public void playMusic(string name)
		{
			int musicIndex = this.getMusicIndex(name);
			this.playMusic(musicIndex);
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x00231D9C File Offset: 0x0022FF9C
		public void PlayMusicImmediately(string name)
		{
			int musicIndex = this.getMusicIndex(name);
			this.flagPlay = true;
			try
			{
				this.audioSource.clip = this.BackGroudMusic[musicIndex].audioClip;
				this.audioSource.volume = this.getBackgroundVoice();
				this.audioSource.Play();
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("播放音乐时出错，PlayMusicImmediately,index{0} {1}", musicIndex, arg));
			}
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0003C7EA File Offset: 0x0003A9EA
		private IEnumerator RealizePlayeMusic(int index)
		{
			float num = this.audioSource.isPlaying ? 1.5f : 0.5f;
			yield return new WaitForSeconds(num);
			this.flagPlay = true;
			try
			{
				this.audioSource.clip = this.BackGroudMusic[index].audioClip;
				this.audioSource.volume = this.getBackgroundVoice();
				this.audioSource.Play();
				yield break;
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("RealizePlayeMusic,index{0} {1}", index, arg));
				yield break;
			}
			yield break;
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x0003C800 File Offset: 0x0003AA00
		private IEnumerator JianBianStopMusic()
		{
			float volumeAdd = -this.getBackgroundVoice();
			if (volumeAdd == 0f)
			{
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				float initVolume = this.audioSource.volume;
				this.flagPlay = false;
				while (!this.flagPlay)
				{
					initVolume += volumeAdd / 10f;
					if (initVolume > this.getBackgroundVoice() || initVolume < 0f)
					{
						initVolume = Mathf.Clamp01(initVolume);
						this.audioSource.volume = initVolume;
						if (initVolume == 0f)
						{
							this.audioSource.Stop();
							goto IL_13A;
						}
						goto IL_13A;
					}
					else
					{
						this.audioSource.volume = initVolume;
						yield return new WaitForSeconds(0.1f);
					}
				}
				this.flagPlay = false;
			}
			IL_13A:
			yield break;
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x00231E1C File Offset: 0x0023001C
		public int getMusicIndex(string name)
		{
			int num = 0;
			using (List<MusicInfo>.Enumerator enumerator = this.BackGroudMusic.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.name == name)
					{
						break;
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0003C80F File Offset: 0x0003AA0F
		public void stopMusic()
		{
			base.StartCoroutine(this.JianBianStopMusic());
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0003C81E File Offset: 0x0003AA1E
		public float getBackgroundVoice()
		{
			return SystemConfig.Inst.GetBackGroundVolume();
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0003C82A File Offset: 0x0003AA2A
		public void initAudio()
		{
			this.setBackGroundVolume(this.getBackgroundVoice());
			this.setEffectVolum(SystemConfig.Inst.GetEffectVolume());
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00231E80 File Offset: 0x00230080
		public void setFunguseMusice()
		{
			float num = (float)((PlayerPrefs.GetInt("SavePlayerSet0", 0) == 0) ? 1 : 0);
			float volume = (float)PlayerPrefs.GetInt("SavePlayerSet2", 10) / 10f * num;
			GameObject gameObject = GameObject.Find("FungusManager");
			if (gameObject != null)
			{
				AudioSource[] components = gameObject.GetComponents<AudioSource>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].volume = volume;
				}
			}
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0003C848 File Offset: 0x0003AA48
		public void setBackGroundVolume(float volume)
		{
			this.audioSource.volume = volume;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0003C856 File Offset: 0x0003AA56
		public void setEffectVolum(float volume)
		{
			this.audioSourceEffect.volume = volume;
		}

		// Token: 0x04005436 RID: 21558
		public List<MusicInfo> BackGroudMusic = new List<MusicInfo>();

		// Token: 0x04005437 RID: 21559
		public List<MusicInfo> EffectMusic = new List<MusicInfo>();

		// Token: 0x04005438 RID: 21560
		public AudioSource audioSource;

		// Token: 0x04005439 RID: 21561
		public AudioSource audioSourceEffect;

		// Token: 0x0400543A RID: 21562
		public static MusicMag instance;

		// Token: 0x0400543B RID: 21563
		private bool flagPlay;

		// Token: 0x0400543C RID: 21564
		private int nowBackgroundIndex = -999;
	}
}
