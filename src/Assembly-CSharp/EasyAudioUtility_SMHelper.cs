using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
[Serializable]
public class EasyAudioUtility_SMHelper
{
	// Token: 0x040009CE RID: 2510
	[Tooltip("Scene name where you want to play the clip.")]
	public string SceneName;

	// Token: 0x040009CF RID: 2511
	[Tooltip("Clip name you have written above in Easy Audio Utility e.g 'BG'.")]
	public string name;

	// Token: 0x040009D0 RID: 2512
	[Tooltip("Clip you want to play.")]
	public AudioClip clip;
}
