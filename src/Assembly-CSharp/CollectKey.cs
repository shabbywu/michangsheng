using System;
using UnityEngine;

// Token: 0x02000684 RID: 1668
public class CollectKey : MonoBehaviour
{
	// Token: 0x060029B1 RID: 10673 RVA: 0x000205DC File Offset: 0x0001E7DC
	private void Start()
	{
		this.manage = GameObject.Find("_GameManager").GetComponent<ManageFull>();
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x000205FF File Offset: 0x0001E7FF
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			base.GetComponent<Collider2D>().enabled = false;
			this.anim.Play("CollectKey");
			base.Invoke("NotifyManager", 0.25f);
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x0002063F File Offset: 0x0001E83F
	private void NotifyManager()
	{
		GameObject.Find("_GameManager").SendMessage("KeyCollected");
	}

	// Token: 0x04002357 RID: 9047
	private ManageFull manage;

	// Token: 0x04002358 RID: 9048
	private Animator anim;
}
