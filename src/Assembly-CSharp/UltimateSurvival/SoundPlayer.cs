using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class SoundPlayer
{
	[SerializeField]
	private AudioClip[] m_Sounds;

	[SerializeField]
	private Vector2 m_VolumeRange = new Vector2(0.5f, 0.75f);

	[SerializeField]
	private Vector2 m_PitchRange = new Vector2(0.9f, 1.1f);

	private int m_LastSoundPlayed;

	public void Play(ItemSelectionMethod selectionMethod, AudioSource audioSource, float volumeFactor = 1f)
	{
		if (Object.op_Implicit((Object)(object)audioSource) && m_Sounds.Length != 0)
		{
			int num = CalculateNextClipToPlay(selectionMethod);
			float num2 = Random.Range(m_VolumeRange.x, m_VolumeRange.y) * volumeFactor;
			audioSource.pitch = Random.Range(m_PitchRange.x, m_PitchRange.y);
			audioSource.PlayOneShot(m_Sounds[num], num2);
			m_LastSoundPlayed = num;
		}
	}

	public void PlayAtPosition(ItemSelectionMethod selectionMethod, Vector3 position, float volumeFactor = 1f)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (m_Sounds.Length != 0)
		{
			int num = CalculateNextClipToPlay(selectionMethod);
			AudioSource.PlayClipAtPoint(m_Sounds[num], position, Random.Range(m_VolumeRange.x, m_VolumeRange.y) * volumeFactor);
			m_LastSoundPlayed = num;
		}
	}

	public void Play2D(ItemSelectionMethod selectionMethod = ItemSelectionMethod.RandomlyButExcludeLast)
	{
		if (m_Sounds.Length != 0)
		{
			int num = CalculateNextClipToPlay(selectionMethod);
			GameController.Audio.Play2D(m_Sounds[num], Random.Range(m_VolumeRange.x, m_VolumeRange.y));
		}
	}

	private int CalculateNextClipToPlay(ItemSelectionMethod selectionMethod)
	{
		int result = 0;
		if (selectionMethod == ItemSelectionMethod.Randomly || m_Sounds.Length == 1)
		{
			result = Random.Range(0, m_Sounds.Length);
		}
		else
		{
			switch (selectionMethod)
			{
			case ItemSelectionMethod.RandomlyButExcludeLast:
			{
				AudioClip val = m_Sounds[0];
				m_Sounds[0] = m_Sounds[m_LastSoundPlayed];
				m_Sounds[m_LastSoundPlayed] = val;
				result = Random.Range(1, m_Sounds.Length);
				break;
			}
			case ItemSelectionMethod.InSequence:
				result = (int)Mathf.Repeat((float)(m_LastSoundPlayed + 1), (float)m_Sounds.Length);
				break;
			}
		}
		return result;
	}
}
