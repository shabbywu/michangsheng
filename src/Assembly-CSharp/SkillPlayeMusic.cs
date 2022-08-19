using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class SkillPlayeMusic : MonoBehaviour
{
	// Token: 0x06000F76 RID: 3958 RVA: 0x0005D194 File Offset: 0x0005B394
	private void Start()
	{
		this.musicObj = new GameObject("SkillMusic");
		Object @object = this.musicObj;
		AudioSource audioSource = this.musicObj.AddComponent<AudioSource>();
		audioSource.clip = this.audioClip;
		audioSource.loop = false;
		audioSource.volume = SystemConfig.Inst.GetEffectVolume();
		audioSource.Play();
		Object.Destroy(@object, 10f);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00004095 File Offset: 0x00002295
	public void removeMusicObject()
	{
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000B9E RID: 2974
	public AudioClip audioClip;

	// Token: 0x04000B9F RID: 2975
	[NonSerialized]
	public GameObject musicObj;
}
