using System;
using UnityEngine;
using YSGame;

// Token: 0x02000243 RID: 579
public class SkillPlayeMusic : MonoBehaviour
{
	// Token: 0x060011D4 RID: 4564 RVA: 0x000ACD88 File Offset: 0x000AAF88
	private void Start()
	{
		this.musicObj = new GameObject("SkillMusic");
		Object @object = this.musicObj;
		AudioSource audioSource = this.musicObj.AddComponent<AudioSource>();
		audioSource.clip = this.audioClip;
		audioSource.loop = false;
		audioSource.volume = MusicMag.instance.getBackgroundVoice();
		audioSource.Play();
		Object.Destroy(@object, 10f);
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000042DD File Offset: 0x000024DD
	public void removeMusicObject()
	{
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000E6E RID: 3694
	public AudioClip audioClip;

	// Token: 0x04000E6F RID: 3695
	[NonSerialized]
	public GameObject musicObj;
}
