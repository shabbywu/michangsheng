using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000625 RID: 1573
public class showCaiLiaoImage : MonoBehaviour
{
	// Token: 0x06002719 RID: 10009 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x001321A8 File Offset: 0x001303A8
	public List<int> getChandi(int itemID)
	{
		List<int> list = new List<int>();
		foreach (JSONObject jsonobject in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			for (int i = 1; i <= 8; i++)
			{
				if (jsonobject["value" + i].I == itemID && !list.Contains(jsonobject["id"].I))
				{
					list.Add(jsonobject["id"].I);
				}
			}
		}
		return list;
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x0013225C File Offset: 0x0013045C
	public List<string> getChanDiString(List<int> changdi)
	{
		List<string> list = new List<string>();
		foreach (int num in changdi)
		{
			if (!list.Contains(jsonData.instance.CaiYaoDiaoLuo[num.ToString()]["FuBen"].str))
			{
				list.Add(jsonData.instance.CaiYaoDiaoLuo[num.ToString()]["FuBen"].str);
			}
		}
		return list;
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x00132304 File Offset: 0x00130504
	public void Click()
	{
		Text component = this.TuJianPlan.transform.Find("name").GetComponent<Text>();
		Text component2 = this.TuJianPlan.transform.Find("yaoxiao").GetComponent<Text>();
		Text component3 = this.TuJianPlan.transform.Find("shuoming").GetComponent<Text>();
		Text component4 = this.TuJianPlan.transform.Find("zhuyao").GetComponent<Text>();
		Text component5 = this.TuJianPlan.transform.Find("fuyao").GetComponent<Text>();
		Text component6 = this.TuJianPlan.transform.Find("yaoyin").GetComponent<Text>();
		Image component7 = this.TuJianPlan.transform.Find("Image/Image").GetComponent<Image>();
		JSONObject jsonobject = jsonData.instance.ItemJsonData[this.ItemID.ToString()];
		Avatar player = Tools.instance.getPlayer();
		component.text = Tools.Code64(jsonobject["name"].str);
		List<int> chandi = this.getChandi(this.ItemID);
		string text = "";
		if (chandi.Count != 0)
		{
			using (List<string>.Enumerator enumerator = this.getChanDiString(chandi).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text2 = enumerator.Current;
					bool flag = false;
					foreach (JSONObject jsonobject2 in player.YaoCaiChanDi.list)
					{
						if (jsonData.instance.CaiYaoDiaoLuo[jsonobject2.I.ToString()]["FuBen"].str == text2)
						{
							flag = true;
						}
					}
					if (flag)
					{
						string str = Tools.Code64(jsonData.instance.SceneNameJsonData[text2]["EventName"].str);
						text = text + str + "  ";
					}
					else
					{
						text += "<color=#c94011>未知</color>  ";
					}
				}
				goto IL_221;
			}
		}
		text = "<color=#c94011>无</color>  ";
		IL_221:
		component2.text = "产地：" + text;
		ItemDatebase component8 = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		Sprite sprite = Sprite.Create(component8.items[this.ItemID].itemIcon, new Rect(0f, 0f, (float)component8.items[this.ItemID].itemIcon.width, (float)component8.items[this.ItemID].itemIcon.height), new Vector2(0.5f, 0.5f));
		component7.sprite = sprite;
		component3.text = "说明：" + Tools.Code64(jsonobject["desc2"].str);
		component6.text = "药引：";
		component4.text = "主药：";
		component5.text = "辅药：";
		string liDanLeiXinStr = Tools.getLiDanLeiXinStr((int)jsonobject["yaoZhi2"].n);
		string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr((int)jsonobject["yaoZhi3"].n);
		string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr((int)jsonobject["yaoZhi1"].n);
		Text text3 = component4;
		text3.text += (player.GetHasZhuYaoShuXin(this.ItemID, jsonobject["quality"].I) ? liDanLeiXinStr : "<color=#c94011>未知</color>");
		Text text4 = component5;
		text4.text += (player.GetHasFuYaoShuXin(this.ItemID, jsonobject["quality"].I) ? liDanLeiXinStr2 : "<color=#c94011>未知</color>");
		Text text5 = component6;
		text5.text += (player.GetHasYaoYinShuXin(this.ItemID, jsonobject["quality"].I) ? liDanLeiXinStr3 : "<color=#c94011>未知</color>");
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400213E RID: 8510
	public int ItemID = -1;

	// Token: 0x0400213F RID: 8511
	public Text TextName;

	// Token: 0x04002140 RID: 8512
	public GameObject TuJianPlan;
}
