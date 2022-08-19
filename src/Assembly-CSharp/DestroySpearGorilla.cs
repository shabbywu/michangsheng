using System;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public class DestroySpearGorilla : MonoBehaviour
{
	// Token: 0x06002594 RID: 9620 RVA: 0x0010437F File Offset: 0x0010257F
	public void DestroyGorilla()
	{
		this.gorilla.GetComponent<KillTheBaboon>().DestoyEnemy();
	}

	// Token: 0x04001E4A RID: 7754
	public Transform gorilla;
}
