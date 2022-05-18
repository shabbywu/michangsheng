using System;
using UnityEngine;

// Token: 0x02000688 RID: 1672
public class DestroySpearGorilla : MonoBehaviour
{
	// Token: 0x060029D0 RID: 10704 RVA: 0x000207A5 File Offset: 0x0001E9A5
	public void DestroyGorilla()
	{
		this.gorilla.GetComponent<KillTheBaboon>().DestoyEnemy();
	}

	// Token: 0x04002370 RID: 9072
	public Transform gorilla;
}
