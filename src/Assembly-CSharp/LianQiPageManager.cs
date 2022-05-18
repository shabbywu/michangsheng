using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class LianQiPageManager : MonoBehaviour
{
	// Token: 0x06001D9F RID: 7583 RVA: 0x00018A8D File Offset: 0x00016C8D
	public void init()
	{
		if (LianQiTotalManager.inst.IsFirstSelect)
		{
			LianQiTotalManager.inst.IsFirstSelect = false;
			this.putCaiLiaoCell.init();
		}
		this.showEquipCell.init();
	}

	// Token: 0x06001DA0 RID: 7584 RVA: 0x00102C60 File Offset: 0x00100E60
	public void updateEuipImage()
	{
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		this.showEquipCell.setEquipImage((Sprite)LianQiTotalManager.inst.equipSprites[selectZhongLei]);
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x00018ABC File Offset: 0x00016CBC
	public PutMaterialCell GetCaiLiaoCellByName(string name)
	{
		return this.putCaiLiaoCell.caiLiaoCellDictionary[name];
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x00102C9C File Offset: 0x00100E9C
	public List<PutMaterialCell> GetHasItemSlot()
	{
		List<PutMaterialCell> list = new List<PutMaterialCell>();
		for (int i = 0; i < this.putCaiLiaoCell.caiLiaoCells.Count; i++)
		{
			if (!this.putCaiLiaoCell.caiLiaoCells[i].IsNull())
			{
				list.Add(this.putCaiLiaoCell.caiLiaoCells[i]);
			}
		}
		return list;
	}

	// Token: 0x04001956 RID: 6486
	[SerializeField]
	public PutCaiLiaoCell putCaiLiaoCell;

	// Token: 0x04001957 RID: 6487
	[SerializeField]
	public ShowEquipCell showEquipCell;
}
