using System;
using Febucci.Attributes;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Examples
{
	// Token: 0x020006BA RID: 1722
	[AddComponentMenu("Febucci/TextAnimator/SoundWriter")]
	[RequireComponent(typeof(TAnimPlayerBase))]
	public class TAnimSoundWriter : MonoBehaviour
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x00175C60 File Offset: 0x00173E60
		private void Awake()
		{
			if (this.source == null || this.sounds.Length == 0)
			{
				return;
			}
			this.source.playOnAwake = false;
			this.source.loop = false;
			TAnimPlayerBase component = base.GetComponent<TAnimPlayerBase>();
			if (component != null)
			{
				component.onCharacterVisible.AddListener(new UnityAction<char>(this.OnCharacter));
			}
			this.clipIndex = (this.randomSequence ? Random.Range(0, this.sounds.Length) : 0);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x00175CE0 File Offset: 0x00173EE0
		private void OnCharacter(char character)
		{
			if (Time.time - this.latestTimePlayed <= this.minSoundDelay)
			{
				return;
			}
			this.source.clip = this.sounds[this.clipIndex];
			if (this.interruptPreviousSound)
			{
				this.source.Play();
			}
			else
			{
				this.source.PlayOneShot(this.source.clip);
			}
			if (this.randomSequence)
			{
				this.clipIndex = Random.Range(0, this.sounds.Length);
			}
			else
			{
				this.clipIndex++;
				if (this.clipIndex >= this.sounds.Length)
				{
					this.clipIndex = 0;
				}
			}
			this.latestTimePlayed = Time.time;
		}

		// Token: 0x04002F8B RID: 12171
		[Header("References")]
		public AudioSource source;

		// Token: 0x04002F8C RID: 12172
		[Header("Management")]
		[Tooltip("How much time has to pass before playing the next sound")]
		[SerializeField]
		[MinValue(0f)]
		private float minSoundDelay = 0.07f;

		// Token: 0x04002F8D RID: 12173
		[Tooltip("True if you want the new sound to cut the previous one\nFalse if each sound will continue until its end")]
		[SerializeField]
		private bool interruptPreviousSound = true;

		// Token: 0x04002F8E RID: 12174
		[Header("Audio Clips")]
		[Tooltip("True if sounds will be picked random from the array\nFalse if they'll be chosen in order")]
		[SerializeField]
		private bool randomSequence;

		// Token: 0x04002F8F RID: 12175
		[SerializeField]
		private AudioClip[] sounds = new AudioClip[0];

		// Token: 0x04002F90 RID: 12176
		private float latestTimePlayed = -1f;

		// Token: 0x04002F91 RID: 12177
		private int clipIndex;
	}
}
