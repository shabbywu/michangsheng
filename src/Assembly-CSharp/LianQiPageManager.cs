using System.Collections.Generic;
using UnityEngine;

public class LianQiPageManager : MonoBehaviour
{
	[SerializeField]
	public PutCaiLiaoCell putCaiLiaoCell;

	[SerializeField]
	public ShowEquipCell showEquipCell;

	public void init()
	{
		if (LianQiTotalManager.inst.IsFirstSelect)
		{
			LianQiTotalManager.inst.IsFirstSelect = false;
			putCaiLiaoCell.init();
		}
		showEquipCell.init();
	}

	public void updateEuipImage()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		showEquipCell.setEquipImage((Sprite)LianQiTotalManager.inst.equipSprites[selectZhongLei]);
	}

	public PutMaterialCell GetCaiLiaoCellByName(string name)
	{
		return putCaiLiaoCell.caiLiaoCellDictionary[name];
	}

	public List<PutMaterialCell> GetHasItemSlot()
	{
		List<PutMaterialCell> list = new List<PutMaterialCell>();
		for (int i = 0; i < putCaiLiaoCell.caiLiaoCells.Count; i++)
		{
			if (!putCaiLiaoCell.caiLiaoCells[i].IsNull())
			{
				list.Add(putCaiLiaoCell.caiLiaoCells[i]);
			}
		}
		return list;
	}
}
