using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

public class MusicMag : MonoBehaviour
{
	public List<MusicInfo> BackGroudMusic = new List<MusicInfo>();

	public List<MusicInfo> EffectMusic = new List<MusicInfo>();

	public AudioSource audioSource;

	public AudioSource audioSourceEffect;

	public static MusicMag instance;

	private bool flagPlay;

	private int nowBackgroundIndex = -999;

	private void Start()
	{
		instance = this;
		initAudio();
	}

	public void PlayEffectMusic(int index, float pitch = 1f)
	{
		audioSourceEffect.clip = EffectMusic[index - 1].audioClip;
		PlayEffectMusic(pitch);
	}

	public void PlayEffectMusic(AudioClip audioClip, float pitch = 1f)
	{
		audioSourceEffect.clip = audioClip;
		PlayEffectMusic(pitch);
	}

	public void PlayEffectMusic(float pitch = 1f)
	{
		audioSourceEffect.pitch = pitch;
		audioSourceEffect.Play();
	}

	public void playMusic(int index)
	{
		if (nowBackgroundIndex != index || !(audioSource.volume > 0f))
		{
			nowBackgroundIndex = index;
			((MonoBehaviour)this).StopCoroutine(JianBianStopMusic());
			((MonoBehaviour)this).StopCoroutine(RealizePlayeMusic(index));
			if (audioSource.isPlaying)
			{
				((MonoBehaviour)this).StartCoroutine(JianBianStopMusic());
			}
			((MonoBehaviour)this).StartCoroutine(RealizePlayeMusic(index));
		}
	}

	public void playMusic(string name)
	{
		int musicIndex = getMusicIndex(name);
		playMusic(musicIndex);
	}

	public void PlayMusicImmediately(string name)
	{
		int musicIndex = getMusicIndex(name);
		flagPlay = true;
		try
		{
			audioSource.clip = BackGroudMusic[musicIndex].audioClip;
			audioSource.volume = getBackgroundVoice();
			audioSource.Play();
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"播放音乐时出错，PlayMusicImmediately,index{musicIndex} {arg}");
		}
	}

	private IEnumerator RealizePlayeMusic(int index)
	{
		float num = (audioSource.isPlaying ? 1.5f : 0.5f);
		yield return (object)new WaitForSeconds(num);
		flagPlay = true;
		try
		{
			audioSource.clip = BackGroudMusic[index].audioClip;
			audioSource.volume = getBackgroundVoice();
			audioSource.Play();
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"RealizePlayeMusic,index{index} {arg}");
		}
	}

	private IEnumerator JianBianStopMusic()
	{
		float volumeAdd = 0f - getBackgroundVoice();
		if (volumeAdd == 0f)
		{
			yield return (object)new WaitForSeconds(0.1f);
			yield break;
		}
		float initVolume2 = audioSource.volume;
		flagPlay = false;
		while (true)
		{
			if (flagPlay)
			{
				flagPlay = false;
				yield break;
			}
			initVolume2 += volumeAdd / 10f;
			if (initVolume2 > getBackgroundVoice() || initVolume2 < 0f)
			{
				break;
			}
			audioSource.volume = initVolume2;
			yield return (object)new WaitForSeconds(0.1f);
		}
		initVolume2 = Mathf.Clamp01(initVolume2);
		audioSource.volume = initVolume2;
		if (initVolume2 == 0f)
		{
			audioSource.Stop();
		}
	}

	public int getMusicIndex(string name)
	{
		int num = 0;
		using (List<MusicInfo>.Enumerator enumerator = BackGroudMusic.GetEnumerator())
		{
			while (enumerator.MoveNext() && !(enumerator.Current.name == name))
			{
				num++;
			}
		}
		return num;
	}

	public void stopMusic()
	{
		((MonoBehaviour)this).StartCoroutine(JianBianStopMusic());
	}

	public float getBackgroundVoice()
	{
		return SystemConfig.Inst.GetBackGroundVolume();
	}

	public void initAudio()
	{
		setBackGroundVolume(getBackgroundVoice());
		setEffectVolum(SystemConfig.Inst.GetEffectVolume());
	}

	public void setFunguseMusice()
	{
		float num = ((PlayerPrefs.GetInt("SavePlayerSet0", 0) == 0) ? 1 : 0);
		float volume = (float)PlayerPrefs.GetInt("SavePlayerSet2", 10) / 10f * num;
		GameObject val = GameObject.Find("FungusManager");
		if ((Object)(object)val != (Object)null)
		{
			AudioSource[] components = val.GetComponents<AudioSource>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].volume = volume;
			}
		}
	}

	public void setBackGroundVolume(float volume)
	{
		audioSource.volume = volume;
	}

	public void setEffectVolum(float volume)
	{
		audioSourceEffect.volume = volume;
	}
}
