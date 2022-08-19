using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class SlotCover : MonoBehaviour
{
	// Token: 0x06000EBB RID: 3771 RVA: 0x00059E66 File Offset: 0x00058066
	private void Start()
	{
		this.inv = base.transform.parent.parent.parent.parent.GetComponent<Inventory>();
		this.rT = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00059E99 File Offset: 0x00058099
	private void Update()
	{
		this.rT.sizeDelta = new Vector3((float)this.inv.slotSize, (float)this.inv.slotSize, 0f);
	}

	// Token: 0x04000AED RID: 2797
	private Inventory inv;

	// Token: 0x04000AEE RID: 2798
	private RectTransform rT;
}
