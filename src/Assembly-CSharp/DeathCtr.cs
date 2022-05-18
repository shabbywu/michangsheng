using System;
using Fungus;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class DeathCtr : MonoBehaviour
{
	// Token: 0x060016BC RID: 5820 RVA: 0x00014256 File Offset: 0x00012456
	private void Start()
	{
		Object.Destroy(SayDialog.GetSayDialog().gameObject);
	}
}
