using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000469 RID: 1129
public class SelectTypePageManager : MonoBehaviour
{
	// Token: 0x06001E54 RID: 7764 RVA: 0x0001928C File Offset: 0x0001748C
	public void setSelectZhongLei(int type)
	{
		this.selectZhongLei = type;
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x00019295 File Offset: 0x00017495
	public int getSelectZhongLei()
	{
		return this.selectZhongLei;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x0001929D File Offset: 0x0001749D
	public void init()
	{
		this.selectZhongLei = -1;
		this.selectEquipType(1);
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void OpenSelectEquipPanel()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x001071C0 File Offset: 0x001053C0
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

	// Token: 0x06001E59 RID: 7769 RVA: 0x00107344 File Offset: 0x00105544
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

	// Token: 0x06001E5A RID: 7770 RVA: 0x0010745C File Offset: 0x0010565C
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

	// Token: 0x06001E5B RID: 7771 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void closeSelectEquipPanel()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019B8 RID: 6584
	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	// Token: 0x040019B9 RID: 6585
	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	// Token: 0x040019BA RID: 6586
	[SerializeField]
	private List<Sprite> equipTypeIcon = new List<Sprite>();

	// Token: 0x040019BB RID: 6587
	[SerializeField]
	private GameObject lianQiEquipCell;

	// Token: 0x040019BC RID: 6588
	private int selectZhongLei;
}
