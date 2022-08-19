using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
[Serializable]
public class EasyAudioUtility_SMHelper
{
	// Token: 0x040007E5 RID: 2021
	[Tooltip("Scene name where you want to play the clip.")]
	public string SceneName;

	// Token: 0x040007E6 RID: 2022
	[Tooltip("Clip name you have written above in Easy Audio Utility e.g 'BG'.")]
	public string name;

	// Token: 0x040007E7 RID: 2023
	[Tooltip("Clip you want to play.")]
	public AudioClip clip;
}
