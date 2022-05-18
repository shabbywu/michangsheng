using System;
using UnityEngine;

// Token: 0x02000681 RID: 1665
public class CoinVisibility : MonoBehaviour
{
	// Token: 0x060029A1 RID: 10657 RVA: 0x00020504 File Offset: 0x0001E704
	private void OnBecameInvisible()
	{
		Debug.Log("SAKRIO, OPAAAAA");
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x0002051C File Offset: 0x0001E71C
	private void OnBecameVisible()
	{
		if (MonkeyController2D.canRespawnThings)
		{
			Debug.Log("SAD SE VIDI, RNZAAAA");
			base.GetComponent<Renderer>().enabled = true;
		}
	}
}
