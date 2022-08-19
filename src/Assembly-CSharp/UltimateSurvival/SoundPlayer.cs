using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000628 RID: 1576
	[Serializable]
	public class SoundPlayer
	{
		// Token: 0x06003206 RID: 12806 RVA: 0x00162254 File Offset: 0x00160454
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

		// Token: 0x06003207 RID: 12807 RVA: 0x001622D0 File Offset: 0x001604D0
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

		// Token: 0x06003208 RID: 12808 RVA: 0x00162324 File Offset: 0x00160524
		public void Play2D(ItemSelectionMethod selectionMethod = ItemSelectionMethod.RandomlyButExcludeLast)
		{
			if (this.m_Sounds.Length == 0)
			{
				return;
			}
			int num = this.CalculateNextClipToPlay(selectionMethod);
			GameController.Audio.Play2D(this.m_Sounds[num], Random.Range(this.m_VolumeRange.x, this.m_VolumeRange.y));
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00162370 File Offset: 0x00160570
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

		// Token: 0x04002C53 RID: 11347
		[SerializeField]
		private AudioClip[] m_Sounds;

		// Token: 0x04002C54 RID: 11348
		[SerializeField]
		private Vector2 m_VolumeRange = new Vector2(0.5f, 0.75f);

		// Token: 0x04002C55 RID: 11349
		[SerializeField]
		private Vector2 m_PitchRange = new Vector2(0.9f, 1.1f);

		// Token: 0x04002C56 RID: 11350
		private int m_LastSoundPlayed;
	}
}
