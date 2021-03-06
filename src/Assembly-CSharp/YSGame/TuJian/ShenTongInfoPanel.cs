using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DE9 RID: 3561
	public class ShenTongInfoPanel : InfoPanelBase2
	{
		// Token: 0x060055E4 RID: 21988 RVA: 0x0023D0C8 File Offset: 0x0023B2C8
		public override void RefreshDataList()
		{
			base.RefreshDataList();
			TuJianItemTab.Inst.SetDropdown(2, 12);
			if (TuJianManager.Inst.NeedRefreshDataList)
			{
				if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianItemTab.Inst.ShuXingDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
				{
					TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[6];
				}
				else
				{
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[6])
					{
						int key = source.First<KeyValuePair<int, string>>().Key;
						string value = source.First<KeyValuePair<int, string>>().Value;
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
							this.DataList.Add(new Dictionary<int, string>
							{
								{
									key,
									value
								}
							});
						}
					}
					TuJianItemTab.Inst.FilterSSV.DataList = this.DataList;
				}
				if (TuJianItemTab.Inst.FilterSSV.DataList.Count == 0)
				{
					this._ItemIconImage.color = new Color(0f, 0f, 0f, 0f);
					this._QualityImage.color = new Color(0f, 0f, 0f, 0f);
					this._QualityUpImage.color = new Color(0f, 0f, 0f, 0f);
				}
				else
				{
					this._ItemIconImage.color = Color.white;
					this._QualityImage.color = Color.white;
					this._QualityUpImage.color = Color.white;
				}
				TuJianManager.Inst.NeedRefreshDataList = false;
			}
			if (this.isOnHyperlink)
			{
				TuJianItemTab.Inst.FilterSSV.NowSelectID = this.hylinkArgs[2];
				TuJianItemTab.Inst.FilterSSV.NeedResetToTop = false;
				this.isOnHyperlink = false;
			}
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x0023D3C8 File Offset: 0x0023B5C8
		public override void RefreshPanelData()
		{
			base.RefreshPanelData();
			this.RefreshDataList();
			int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
			if (nowSelectID < 1)
			{
				this._HyText.text = "";
				this._HyText2.text = "";
				return;
			}
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_6_{0}", nowSelectID);
			bool flag = TuJianManager.Inst.IsUnlockedSkill(nowSelectID) || TuJianManager.IsDebugMode;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("#c449491名称：#n" + TuJianDB.ShenTongMiShuNameData[nowSelectID]);
			stringBuilder.Append("\n\n#c449491品级：#n" + TuJianDB.ShenTongMiShuPinJiData[nowSelectID]);
			string str = "未知";
			if (flag)
			{
				str = TuJianDB.ShenTongMiShuShuXingData[nowSelectID];
			}
			stringBuilder.Append("\n\n#c449491属性：#n" + str);
			stringBuilder.Append("\n\n#c449491消耗：#n");
			if (flag)
			{
				List<int> list = TuJianDB.ShenTongMiShuCastData[nowSelectID];
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.Append(string.Format("#c[W]<sprite n=MCS_TJ_Cast_{0}>#n", list[i]));
				}
			}
			else
			{
				stringBuilder.Append("未知");
			}
			stringBuilder.Append("\n\n#c449491#s34介绍：#n#s24");
			if (flag)
			{
				if (TuJianDB.ShenTongMiShuDesc1Data.ContainsKey(nowSelectID))
				{
					stringBuilder.Append(TuJianDB.ShenTongMiShuDesc1Data[nowSelectID] ?? "");
				}
				else
				{
					stringBuilder.Append(string.Format("找不到id为{0}的神通描述，请反馈官方", nowSelectID));
				}
			}
			else
			{
				stringBuilder.Append("未知");
			}
			this._HyText.text = stringBuilder.ToString();
			stringBuilder.Clear();
			if (flag)
			{
				List<string> list2 = TuJianDB.ShenTongDesc2Data[nowSelectID];
				for (int j = 0; j < list2.Count; j++)
				{
					stringBuilder.Append("#s28#c07464e" + TuJianDB.LevelNames[j] + "：#n");
					stringBuilder.Append("#s28" + list2[j] + "\n");
				}
			}
			else
			{
				stringBuilder.Append("#s28未知");
			}
			this._HyText2.text = stringBuilder.ToString();
			base.SetSkillIcon(nowSelectID, TuJianDB.ShenTongMiShuQualityData[nowSelectID]);
		}
	}
}
