using System;
using UnityEngine;

public class SkillPlayeMusic : MonoBehaviour
{
	public AudioClip audioClip;

	[NonSerialized]
	public GameObject musicObj;

	private void Start()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		musicObj = new GameObject("SkillMusic");
		GameObject obj = musicObj;
		AudioSource obj2 = musicObj.AddComponent<AudioSource>();
		obj2.clip = audioClip;
		obj2.loop = false;
		obj2.volume = SystemConfig.Inst.GetEffectVolume();
		obj2.Play();
		Object.Destroy((Object)(object)obj, 10f);
	}

	public void removeMusicObject()
	{
	}

	private void Update()
	{
	}
}
