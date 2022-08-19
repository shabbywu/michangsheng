using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A7F RID: 2687
	public class MusicMag : MonoBehaviour
	{
		// Token: 0x06004B67 RID: 19303 RVA: 0x00200360 File Offset: 0x001FE560
		private void Start()
		{
			MusicMag.instance = this;
			this.initAudio();
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x0020036E File Offset: 0x001FE56E
		public void PlayEffectMusic(int index, float pitch = 1f)
		{
			this.audioSourceEffect.clip = this.EffectMusic[index - 1].audioClip;
			this.PlayEffectMusic(pitch);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00200395 File Offset: 0x001FE595
		public void PlayEffectMusic(AudioClip audioClip, float pitch = 1f)
		{
			this.audioSourceEffect.clip = audioClip;
			this.PlayEffectMusic(pitch);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x002003AA File Offset: 0x001FE5AA
		public void PlayEffectMusic(float pitch = 1f)
		{
			this.audioSourceEffect.pitch = pitch;
			this.audioSourceEffect.Play();
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x002003C4 File Offset: 0x001FE5C4
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

		// Token: 0x06004B6C RID: 19308 RVA: 0x00200438 File Offset: 0x001FE638
		public void playMusic(string name)
		{
			int musicIndex = this.getMusicIndex(name);
			this.playMusic(musicIndex);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00200454 File Offset: 0x001FE654
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

		// Token: 0x06004B6E RID: 19310 RVA: 0x002004D4 File Offset: 0x001FE6D4
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

		// Token: 0x06004B6F RID: 19311 RVA: 0x002004EA File Offset: 0x001FE6EA
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

		// Token: 0x06004B70 RID: 19312 RVA: 0x002004FC File Offset: 0x001FE6FC
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

		// Token: 0x06004B71 RID: 19313 RVA: 0x00200560 File Offset: 0x001FE760
		public void stopMusic()
		{
			base.StartCoroutine(this.JianBianStopMusic());
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0020056F File Offset: 0x001FE76F
		public float getBackgroundVoice()
		{
			return SystemConfig.Inst.GetBackGroundVolume();
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0020057B File Offset: 0x001FE77B
		public void initAudio()
		{
			this.setBackGroundVolume(this.getBackgroundVoice());
			this.setEffectVolum(SystemConfig.Inst.GetEffectVolume());
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0020059C File Offset: 0x001FE79C
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

		// Token: 0x06004B75 RID: 19317 RVA: 0x00200609 File Offset: 0x001FE809
		public void setBackGroundVolume(float volume)
		{
			this.audioSource.volume = volume;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00200617 File Offset: 0x001FE817
		public void setEffectVolum(float volume)
		{
			this.audioSourceEffect.volume = volume;
		}

		// Token: 0x04004A80 RID: 19072
		public List<MusicInfo> BackGroudMusic = new List<MusicInfo>();

		// Token: 0x04004A81 RID: 19073
		public List<MusicInfo> EffectMusic = new List<MusicInfo>();

		// Token: 0x04004A82 RID: 19074
		public AudioSource audioSource;

		// Token: 0x04004A83 RID: 19075
		public AudioSource audioSourceEffect;

		// Token: 0x04004A84 RID: 19076
		public static MusicMag instance;

		// Token: 0x04004A85 RID: 19077
		private bool flagPlay;

		// Token: 0x04004A86 RID: 19078
		private int nowBackgroundIndex = -999;
	}
}
