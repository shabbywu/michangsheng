using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200061C RID: 1564
	public class AudioUtils : MonoBehaviour
	{
		// Token: 0x060031D6 RID: 12758 RVA: 0x00161751 File Offset: 0x0015F951
		public void Play2D(AudioClip clip, float volume)
		{
			if (this.m_2DAudioSource)
			{
				this.m_2DAudioSource.PlayOneShot(clip, volume);
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00161770 File Offset: 0x0015F970
		public AudioSource CreateAudioSource(string name, Transform parent, Vector3 localPosition, bool is2D, float startVolume, float minDistance)
		{
			GameObject gameObject = new GameObject(name, new Type[]
			{
				typeof(AudioSource)
			});
			if (!parent)
			{
				parent = base.transform;
			}
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = localPosition;
			AudioSource component = gameObject.GetComponent<AudioSource>();
			component.volume = startVolume;
			component.spatialBlend = (is2D ? 0f : 1f);
			component.minDistance = minDistance;
			return component;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x001617EC File Offset: 0x0015F9EC
		public void LerpVolumeOverTime(AudioSource audioSource, float targetVolume, float speed)
		{
			if (this.m_LevelSetters.ContainsKey(audioSource))
			{
				if (this.m_LevelSetters[audioSource] != null)
				{
					base.StopCoroutine(this.m_LevelSetters[audioSource]);
				}
				this.m_LevelSetters[audioSource] = base.StartCoroutine(this.C_LerpVolumeOverTime(audioSource, targetVolume, speed));
				return;
			}
			this.m_LevelSetters.Add(audioSource, base.StartCoroutine(this.C_LerpVolumeOverTime(audioSource, targetVolume, speed)));
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0016185E File Offset: 0x0015FA5E
		private IEnumerator C_LerpVolumeOverTime(AudioSource audioSource, float volume, float speed)
		{
			while (audioSource != null && Mathf.Abs(audioSource.volume - volume) > 0.01f)
			{
				audioSource.volume = Mathf.MoveTowards(audioSource.volume, volume, Time.deltaTime * speed);
				yield return null;
			}
			this.m_LevelSetters.Remove(audioSource);
			yield break;
		}

		// Token: 0x04002C39 RID: 11321
		public Value<Gunshot> LastGunshot = new Value<Gunshot>(null);

		// Token: 0x04002C3A RID: 11322
		private Dictionary<AudioSource, Coroutine> m_LevelSetters = new Dictionary<AudioSource, Coroutine>();

		// Token: 0x04002C3B RID: 11323
		[SerializeField]
		private AudioSource m_2DAudioSource;
	}
}
