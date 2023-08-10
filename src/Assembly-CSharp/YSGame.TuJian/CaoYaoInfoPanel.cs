using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class CaoYaoInfoPanel : InfoPanelBase1
{
	public override void RefreshDataList()
	{
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(1, 0);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[1];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[1])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					JSONObject jSONObject = key.ItemJson();
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && jSONObject["quality"].I != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(jSONObject["desc2"].Str))
					{
						flag = false;
					}
					if (flag)
					{
						DataList.Add(new Dictionary<int, string> { { key, value } });
					}
				}
				TuJianItemTab.Inst.FilterSSV.DataList = DataList;
			}
			if (TuJianItemTab.Inst.FilterSSV.DataList.Count == 0)
			{
				((Graphic)_ItemIconImage).color = new Color(0f, 0f, 0f, 0f);
				((Graphic)_QualityImage).color = new Color(0f, 0f, 0f, 0f);
				((Graphic)_QualityUpImage).color = new Color(0f, 0f, 0f, 0f);
			}
			else
			{
				((Graphic)_ItemIconImage).color = Color.white;
				((Graphic)_QualityImage).color = Color.white;
				((Graphic)_QualityUpImage).color = Color.white;
			}
			TuJianManager.Inst.NeedRefreshDataList = false;
		}
		if (isOnHyperlink)
		{
			TuJianItemTab.Inst.FilterSSV.NowSelectID = hylinkArgs[2];
			TuJianItemTab.Inst.FilterSSV.NeedResetToTop = false;
			isOnHyperlink = false;
		}
	}

	public override void RefreshPanelData()
	{
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshPanelData();
		RefreshDataList();
		int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
		if (nowSelectID < 1)
		{
			((Text)_HyText).text = "";
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_1_{nowSelectID}";
		JSONObject jSONObject = nowSelectID.ItemJson();
		bool flag = TuJianManager.Inst.IsUnlockedItem(nowSelectID) || TuJianManager.IsDebugMode;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + jSONObject["name"].str.ToCN() + "<pos v=0.72 t=1>#c449491品级：#n" + jSONObject["quality"].I.ToCNNumber() + "品\n\n");
		stringBuilder.Append("#c449491类型：#n草药\n\n");
		string liDanLeiXinStr = Tools.getLiDanLeiXinStr(jSONObject["yaoZhi2"].I);
		string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(jSONObject["yaoZhi3"].I);
		string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr(jSONObject["yaoZhi1"].I);
		bool flag2 = TuJianManager.Inst.IsUnlockedZhuYao(nowSelectID) || TuJianManager.IsDebugMode;
		bool flag3 = TuJianManager.Inst.IsUnlockedFuYao(nowSelectID) || TuJianManager.IsDebugMode;
		bool flag4 = TuJianManager.Inst.IsUnlockedYaoYin(nowSelectID) || TuJianManager.IsDebugMode;
		stringBuilder.Append("#c449491主药：#n" + (flag2 ? liDanLeiXinStr : "未知") + " <pos v=0.36 t=1>#c449491辅药：#n" + (flag3 ? liDanLeiXinStr2 : "未知") + " <pos v=0.72 t=1>#c449491药引：#n" + (flag4 ? liDanLeiXinStr3 : "未知") + "\n\n");
		stringBuilder.Append("#c449491产地：#n#c" + ColorUtility.ToHtmlStringRGB(HyTextColor));
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		foreach (JSONObject item in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			for (int i = 1; i <= 8; i++)
			{
				if (item[$"value{i}"].I != nowSelectID)
				{
					continue;
				}
				string str = item["FuBen"].str;
				if (!list.Contains(str))
				{
					if (TuJianManager.IsDebugMode || TuJianManager.Inst.IsUnlockedMap(str))
					{
						list.Add(item["FuBen"].str);
						list2.Add(TuJianDB.GetMapNameByID(str));
					}
					else
					{
						list.Add(str);
						list2.Add("None");
					}
					list3.Add($"Map{TuJianDB.GetMapHighlightIDByMapID(str)}");
				}
				break;
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list2[j] == "None")
			{
				stringBuilder.Append("未知");
			}
			else
			{
				stringBuilder.Append("<hy t=" + list2[j] + " l=" + list3[j] + " fhc=#" + ColorUtility.ToHtmlStringRGB(HyTextHoverColor) + " ul=1>");
			}
			if (j != list.Count - 1)
			{
				stringBuilder.Append("，");
			}
		}
		if (list.Count == 0)
		{
			stringBuilder.Append("无");
		}
		stringBuilder.Append("\n\n");
		stringBuilder.Append("#c449491介绍：#n#s24");
		if (flag)
		{
			stringBuilder.Append(jSONObject["desc2"].Str ?? "");
		}
		else
		{
			stringBuilder.Append("未知");
		}
		if (TuJianManager.IsDebugMode)
		{
			stringBuilder.Append($"\n\n#s34#RID:{nowSelectID}");
		}
		((Text)_HyText).text = stringBuilder.ToString();
		SetItemIcon(nowSelectID);
	}
}
