using JSONClass;
using UnityEngine;
using UnityEngine.UI;

public class LianQiEquipCell : MonoBehaviour
{
	[SerializeField]
	private Text equipName;

	[SerializeField]
	private Image equipIcon;

	private int equipID;

	private int zhongLei;

	public void setEquipIcon(Sprite sprite)
	{
		equipIcon.sprite = sprite;
	}

	public void setEquipID(int id)
	{
		equipID = id;
	}

	public int getEquipID()
	{
		return equipID;
	}

	public void setEquipName(string str)
	{
		equipName.text = Tools.Code64(str);
	}

	public void Onclick()
	{
		if (LianQiTotalManager.inst.selectTypePageManager.checkCanSelect(zhongLei))
		{
			LianQiTotalManager.inst.setCurSelectEquipMuBanID(equipID);
			LianQiTotalManager.inst.setCurSelectEquipType(_ItemJsonData.DataDict[equipID].type + 1);
			LianQiTotalManager.inst.selectTypePageManager.closeSelectEquipPanel();
			LianQiTotalManager.inst.selectTypePageManager.setSelectZhongLei(zhongLei);
			LianQiTotalManager.inst.selectEquipCallBack();
		}
	}

	public void setZhongLei(int type)
	{
		zhongLei = type;
	}
}
