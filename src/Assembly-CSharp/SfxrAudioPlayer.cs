using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[AddComponentMenu("")]
public class SfxrAudioPlayer : MonoBehaviour
{
	// Token: 0x060002EC RID: 748 RVA: 0x0000F17A File Offset: 0x0000D37A
	private void Start()
	{
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.clip = null;
		audioSource.volume = 1f;
		audioSource.pitch = 1f;
		audioSource.priority = 128;
		audioSource.Play();
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
	private void Update()
	{
		if (this.sfxrSynth == null)
		{
			this.needsToDestroy = true;
		}
		if (this.needsToDestroy)
		{
			this.needsToDestroy = false;
			this.Destroy();
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000F1DA File Offset: 0x0000D3DA
	private void OnAudioFilterRead(float[] __data, int __channels)
	{
		if (!this.isDestroyed && !this.needsToDestroy && this.sfxrSynth != null && !this.sfxrSynth.GenerateAudioFilterData(__data, __channels))
		{
			this.needsToDestroy = true;
			bool flag = this.runningInEditMode;
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000F211 File Offset: 0x0000D411
	public void SetSfxrSynth(SfxrSynth __sfxrSynth)
	{
		this.sfxrSynth = __sfxrSynth;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000F21A File Offset: 0x0000D41A
	public void SetRunningInEditMode(bool __runningInEditMode)
	{
		this.runningInEditMode = __runningInEditMode;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000F223 File Offset: 0x0000D423
	public void Destroy()
	{
		if (!this.isDestroyed)
		{
			this.isDestroyed = true;
			this.sfxrSynth = null;
			if (this.runningInEditMode || !Application.isPlaying)
			{
				Object.DestroyImmediate(base.gameObject);
				return;
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000172 RID: 370
	private bool isDestroyed;

	// Token: 0x04000173 RID: 371
	private bool needsToDestroy;

	// Token: 0x04000174 RID: 372
	private bool runningInEditMode;

	// Token: 0x04000175 RID: 373
	private SfxrSynth sfxrSynth;
}
