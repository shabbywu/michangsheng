using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class openDanlu : MonoBehaviour
{
	// Token: 0x0600234F RID: 9039 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x000F1E04 File Offset: 0x000F0004
	private void OnPress()
	{
		LianDanMag.instence.showChoiceDanLu();
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x000F1E10 File Offset: 0x000F0010
	private void Update()
	{
		if (this.ItemCellEX.inventory.inventory[0].itemID != -1 && this.ItemCellEX.inventory.inventory[0].Seid.HasField("NaiJiu"))
		{
			this.label.text = (int)this.ItemCellEX.inventory.inventory[0].Seid["NaiJiu"].n + "/" + 100;
		}
	}

	// Token: 0x04001C65 RID: 7269
	public UILabel label;

	// Token: 0x04001C66 RID: 7270
	public ItemCellEX ItemCellEX;
}
