using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000902 RID: 2306
	public class AudioUtils : MonoBehaviour
	{
		// Token: 0x06003B04 RID: 15108 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		public void Play2D(AudioClip clip, float volume)
		{
			if (this.m_2DAudioSource)
			{
				this.m_2DAudioSource.PlayOneShot(clip, volume);
			}
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x001AB058 File Offset: 0x001A9258
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

		// Token: 0x06003B06 RID: 15110 RVA: 0x001AB0D4 File Offset: 0x001A92D4
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

		// Token: 0x06003B07 RID: 15111 RVA: 0x0002ACCC File Offset: 0x00028ECC
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

		// Token: 0x0400355A RID: 13658
		public Value<Gunshot> LastGunshot = new Value<Gunshot>(null);

		// Token: 0x0400355B RID: 13659
		private Dictionary<AudioSource, Coroutine> m_LevelSetters = new Dictionary<AudioSource, Coroutine>();

		// Token: 0x0400355C RID: 13660
		[SerializeField]
		private AudioSource m_2DAudioSource;
	}
}
