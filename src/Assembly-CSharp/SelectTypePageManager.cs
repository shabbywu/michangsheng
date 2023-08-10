using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectTypePageManager : MonoBehaviour
{
	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	[SerializeField]
	private List<Sprite> equipTypeIcon = new List<Sprite>();

	[SerializeField]
	private GameObject lianQiEquipCell;

	private int selectZhongLei;

	public void setSelectZhongLei(int type)
	{
		selectZhongLei = type;
	}

	public int getSelectZhongLei()
	{
		return selectZhongLei;
	}

	public void init()
	{
		selectZhongLei = -1;
		selectEquipType();
	}

	public void OpenSelectEquipPanel()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void selectEquipType(int type = 1)
	{
		if (equipToggles[0].isOn)
		{
			((Component)((Component)equipToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[1];
		}
		else
		{
			((Component)((Component)equipToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[0];
		}
		if (equipToggles[1].isOn)
		{
			((Component)((Component)equipToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[3];
		}
		else
		{
			((Component)((Component)equipToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[2];
		}
		if (equipToggles[2].isOn)
		{
			((Component)((Component)equipToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[5];
		}
		else
		{
			((Component)((Component)equipToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[4];
		}
		if (equipToggles[type - 1].isOn)
		{
			LianQiTotalManager.inst.setCurSelectEquipType(type);
			InitEquipType(type);
		}
	}

	private void InitEquipType(int type)
	{
		Tools.ClearObj(lianQiEquipCell.transform);
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.LianQiEquipType)
		{
			if ((int)item.Value[(object)"zhonglei"] == type)
			{
				LianQiEquipCell component = Tools.InstantiateGameObject(lianQiEquipCell, lianQiEquipCell.transform.parent).GetComponent<LianQiEquipCell>();
				component.setEquipIcon(equipTypeIcon[(int)item.Value[(object)"id"] - 1]);
				component.setEquipName(((object)item.Value[(object)"desc"]).ToString());
				component.setEquipID((int)item.Value[(object)"ItemID"]);
				component.setZhongLei((int)item.Value[(object)"id"]);
			}
		}
	}

	public bool checkCanSelect(int id)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(22);
		string text = "";
		if (wuDaoLevelByType == 0)
		{
			List<JSONObject> list = jsonData.instance.LianQiJieSuoBiao.list;
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i]["content"].list.Count; j++)
				{
					if (list[i]["content"].list[j].I == id)
					{
						text = list[i]["desc"].Str;
						UIPopTip.Inst.Pop("炼器之道需" + text);
						return false;
					}
				}
			}
		}
		else
		{
			List<JSONObject> list2 = jsonData.instance.LianQiJieSuoBiao[wuDaoLevelByType.ToString()]["content"].list;
			for (int k = 0; k < list2.Count; k++)
			{
				if (list2[k].I == id)
				{
					return true;
				}
			}
			List<JSONObject> list3 = jsonData.instance.LianQiJieSuoBiao.list;
			for (int l = wuDaoLevelByType - 1; l < list3.Count; l++)
			{
				for (int m = 0; m < list3[l]["content"].list.Count; m++)
				{
					if (list3[l]["content"].list[m].I == id)
					{
						text = list3[l]["desc"].Str;
						UIPopTip.Inst.Pop("炼器之道需" + text);
						return false;
					}
				}
			}
		}
		return false;
	}

	public void closeSelectEquipPanel()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
