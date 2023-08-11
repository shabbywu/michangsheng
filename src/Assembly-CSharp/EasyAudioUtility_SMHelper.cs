using System;
using UnityEngine;

[Serializable]
public class EasyAudioUtility_SMHelper
{
	[Tooltip("Scene name where you want to play the clip.")]
	public string SceneName;

	[Tooltip("Clip name you have written above in Easy Audio Utility e.g 'BG'.")]
	public string name;

	[Tooltip("Clip you want to play.")]
	public AudioClip clip;
}
