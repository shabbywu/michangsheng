using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000620 RID: 1568
public class openDanlu : MonoBehaviour
{
	// Token: 0x06002705 RID: 9989 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x0001F049 File Offset: 0x0001D249
	private void OnPress()
	{
		LianDanMag.instence.showChoiceDanLu();
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x001320A8 File Offset: 0x001302A8
	private void Update()
	{
		if (this.ItemCellEX.inventory.inventory[0].itemID != -1 && this.ItemCellEX.inventory.inventory[0].Seid.HasField("NaiJiu"))
		{
			this.label.text = (int)this.ItemCellEX.inventory.inventory[0].Seid["NaiJiu"].n + "/" + 100;
		}
	}

	// Token: 0x0400213A RID: 8506
	public UILabel label;

	// Token: 0x0400213B RID: 8507
	public ItemCellEX ItemCellEX;
}
