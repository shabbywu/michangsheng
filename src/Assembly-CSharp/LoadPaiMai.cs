using System;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class LoadPaiMai : MonoBehaviour
{
	// Token: 0x06001D7D RID: 7549 RVA: 0x000D0543 File Offset: 0x000CE743
	private void Awake()
	{
		ResManager.inst.LoadPrefab("PaiMai/PaiMaiPanel").Inst(null);
	}
}
