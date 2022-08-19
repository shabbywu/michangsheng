using System;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class RotateLogo : MonoBehaviour
{
	// Token: 0x06002825 RID: 10277 RVA: 0x00130231 File Offset: 0x0012E431
	private void Start()
	{
		base.Invoke("PlaySound", 0.15f);
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x00130243 File Offset: 0x0012E443
	private void OnTriggerEnter(Collider col)
	{
		base.GetComponent<Collider>().enabled = false;
		base.GetComponent<Animator>().Play("RotateLogo_v2");
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x00130261 File Offset: 0x0012E461
	private void AnimationDone()
	{
		RotateLogo.animationDone = true;
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x00130269 File Offset: 0x0012E469
	private void PlaySound()
	{
		base.transform.GetChild(0).GetComponent<AudioSource>().Play();
	}

	// Token: 0x0400232F RID: 9007
	public static bool animationDone;
}
