using System;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
public class CoinVisibility : MonoBehaviour
{
	// Token: 0x0600256B RID: 9579 RVA: 0x001031F6 File Offset: 0x001013F6
	private void OnBecameInvisible()
	{
		Debug.Log("SAKRIO, OPAAAAA");
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x0010320E File Offset: 0x0010140E
	private void OnBecameVisible()
	{
		if (MonkeyController2D.canRespawnThings)
		{
			Debug.Log("SAD SE VIDI, RNZAAAA");
			base.GetComponent<Renderer>().enabled = true;
		}
	}
}
