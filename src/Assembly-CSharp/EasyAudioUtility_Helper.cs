using System;
using UnityEngine;

[Serializable]
public class EasyAudioUtility_Helper
{
	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;

	[Range(0f, 1f)]
	public float volumeVariance;

	[Range(-2f, 2f)]
	public float pitch;

	[Range(0f, 1f)]
	public float pitchVariance;

	public bool loop;

	[HideInInspector]
	public AudioSource source;
}
