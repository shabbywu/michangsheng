using System;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class FinishEvent : MonoBehaviour
{
	// Token: 0x060025F0 RID: 9712 RVA: 0x00106C52 File Offset: 0x00104E52
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			base.GetComponent<Collider2D>().enabled = false;
			GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().Finish();
		}
	}
}
