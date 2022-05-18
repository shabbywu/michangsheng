using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class EGA_EffectSound : MonoBehaviour
{
	// Token: 0x06000044 RID: 68 RVA: 0x0005DAAC File Offset: 0x0005BCAC
	private void Start()
	{
		this.soundComponent = base.GetComponent<AudioSource>();
		this.clip = this.soundComponent.clip;
		if (this.RandomVolume)
		{
			this.soundComponent.volume = Random.Range(this.minVolume, this.maxVolume);
			this.RepeatSound();
		}
		if (this.Repeating)
		{
			base.InvokeRepeating("RepeatSound", this.StartTime, this.RepeatTime);
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000041C6 File Offset: 0x000023C6
	private void RepeatSound()
	{
		this.soundComponent.PlayOneShot(this.clip);
	}

	// Token: 0x04000030 RID: 48
	public bool Repeating = true;

	// Token: 0x04000031 RID: 49
	public float RepeatTime = 2f;

	// Token: 0x04000032 RID: 50
	public float StartTime;

	// Token: 0x04000033 RID: 51
	public bool RandomVolume;

	// Token: 0x04000034 RID: 52
	public float minVolume = 0.4f;

	// Token: 0x04000035 RID: 53
	public float maxVolume = 1f;

	// Token: 0x04000036 RID: 54
	private AudioClip clip;

	// Token: 0x04000037 RID: 55
	private AudioSource soundComponent;
}
