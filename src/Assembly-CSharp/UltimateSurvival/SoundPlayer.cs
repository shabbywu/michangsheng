using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000910 RID: 2320
	[Serializable]
	public class SoundPlayer
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x001ABBC0 File Offset: 0x001A9DC0
		public void Play(ItemSelectionMethod selectionMethod, AudioSource audioSource, float volumeFactor = 1f)
		{
			if (!audioSource || this.m_Sounds.Length == 0)
			{
				return;
			}
			int num = this.CalculateNextClipToPlay(selectionMethod);
			float num2 = Random.Range(this.m_VolumeRange.x, this.m_VolumeRange.y) * volumeFactor;
			audioSource.pitch = Random.Range(this.m_PitchRange.x, this.m_PitchRange.y);
			audioSource.PlayOneShot(this.m_Sounds[num], num2);
			this.m_LastSoundPlayed = num;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x001ABC3C File Offset: 0x001A9E3C
		public void PlayAtPosition(ItemSelectionMethod selectionMethod, Vector3 position, float volumeFactor = 1f)
		{
			if (this.m_Sounds.Length == 0)
			{
				return;
			}
			int num = this.CalculateNextClipToPlay(selectionMethod);
			AudioSource.PlayClipAtPoint(this.m_Sounds[num], position, Random.Range(this.m_VolumeRange.x, this.m_VolumeRange.y) * volumeFactor);
			this.m_LastSoundPlayed = num;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x001ABC90 File Offset: 0x001A9E90
		public void Play2D(ItemSelectionMethod selectionMethod = ItemSelectionMethod.RandomlyButExcludeLast)
		{
			if (this.m_Sounds.Length == 0)
			{
				return;
			}
			int num = this.CalculateNextClipToPlay(selectionMethod);
			GameController.Audio.Play2D(this.m_Sounds[num], Random.Range(this.m_VolumeRange.x, this.m_VolumeRange.y));
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x001ABCDC File Offset: 0x001A9EDC
		private int CalculateNextClipToPlay(ItemSelectionMethod selectionMethod)
		{
			int result = 0;
			if (selectionMethod == ItemSelectionMethod.Randomly || this.m_Sounds.Length == 1)
			{
				result = Random.Range(0, this.m_Sounds.Length);
			}
			else if (selectionMethod == ItemSelectionMethod.RandomlyButExcludeLast)
			{
				AudioClip audioClip = this.m_Sounds[0];
				this.m_Sounds[0] = this.m_Sounds[this.m_LastSoundPlayed];
				this.m_Sounds[this.m_LastSoundPlayed] = audioClip;
				result = Random.Range(1, this.m_Sounds.Length);
			}
			else if (selectionMethod == ItemSelectionMethod.InSequence)
			{
				result = (int)Mathf.Repeat((float)(this.m_LastSoundPlayed + 1), (float)this.m_Sounds.Length);
			}
			return result;
		}

		// Token: 0x04003581 RID: 13697
		[SerializeField]
		private AudioClip[] m_Sounds;

		// Token: 0x04003582 RID: 13698
		[SerializeField]
		private Vector2 m_VolumeRange = new Vector2(0.5f, 0.75f);

		// Token: 0x04003583 RID: 13699
		[SerializeField]
		private Vector2 m_PitchRange = new Vector2(0.9f, 1.1f);

		// Token: 0x04003584 RID: 13700
		private int m_LastSoundPlayed;
	}
}
