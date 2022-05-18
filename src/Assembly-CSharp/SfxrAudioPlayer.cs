using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
[AddComponentMenu("")]
public class SfxrAudioPlayer : MonoBehaviour
{
	// Token: 0x060002FE RID: 766 RVA: 0x00006CC2 File Offset: 0x00004EC2
	private void Start()
	{
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.clip = null;
		audioSource.volume = 1f;
		audioSource.pitch = 1f;
		audioSource.priority = 128;
		audioSource.Play();
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00006CFC File Offset: 0x00004EFC
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

	// Token: 0x06000300 RID: 768 RVA: 0x00006D22 File Offset: 0x00004F22
	private void OnAudioFilterRead(float[] __data, int __channels)
	{
		if (!this.isDestroyed && !this.needsToDestroy && this.sfxrSynth != null && !this.sfxrSynth.GenerateAudioFilterData(__data, __channels))
		{
			this.needsToDestroy = true;
			bool flag = this.runningInEditMode;
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00006D59 File Offset: 0x00004F59
	public void SetSfxrSynth(SfxrSynth __sfxrSynth)
	{
		this.sfxrSynth = __sfxrSynth;
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00006D62 File Offset: 0x00004F62
	public void SetRunningInEditMode(bool __runningInEditMode)
	{
		this.runningInEditMode = __runningInEditMode;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00006D6B File Offset: 0x00004F6B
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

	// Token: 0x04000187 RID: 391
	private bool isDestroyed;

	// Token: 0x04000188 RID: 392
	private bool needsToDestroy;

	// Token: 0x04000189 RID: 393
	private bool runningInEditMode;

	// Token: 0x0400018A RID: 394
	private SfxrSynth sfxrSynth;
}
