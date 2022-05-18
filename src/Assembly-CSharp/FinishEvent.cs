using System;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
public class FinishEvent : MonoBehaviour
{
	// Token: 0x06002A68 RID: 10856 RVA: 0x00020F65 File Offset: 0x0001F165
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey")
		{
			base.GetComponent<Collider2D>().enabled = false;
			GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>().Finish();
		}
	}
}
