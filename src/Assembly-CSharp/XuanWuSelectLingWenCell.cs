using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FD RID: 765
public class XuanWuSelectLingWenCell : MonoBehaviour
{
	// Token: 0x06001AAB RID: 6827 RVA: 0x000BDD75 File Offset: 0x000BBF75
	public void showSelect()
	{
		base.gameObject.SetActive(true);
		this.lingWenType = -1;
		if (!this.isInit)
		{
			this.init();
		}
		this.updateBuffIDSelect();
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
	private void updateBuffIDSelect()
	{
		this.lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		Tools.ClearObj(this.buffCell.transform);
		LingWenCell lingWenCell = null;
		jsonData.YSDictionary<string, JSONObject> buffJsonData = jsonData.instance.BuffJsonData;
		int num = 0;
		foreach (int num2 in this.xuanWuDictionary.Keys)
		{
			if (LianQiTotalManager.inst.getCurSelectEquipType() != 2 || num2 != 8)
			{
				LingWenCell component = Tools.InstantiateGameObject(this.buffCell, this.buffCell.transform.parent).GetComponent<LingWenCell>();
				lingWenCell = component;
				component.buffID = num2;
				string str = buffJsonData[num2.ToString()]["name"].str;
				string text = "获得<color=#f5e929>【" + str + "】</color>";
				component.setDesc(text);
				component.xuanWuBuffIDCallBack = new Action<int, string>(this.xuanWuBuffIDClickCallBack);
				if (num == 0)
				{
					int sum = this.xuanWuDictionary[num2][0];
					int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(num2, sum);
					JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
					string str2 = (jsonobject["value3"].I == 1) ? "x" : "+";
					this.setCurSelectContent(text, sum.ToString(), str2, jsonobject["value4"].I.ToString());
					component.showDaoSanJiao();
					LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
					this.updateBuffSum(num2);
				}
				num++;
			}
		}
		lingWenCell.hideFenGeXian();
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000BDF8C File Offset: 0x000BC18C
	private void init()
	{
		this.lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		this.xuanWuDictionary = new Dictionary<int, List<int>>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["type"].I == this.lingWenType)
			{
				if (this.xuanWuDictionary.ContainsKey(list[i]["value1"].I))
				{
					this.xuanWuDictionary[list[i]["value1"].I].Add(list[i]["value2"].I);
				}
				else
				{
					List<int> list2 = new List<int>();
					list2.Add(list[i]["value2"].I);
					this.xuanWuDictionary.Add(list[i]["value1"].I, list2);
				}
			}
		}
		this.isInit = true;
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x000BE0B4 File Offset: 0x000BC2B4
	private void setCurSelectContent(string str1, string str2, string str3, string str4)
	{
		this.xuanWuBUFFDesc.text = Tools.Code64(str1);
		this.BuFFChengShuDesc.text = Tools.Code64("<color=#f5e929>效果x" + str2 + "</color>");
		this.addLingLi.text = Tools.Code64("灵力<color=#f5e929>" + str3 + str4 + "</color>");
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x000BE114 File Offset: 0x000BC314
	private void xuanWuBuffIDClickCallBack(int id, string str1)
	{
		int sum = this.xuanWuDictionary[id][0];
		int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(id, sum);
		string str2 = sum.ToString();
		JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
		string str3 = (jsonobject["value3"].I == 1) ? "x" : "+";
		string str4 = jsonobject["value4"].I.ToString();
		this.setCurSelectContent(str1, str2, str3, str4);
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
		this.updateBuffSum(id);
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
	private void updateBuffSum(int buffID)
	{
		Tools.ClearObj(this.chengShuCell.transform);
		LingWenCell lingWenCell = null;
		List<int> list = this.xuanWuDictionary[buffID];
		for (int i = 0; i < list.Count; i++)
		{
			LingWenCell component = Tools.InstantiateGameObject(this.chengShuCell, this.chengShuCell.transform.parent).GetComponent<LingWenCell>();
			lingWenCell = component;
			component.buffSum = list[i];
			component.buffID = buffID;
			component.xuanWuBuffSumCallBack = new Action<int, int, string>(this.xuanWuSumClickCallBack);
			component.setDesc(string.Format("效果x{0}", list[i]));
			if (i == 0)
			{
				component.showDaoSanJiao();
			}
		}
		lingWenCell.hideFenGeXian();
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x000BE274 File Offset: 0x000BC474
	private void xuanWuSumClickCallBack(int buffID, int sum, string str2)
	{
		int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(buffID, sum);
		JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
		string yunSuanFu = (jsonobject["value3"].I == 1) ? "x" : "+";
		string lingLi = jsonobject["value4"].I.ToString();
		this.setContent(str2, yunSuanFu, lingLi);
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x000BE2FA File Offset: 0x000BC4FA
	private void setContent(string str2, string yunSuanFu, string lingLi)
	{
		this.BuFFChengShuDesc.text = Tools.Code64(str2);
		this.addLingLi.text = Tools.Code64("灵力<color=#f5e929>" + yunSuanFu + lingLi + "</color>");
	}

	// Token: 0x0400156D RID: 5485
	[SerializeField]
	private Text xuanWuBUFFDesc;

	// Token: 0x0400156E RID: 5486
	[SerializeField]
	private Text BuFFChengShuDesc;

	// Token: 0x0400156F RID: 5487
	[SerializeField]
	private Text addLingLi;

	// Token: 0x04001570 RID: 5488
	[SerializeField]
	private GameObject buffCell;

	// Token: 0x04001571 RID: 5489
	[SerializeField]
	private GameObject chengShuCell;

	// Token: 0x04001572 RID: 5490
	public Dictionary<int, List<int>> xuanWuDictionary;

	// Token: 0x04001573 RID: 5491
	private bool isInit;

	// Token: 0x04001574 RID: 5492
	private int lingWenType;
}
