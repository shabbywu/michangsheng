using System;
using UnityEngine;

// Token: 0x02000749 RID: 1865
public class RotateLogo : MonoBehaviour
{
	// Token: 0x06002F7C RID: 12156 RVA: 0x0002331D File Offset: 0x0002151D
	private void Start()
	{
		base.Invoke("PlaySound", 0.15f);
	}

	// Token: 0x06002F7D RID: 12157 RVA: 0x0002332F File Offset: 0x0002152F
	private void OnTriggerEnter(Collider col)
	{
		base.GetComponent<Collider>().enabled = false;
		base.GetComponent<Animator>().Play("RotateLogo_v2");
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x0002334D File Offset: 0x0002154D
	private void AnimationDone()
	{
		RotateLogo.animationDone = true;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x00023355 File Offset: 0x00021555
	private void PlaySound()
	{
		base.transform.GetChild(0).GetComponent<AudioSource>().Play();
	}

	// Token: 0x04002AAF RID: 10927
	public static bool animationDone;
}
