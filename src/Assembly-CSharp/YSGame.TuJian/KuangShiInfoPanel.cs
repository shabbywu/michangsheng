using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class KuangShiInfoPanel : InfoPanelBase1
{
	private static Dictionary<int, int> ShuXingDropdownDict = new Dictionary<int, int>
	{
		{ 1, 1 },
		{ 11, 2 },
		{ 21, 3 },
		{ 31, 4 },
		{ 41, 5 },
		{ 71, 6 },
		{ 51, 7 },
		{ 61, 8 },
		{ 2, 1 },
		{ 12, 2 },
		{ 22, 3 },
		{ 32, 4 },
		{ 42, 5 },
		{ 72, 6 },
		{ 52, 7 },
		{ 62, 8 }
	};

	public override void RefreshDataList()
	{
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(1, 11);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianItemTab.Inst.ShuXingDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[2];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[2])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					JSONObject jSONObject = key.ItemJson();
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && jSONObject["quality"].I != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianItemTab.Inst.ShuXingDropdown.value > 0)
					{
						if (ShuXingDropdownDict[jSONObject["ShuXingType"].I] != TuJianItemTab.Inst.ShuXingDropdown.value)
						{
							flag = false;
						}
						else if (!TuJianManager.IsDebugMode && !TuJianManager.Inst.IsUnlockedItem(key))
						{
							flag = false;
						}
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
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshPanelData();
		RefreshDataList();
		int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
		if (nowSelectID < 1)
		{
			((Text)_HyText).text = "";
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_2_{nowSelectID}";
		JSONObject jSONObject = nowSelectID.ItemJson();
		bool flag = TuJianManager.Inst.IsUnlockedItem(nowSelectID) || TuJianManager.IsDebugMode;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + jSONObject["name"].str.ToCN() + "<pos v=0.72 t=1>#c449491品级：#n" + jSONObject["quality"].I.ToCNNumber() + "品\n\n");
		stringBuilder.Append("#c449491类型：#n矿石\n\n");
		int i = jSONObject["WuWeiType"].I;
		string lQShuXingTypeName = TuJianDB.GetLQShuXingTypeName(jSONObject["ShuXingType"].I);
		float num = 0.61f - (float)(lQShuXingTypeName.Length - 4) * 0.05f;
		string lQWuWeiTypeName = TuJianDB.GetLQWuWeiTypeName(i);
		if (flag)
		{
			stringBuilder.Append("#c449491种类：#n" + lQWuWeiTypeName + " <pos v=" + num.ToString("f2") + " t=1>#c449491属性：#n" + lQShuXingTypeName + "\n\n");
		}
		else
		{
			stringBuilder.Append("#c449491种类：#n未知 <pos v=0.72 t=1>#c449491属性：#n未知\n\n");
		}
		stringBuilder.Append("#c449491产地：#n#c" + ColorUtility.ToHtmlStringRGB(HyTextColor));
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		foreach (JSONObject item in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			for (int j = 1; j <= 8; j++)
			{
				if (item[$"value{j}"].I != nowSelectID)
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
		for (int k = 0; k < list.Count; k++)
		{
			if (list2[k] == "None")
			{
				stringBuilder.Append("未知");
			}
			else
			{
				stringBuilder.Append("<hy t=" + list2[k] + " l=" + list3[k] + " fhc=#" + ColorUtility.ToHtmlStringRGB(HyTextHoverColor) + " ul=1>");
			}
			if (k != list.Count - 1)
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
			stringBuilder.Append(jSONObject["desc2"].str.ToCN() ?? "");
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
