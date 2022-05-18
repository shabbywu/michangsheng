using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200044B RID: 1099
public class LianQiEquipCell : MonoBehaviour
{
	// Token: 0x06001D46 RID: 7494 RVA: 0x00018669 File Offset: 0x00016869
	public void setEquipIcon(Sprite sprite)
	{
		this.equipIcon.sprite = sprite;
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00018677 File Offset: 0x00016877
	public void setEquipID(int id)
	{
		this.equipID = id;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00018680 File Offset: 0x00016880
	public int getEquipID()
	{
		return this.equipID;
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x00018688 File Offset: 0x00016888
	public void setEquipName(string str)
	{
		this.equipName.text = Tools.Code64(str);
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x00101190 File Offset: 0x000FF390
	public void Onclick()
	{
		if (LianQiTotalManager.inst.selectTypePageManager.checkCanSelect(this.zhongLei))
		{
			LianQiTotalManager.inst.setCurSelectEquipMuBanID(this.equipID);
			LianQiTotalManager.inst.setCurSelectEquipType(_ItemJsonData.DataDict[this.equipID].type + 1);
			LianQiTotalManager.inst.selectTypePageManager.closeSelectEquipPanel();
			LianQiTotalManager.inst.selectTypePageManager.setSelectZhongLei(this.zhongLei);
			LianQiTotalManager.inst.selectEquipCallBack();
		}
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0001869B File Offset: 0x0001689B
	public void setZhongLei(int type)
	{
		this.zhongLei = type;
	}

	// Token: 0x0400192C RID: 6444
	[SerializeField]
	private Text equipName;

	// Token: 0x0400192D RID: 6445
	[SerializeField]
	private Image equipIcon;

	// Token: 0x0400192E RID: 6446
	private int equipID;

	// Token: 0x0400192F RID: 6447
	private int zhongLei;
}
