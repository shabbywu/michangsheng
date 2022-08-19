using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030C RID: 780
public class SelectTypePageManager : MonoBehaviour
{
	// Token: 0x06001B2E RID: 6958 RVA: 0x000C1A7B File Offset: 0x000BFC7B
	public void setSelectZhongLei(int type)
	{
		this.selectZhongLei = type;
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x000C1A84 File Offset: 0x000BFC84
	public int getSelectZhongLei()
	{
		return this.selectZhongLei;
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x000C1A8C File Offset: 0x000BFC8C
	public void init()
	{
		this.selectZhongLei = -1;
		this.selectEquipType(1);
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void OpenSelectEquipPanel()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x000C1A9C File Offset: 0x000BFC9C
	public void selectEquipType(int type = 1)
	{
		if (this.equipToggles[0].isOn)
		{
			this.equipToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[1];
		}
		else
		{
			this.equipToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[0];
		}
		if (this.equipToggles[1].isOn)
		{
			this.equipToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[3];
		}
		else
		{
			this.equipToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[2];
		}
		if (this.equipToggles[2].isOn)
		{
			this.equipToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[5];
		}
		else
		{
			this.equipToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[4];
		}
		if (this.equipToggles[type - 1].isOn)
		{
			LianQiTotalManager.inst.setCurSelectEquipType(type);
			this.InitEquipType(type);
		}
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x000C1C20 File Offset: 0x000BFE20
	private void InitEquipType(int type)
	{
		Tools.ClearObj(this.lianQiEquipCell.transform);
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.LianQiEquipType)
		{
			if ((int)keyValuePair.Value["zhonglei"] == type)
			{
				LianQiEquipCell component = Tools.InstantiateGameObject(this.lianQiEquipCell, this.lianQiEquipCell.transform.parent).GetComponent<LianQiEquipCell>();
				component.setEquipIcon(this.equipTypeIcon[(int)keyValuePair.Value["id"] - 1]);
				component.setEquipName(keyValuePair.Value["desc"].ToString());
				component.setEquipID((int)keyValuePair.Value["ItemID"]);
				component.setZhongLei((int)keyValuePair.Value["id"]);
			}
		}
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x000C1D38 File Offset: 0x000BFF38
	public bool checkCanSelect(int id)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(22);
		if (wuDaoLevelByType == 0)
		{
			List<JSONObject> list = jsonData.instance.LianQiJieSuoBiao.list;
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i]["content"].list.Count; j++)
				{
					if (list[i]["content"].list[j].I == id)
					{
						string str = list[i]["desc"].Str;
						UIPopTip.Inst.Pop("炼器之道需" + str, PopTipIconType.叹号);
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
						string str = list3[l]["desc"].Str;
						UIPopTip.Inst.Pop("炼器之道需" + str, PopTipIconType.叹号);
						return false;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void closeSelectEquipPanel()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040015AB RID: 5547
	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	// Token: 0x040015AC RID: 5548
	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	// Token: 0x040015AD RID: 5549
	[SerializeField]
	private List<Sprite> equipTypeIcon = new List<Sprite>();

	// Token: 0x040015AE RID: 5550
	[SerializeField]
	private GameObject lianQiEquipCell;

	// Token: 0x040015AF RID: 5551
	private int selectZhongLei;
}
