using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
[Serializable]
public class EasyAudioUtility_Helper
{
	// Token: 0x040009BF RID: 2495
	public string name;

	// Token: 0x040009C0 RID: 2496
	public AudioClip clip;

	// Token: 0x040009C1 RID: 2497
	[Range(0f, 1f)]
	public float volume;

	// Token: 0x040009C2 RID: 2498
	[Range(0f, 1f)]
	public float volumeVariance;

	// Token: 0x040009C3 RID: 2499
	[Range(-2f, 2f)]
	public float pitch;

	// Token: 0x040009C4 RID: 2500
	[Range(0f, 1f)]
	public float pitchVariance;

	// Token: 0x040009C5 RID: 2501
	public bool loop;

	// Token: 0x040009C6 RID: 2502
	[HideInInspector]
	public AudioSource source;
}
