using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class showCaiLiaoImage : MonoBehaviour
{
	public int ItemID = -1;

	public Text TextName;

	public GameObject TuJianPlan;

	private void Start()
	{
	}

	public List<int> getChandi(int itemID)
	{
		List<int> list = new List<int>();
		foreach (JSONObject item in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			for (int i = 1; i <= 8; i++)
			{
				if (item["value" + i].I == itemID && !list.Contains(item["id"].I))
				{
					list.Add(item["id"].I);
				}
			}
		}
		return list;
	}

	public List<string> getChanDiString(List<int> changdi)
	{
		List<string> list = new List<string>();
		foreach (int item in changdi)
		{
			if (!list.Contains(jsonData.instance.CaiYaoDiaoLuo[item.ToString()]["FuBen"].str))
			{
				list.Add(jsonData.instance.CaiYaoDiaoLuo[item.ToString()]["FuBen"].str);
			}
		}
		return list;
	}

	public void Click()
	{
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		Text component = ((Component)TuJianPlan.transform.Find("name")).GetComponent<Text>();
		Text component2 = ((Component)TuJianPlan.transform.Find("yaoxiao")).GetComponent<Text>();
		Text component3 = ((Component)TuJianPlan.transform.Find("shuoming")).GetComponent<Text>();
		Text component4 = ((Component)TuJianPlan.transform.Find("zhuyao")).GetComponent<Text>();
		Text component5 = ((Component)TuJianPlan.transform.Find("fuyao")).GetComponent<Text>();
		Text component6 = ((Component)TuJianPlan.transform.Find("yaoyin")).GetComponent<Text>();
		Image component7 = ((Component)TuJianPlan.transform.Find("Image/Image")).GetComponent<Image>();
		JSONObject jSONObject = jsonData.instance.ItemJsonData[ItemID.ToString()];
		Avatar player = Tools.instance.getPlayer();
		component.text = Tools.Code64(jSONObject["name"].str);
		List<int> chandi = getChandi(ItemID);
		string text = "";
		if (chandi.Count != 0)
		{
			foreach (string item in getChanDiString(chandi))
			{
				bool flag = false;
				foreach (JSONObject item2 in player.YaoCaiChanDi.list)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[item2.I.ToString()]["FuBen"].str == item)
					{
						flag = true;
					}
				}
				if (flag)
				{
					string text2 = Tools.Code64(jsonData.instance.SceneNameJsonData[item]["EventName"].str);
					text = text + text2 + "  ";
				}
				else
				{
					text += "<color=#c94011>未知</color>  ";
				}
			}
		}
		else
		{
			text = "<color=#c94011>无</color>  ";
		}
		component2.text = "产地：" + text;
		ItemDatebase component8 = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		Sprite sprite = Sprite.Create(component8.items[ItemID].itemIcon, new Rect(0f, 0f, (float)((Texture)component8.items[ItemID].itemIcon).width, (float)((Texture)component8.items[ItemID].itemIcon).height), new Vector2(0.5f, 0.5f));
		component7.sprite = sprite;
		component3.text = "说明：" + Tools.Code64(jSONObject["desc2"].str);
		component6.text = "药引：";
		component4.text = "主药：";
		component5.text = "辅药：";
		string liDanLeiXinStr = Tools.getLiDanLeiXinStr((int)jSONObject["yaoZhi2"].n);
		string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr((int)jSONObject["yaoZhi3"].n);
		string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr((int)jSONObject["yaoZhi1"].n);
		component4.text += (player.GetHasZhuYaoShuXin(ItemID, jSONObject["quality"].I) ? liDanLeiXinStr : "<color=#c94011>未知</color>");
		component5.text += (player.GetHasFuYaoShuXin(ItemID, jSONObject["quality"].I) ? liDanLeiXinStr2 : "<color=#c94011>未知</color>");
		component6.text += (player.GetHasYaoYinShuXin(ItemID, jSONObject["quality"].I) ? liDanLeiXinStr3 : "<color=#c94011>未知</color>");
	}

	private void Update()
	{
	}
}
