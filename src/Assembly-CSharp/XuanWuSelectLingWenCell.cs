using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XuanWuSelectLingWenCell : MonoBehaviour
{
	[SerializeField]
	private Text xuanWuBUFFDesc;

	[SerializeField]
	private Text BuFFChengShuDesc;

	[SerializeField]
	private Text addLingLi;

	[SerializeField]
	private GameObject buffCell;

	[SerializeField]
	private GameObject chengShuCell;

	public Dictionary<int, List<int>> xuanWuDictionary;

	private bool isInit;

	private int lingWenType;

	public void showSelect()
	{
		((Component)this).gameObject.SetActive(true);
		lingWenType = -1;
		if (!isInit)
		{
			init();
		}
		updateBuffIDSelect();
	}

	private void updateBuffIDSelect()
	{
		lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		Tools.ClearObj(buffCell.transform);
		LingWenCell lingWenCell = null;
		jsonData.YSDictionary<string, JSONObject> buffJsonData = jsonData.instance.BuffJsonData;
		int num = 0;
		foreach (int key in xuanWuDictionary.Keys)
		{
			if (LianQiTotalManager.inst.getCurSelectEquipType() != 2 || key != 8)
			{
				LingWenCell component = Tools.InstantiateGameObject(buffCell, buffCell.transform.parent).GetComponent<LingWenCell>();
				lingWenCell = component;
				component.buffID = key;
				string str = buffJsonData[key.ToString()]["name"].str;
				string text = "获得<color=#f5e929>【" + str + "】</color>";
				component.setDesc(text);
				component.xuanWuBuffIDCallBack = xuanWuBuffIDClickCallBack;
				if (num == 0)
				{
					int sum = xuanWuDictionary[key][0];
					int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(key, sum);
					JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
					string str2 = ((jSONObject["value3"].I == 1) ? "x" : "+");
					setCurSelectContent(text, sum.ToString(), str2, jSONObject["value4"].I.ToString());
					component.showDaoSanJiao();
					LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
					updateBuffSum(key);
				}
				num++;
			}
		}
		lingWenCell.hideFenGeXian();
	}

	private void init()
	{
		lingWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		xuanWuDictionary = new Dictionary<int, List<int>>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["type"].I == lingWenType)
			{
				if (xuanWuDictionary.ContainsKey(list[i]["value1"].I))
				{
					xuanWuDictionary[list[i]["value1"].I].Add(list[i]["value2"].I);
					continue;
				}
				List<int> list2 = new List<int>();
				list2.Add(list[i]["value2"].I);
				xuanWuDictionary.Add(list[i]["value1"].I, list2);
			}
		}
		isInit = true;
	}

	private void setCurSelectContent(string str1, string str2, string str3, string str4)
	{
		xuanWuBUFFDesc.text = Tools.Code64(str1);
		BuFFChengShuDesc.text = Tools.Code64("<color=#f5e929>效果x" + str2 + "</color>");
		addLingLi.text = Tools.Code64("灵力<color=#f5e929>" + str3 + str4 + "</color>");
	}

	private void xuanWuBuffIDClickCallBack(int id, string str1)
	{
		int sum = xuanWuDictionary[id][0];
		int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(id, sum);
		string str2 = sum.ToString();
		JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
		string str3 = ((jSONObject["value3"].I == 1) ? "x" : "+");
		string str4 = jSONObject["value4"].I.ToString();
		setCurSelectContent(str1, str2, str3, str4);
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
		updateBuffSum(id);
	}

	private void updateBuffSum(int buffID)
	{
		Tools.ClearObj(chengShuCell.transform);
		LingWenCell lingWenCell = null;
		List<int> list = xuanWuDictionary[buffID];
		for (int i = 0; i < list.Count; i++)
		{
			LingWenCell component = Tools.InstantiateGameObject(chengShuCell, chengShuCell.transform.parent).GetComponent<LingWenCell>();
			lingWenCell = component;
			component.buffSum = list[i];
			component.buffID = buffID;
			component.xuanWuBuffSumCallBack = xuanWuSumClickCallBack;
			component.setDesc($"效果x{list[i]}");
			if (i == 0)
			{
				component.showDaoSanJiao();
			}
		}
		lingWenCell.hideFenGeXian();
	}

	private void xuanWuSumClickCallBack(int buffID, int sum, string str2)
	{
		int lingWenIDByBUFFIDAndSum = LianQiTotalManager.inst.getLingWenIDByBUFFIDAndSum(buffID, sum);
		JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[lingWenIDByBUFFIDAndSum.ToString()];
		string yunSuanFu = ((jSONObject["value3"].I == 1) ? "x" : "+");
		string lingLi = jSONObject["value4"].I.ToString();
		setContent(str2, yunSuanFu, lingLi);
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(lingWenIDByBUFFIDAndSum);
	}

	private void setContent(string str2, string yunSuanFu, string lingLi)
	{
		BuFFChengShuDesc.text = Tools.Code64(str2);
		addLingLi.text = Tools.Code64("灵力<color=#f5e929>" + yunSuanFu + lingLi + "</color>");
	}
}
