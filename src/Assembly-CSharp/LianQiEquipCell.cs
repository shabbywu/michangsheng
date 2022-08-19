using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F0 RID: 752
public class LianQiEquipCell : MonoBehaviour
{
	// Token: 0x06001A22 RID: 6690 RVA: 0x000BABD0 File Offset: 0x000B8DD0
	public void setEquipIcon(Sprite sprite)
	{
		this.equipIcon.sprite = sprite;
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x000BABDE File Offset: 0x000B8DDE
	public void setEquipID(int id)
	{
		this.equipID = id;
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000BABE7 File Offset: 0x000B8DE7
	public int getEquipID()
	{
		return this.equipID;
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x000BABEF File Offset: 0x000B8DEF
	public void setEquipName(string str)
	{
		this.equipName.text = Tools.Code64(str);
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000BAC04 File Offset: 0x000B8E04
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

	// Token: 0x06001A27 RID: 6695 RVA: 0x000BAC87 File Offset: 0x000B8E87
	public void setZhongLei(int type)
	{
		this.zhongLei = type;
	}

	// Token: 0x04001526 RID: 5414
	[SerializeField]
	private Text equipName;

	// Token: 0x04001527 RID: 5415
	[SerializeField]
	private Image equipIcon;

	// Token: 0x04001528 RID: 5416
	private int equipID;

	// Token: 0x04001529 RID: 5417
	private int zhongLei;
}
