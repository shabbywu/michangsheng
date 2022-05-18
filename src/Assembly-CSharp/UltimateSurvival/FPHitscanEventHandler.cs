using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008BC RID: 2236
	public class FPHitscanEventHandler : MonoBehaviour
	{
		// Token: 0x06003989 RID: 14729 RVA: 0x001A5E68 File Offset: 0x001A4068
		public void PlaySound(int index)
		{
			if (this.m_AudioSource && this.m_Sounds.Length != 0)
			{
				this.m_AudioSource.PlayOneShot(this.m_Sounds[Mathf.Clamp(index, 0, this.m_Sounds.Length - 1)], this.m_Volume);
			}
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x00029C43 File Offset: 0x00027E43
		public void SpawnObject(string name)
		{
			this.AnimEvent_SpawnObject.Send(name);
		}

		// Token: 0x040033B0 RID: 13232
		public Message<string> AnimEvent_SpawnObject = new Message<string>();

		// Token: 0x040033B1 RID: 13233
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x040033B2 RID: 13234
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Volume = 0.7f;

		// Token: 0x040033B3 RID: 13235
		[SerializeField]
		private AudioClip[] m_Sounds;
	}
}
