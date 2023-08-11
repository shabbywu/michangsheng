using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class GongFaInfoPanel : InfoPanelBase2
{
	public override void RefreshDataList()
	{
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(2, 13);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianItemTab.Inst.ShuXingDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[7];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[7])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && TuJianDB.GongFaQualityData[key] != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianItemTab.Inst.ShuXingDropdown.value > 0)
					{
						if (!TuJianDB.GongFaShuXingData[key].Contains(TuJianItemTab.Inst.ShuXingDropdown.options[TuJianItemTab.Inst.ShuXingDropdown.value].text))
						{
							flag = false;
						}
						else if (!TuJianManager.IsDebugMode && !TuJianManager.Inst.IsUnlockedGongFa(key))
						{
							flag = false;
						}
					}
					if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(TuJianDB.GongFaDesc1Data[key]))
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
		base.RefreshPanelData();
		RefreshDataList();
		int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
		if (nowSelectID < 1)
		{
			((Text)_HyText).text = "";
			((Text)_HyText2).text = "";
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_7_{nowSelectID}";
		bool num = TuJianManager.Inst.IsUnlockedGongFa(nowSelectID) || TuJianManager.IsDebugMode;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + TuJianDB.GongFaNameData[nowSelectID]);
		stringBuilder.Append("\n\n#c449491品级：#n" + TuJianDB.GongFaPinJiData[nowSelectID]);
		string text = "未知";
		if (num)
		{
			text = TuJianDB.GongFaShuXingData[nowSelectID];
		}
		stringBuilder.Append("\n\n#c449491属性：#n" + text);
		stringBuilder.Append("\n\n#c449491基础修炼速度：#n");
		if (num)
		{
			int num2 = TuJianDB.GongFaSpeedData[nowSelectID];
			stringBuilder.Append($"{num2}/月");
		}
		else
		{
			stringBuilder.Append("未知");
		}
		stringBuilder.Append("\n\n#c449491#s34介绍：#n#s24");
		if (num)
		{
			if (TuJianDB.GongFaDesc1Data.ContainsKey(nowSelectID))
			{
				stringBuilder.Append(TuJianDB.GongFaDesc1Data[nowSelectID] ?? "");
			}
			else
			{
				stringBuilder.Append($"找不到id为{nowSelectID}的功法描述，请反馈官方");
			}
		}
		else
		{
			stringBuilder.Append("未知");
		}
		((Text)_HyText).text = stringBuilder.ToString();
		stringBuilder.Clear();
		if (num)
		{
			List<string> list = TuJianDB.GongFaDesc2Data[nowSelectID];
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append("#s28#c07464e第" + (i + 1).ToCNNumber() + "层：#n");
				stringBuilder.Append("#s28" + list[i] + "\n");
			}
		}
		else
		{
			stringBuilder.Append("#s28未知");
		}
		((Text)_HyText2).text = stringBuilder.ToString();
		SetGongFaIcon(nowSelectID, TuJianDB.GongFaQualityData[nowSelectID]);
	}
}
