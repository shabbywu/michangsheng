using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005EA RID: 1514
	public class FPHitscanEventHandler : MonoBehaviour
	{
		// Token: 0x060030B1 RID: 12465 RVA: 0x0015C6A8 File Offset: 0x0015A8A8
		public void PlaySound(int index)
		{
			if (this.m_AudioSource && this.m_Sounds.Length != 0)
			{
				this.m_AudioSource.PlayOneShot(this.m_Sounds[Mathf.Clamp(index, 0, this.m_Sounds.Length - 1)], this.m_Volume);
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0015C6F4 File Offset: 0x0015A8F4
		public void SpawnObject(string name)
		{
			this.AnimEvent_SpawnObject.Send(name);
		}

		// Token: 0x04002AE1 RID: 10977
		public Message<string> AnimEvent_SpawnObject = new Message<string>();

		// Token: 0x04002AE2 RID: 10978
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002AE3 RID: 10979
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Volume = 0.7f;

		// Token: 0x04002AE4 RID: 10980
		[SerializeField]
		private AudioClip[] m_Sounds;
	}
}
