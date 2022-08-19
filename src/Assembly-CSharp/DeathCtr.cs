using System;
using Fungus;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class DeathCtr : MonoBehaviour
{
	// Token: 0x0600141D RID: 5149 RVA: 0x00082678 File Offset: 0x00080878
	private void Start()
	{
		Object.Destroy(SayDialog.GetSayDialog().gameObject);
	}
}
