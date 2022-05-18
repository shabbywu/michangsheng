using System;
using UnityEngine;

// Token: 0x02000680 RID: 1664
public class CoinMagnet : MonoBehaviour
{
	// Token: 0x0600299F RID: 10655 RVA: 0x000204E4 File Offset: 0x0001E6E4
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Novcic")
		{
			col.gameObject.SetActive(false);
		}
	}
}
