using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class AudioUtils : MonoBehaviour
{
	public Value<Gunshot> LastGunshot = new Value<Gunshot>(null);

	private Dictionary<AudioSource, Coroutine> m_LevelSetters = new Dictionary<AudioSource, Coroutine>();

	[SerializeField]
	private AudioSource m_2DAudioSource;

	public void Play2D(AudioClip clip, float volume)
	{
		if (Object.op_Implicit((Object)(object)m_2DAudioSource))
		{
			m_2DAudioSource.PlayOneShot(clip, volume);
		}
	}

	public AudioSource CreateAudioSource(string name, Transform parent, Vector3 localPosition, bool is2D, float startVolume, float minDistance)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject(name, new Type[1] { typeof(AudioSource) });
		if (!Object.op_Implicit((Object)(object)parent))
		{
			parent = ((Component)this).transform;
		}
		val.transform.parent = parent;
		val.transform.localPosition = localPosition;
		AudioSource component = val.GetComponent<AudioSource>();
		component.volume = startVolume;
		component.spatialBlend = (is2D ? 0f : 1f);
		component.minDistance = minDistance;
		return component;
	}

	public void LerpVolumeOverTime(AudioSource audioSource, float targetVolume, float speed)
	{
		if (m_LevelSetters.ContainsKey(audioSource))
		{
			if (m_LevelSetters[audioSource] != null)
			{
				((MonoBehaviour)this).StopCoroutine(m_LevelSetters[audioSource]);
			}
			m_LevelSetters[audioSource] = ((MonoBehaviour)this).StartCoroutine(C_LerpVolumeOverTime(audioSource, targetVolume, speed));
		}
		else
		{
			m_LevelSetters.Add(audioSource, ((MonoBehaviour)this).StartCoroutine(C_LerpVolumeOverTime(audioSource, targetVolume, speed)));
		}
	}

	private IEnumerator C_LerpVolumeOverTime(AudioSource audioSource, float volume, float speed)
	{
		while ((Object)(object)audioSource != (Object)null && Mathf.Abs(audioSource.volume - volume) > 0.01f)
		{
			audioSource.volume = Mathf.MoveTowards(audioSource.volume, volume, Time.deltaTime * speed);
			yield return null;
		}
		m_LevelSetters.Remove(audioSource);
	}
}
