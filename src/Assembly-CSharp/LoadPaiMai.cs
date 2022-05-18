using System;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class LoadPaiMai : MonoBehaviour
{
	// Token: 0x060020E2 RID: 8418 RVA: 0x0001B1E0 File Offset: 0x000193E0
	private void Awake()
	{
		ResManager.inst.LoadPrefab("PaiMai/PaiMaiPanel").Inst(null);
	}
}
