using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class SlotCover : MonoBehaviour
{
	// Token: 0x060010DB RID: 4315 RVA: 0x000107AB File Offset: 0x0000E9AB
	private void Start()
	{
		this.inv = base.transform.parent.parent.parent.parent.GetComponent<Inventory>();
		this.rT = base.GetComponent<RectTransform>();
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000107DE File Offset: 0x0000E9DE
	private void Update()
	{
		this.rT.sizeDelta = new Vector3((float)this.inv.slotSize, (float)this.inv.slotSize, 0f);
	}

	// Token: 0x04000D88 RID: 3464
	private Inventory inv;

	// Token: 0x04000D89 RID: 3465
	private RectTransform rT;
}
