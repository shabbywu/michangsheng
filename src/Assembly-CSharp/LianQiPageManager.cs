using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class LianQiPageManager : MonoBehaviour
{
	// Token: 0x06001A79 RID: 6777 RVA: 0x000BCAB4 File Offset: 0x000BACB4
	public void init()
	{
		if (LianQiTotalManager.inst.IsFirstSelect)
		{
			LianQiTotalManager.inst.IsFirstSelect = false;
			this.putCaiLiaoCell.init();
		}
		this.showEquipCell.init();
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000BCAE4 File Offset: 0x000BACE4
	public void updateEuipImage()
	{
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		this.showEquipCell.setEquipImage((Sprite)LianQiTotalManager.inst.equipSprites[selectZhongLei]);
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x000BCB1D File Offset: 0x000BAD1D
	public PutMaterialCell GetCaiLiaoCellByName(string name)
	{
		return this.putCaiLiaoCell.caiLiaoCellDictionary[name];
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x000BCB30 File Offset: 0x000BAD30
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

	// Token: 0x04001549 RID: 5449
	[SerializeField]
	public PutCaiLiaoCell putCaiLiaoCell;

	// Token: 0x0400154A RID: 5450
	[SerializeField]
	public ShowEquipCell showEquipCell;
}
