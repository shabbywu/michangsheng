using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
[Serializable]
public class EasyAudioUtility_Helper
{
	// Token: 0x040007DC RID: 2012
	public string name;

	// Token: 0x040007DD RID: 2013
	public AudioClip clip;

	// Token: 0x040007DE RID: 2014
	[Range(0f, 1f)]
	public float volume;

	// Token: 0x040007DF RID: 2015
	[Range(0f, 1f)]
	public float volumeVariance;

	// Token: 0x040007E0 RID: 2016
	[Range(-2f, 2f)]
	public float pitch;

	// Token: 0x040007E1 RID: 2017
	[Range(0f, 1f)]
	public float pitchVariance;

	// Token: 0x040007E2 RID: 2018
	public bool loop;

	// Token: 0x040007E3 RID: 2019
	[HideInInspector]
	public AudioSource source;
}
