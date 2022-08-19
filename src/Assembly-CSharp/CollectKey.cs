using System;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public class CollectKey : MonoBehaviour
{
	// Token: 0x06002575 RID: 9589 RVA: 0x001034F2 File Offset: 0x001016F2
	private void Start()
	{
		this.manage = GameObject.Find("_GameManager").GetComponent<ManageFull>();
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x00103515 File Offset: 0x00101715
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			base.GetComponent<Collider2D>().enabled = false;
			this.anim.Play("CollectKey");
			base.Invoke("NotifyManager", 0.25f);
		}
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x00103555 File Offset: 0x00101755
	private void NotifyManager()
	{
		GameObject.Find("_GameManager").SendMessage("KeyCollected");
	}

	// Token: 0x04001E31 RID: 7729
	private ManageFull manage;

	// Token: 0x04001E32 RID: 7730
	private Animator anim;
}
