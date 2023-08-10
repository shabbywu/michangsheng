using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class MiShuInfoPanel : InfoPanelBase1
{
	public override void RefreshDataList()
	{
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(2, 0);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[8];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[8])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && TuJianDB.ShenTongMiShuQualityData[key] != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianItemTab.Inst.ShuXingDropdown.value > 0)
					{
						if (!TuJianDB.ShenTongMiShuShuXingData[key].Contains(TuJianItemTab.Inst.ShuXingDropdown.options[TuJianItemTab.Inst.ShuXingDropdown.value].text))
						{
							flag = false;
						}
						else if (!TuJianManager.IsDebugMode && !TuJianManager.Inst.IsUnlockedSkill(key))
						{
							flag = false;
						}
					}
					if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(TuJianDB.ShenTongMiShuDesc1Data[key]))
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
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_8_{nowSelectID}";
		bool flag = TuJianManager.Inst.IsUnlockedSkill(nowSelectID) || TuJianManager.IsDebugMode;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + TuJianDB.ShenTongMiShuNameData[nowSelectID] + "<pos v=0.67 t=1>#c449491品级：#n" + TuJianDB.ShenTongMiShuPinJiData[nowSelectID]);
		stringBuilder.Append("\n\n#c449491属性：#n");
		if (flag)
		{
			stringBuilder.Append(TuJianDB.ShenTongMiShuShuXingData[nowSelectID] ?? "");
		}
		else
		{
			stringBuilder.Append("未知");
		}
		stringBuilder.Append("\n\n#c449491消耗：#n");
		if (flag)
		{
			List<int> list = TuJianDB.ShenTongMiShuCastData[nowSelectID];
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append($"#c[W]<sprite n=MCS_TJ_Cast_{list[i]}>#n");
			}
		}
		else
		{
			stringBuilder.Append("未知");
		}
		stringBuilder.Append("\n\n#c449491说明：#n");
		if (flag)
		{
			stringBuilder.Append(TuJianDB.MiShuDesc2Data[nowSelectID]);
		}
		else
		{
			stringBuilder.Append("未知");
		}
		stringBuilder.Append("\n\n#c449491#s34介绍：#n#s24");
		if (flag)
		{
			stringBuilder.Append(TuJianDB.ShenTongMiShuDesc1Data[nowSelectID]);
		}
		else
		{
			stringBuilder.Append("未知");
		}
		((Text)_HyText).text = stringBuilder.ToString();
		SetSkillIcon(nowSelectID, TuJianDB.ShenTongMiShuQualityData[nowSelectID]);
	}
}
