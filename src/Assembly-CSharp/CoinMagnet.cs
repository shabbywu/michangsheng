using System;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public class CoinMagnet : MonoBehaviour
{
	// Token: 0x06002569 RID: 9577 RVA: 0x001031D6 File Offset: 0x001013D6
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Novcic")
		{
			col.gameObject.SetActive(false);
		}
	}
}
