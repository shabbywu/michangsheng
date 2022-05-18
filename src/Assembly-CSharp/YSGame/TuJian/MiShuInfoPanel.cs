using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DE7 RID: 3559
	public class MiShuInfoPanel : InfoPanelBase1
	{
		// Token: 0x060055D5 RID: 21973 RVA: 0x0023C438 File Offset: 0x0023A638
		public override void RefreshDataList()
		{
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
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[8])
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

		// Token: 0x060055D6 RID: 21974 RVA: 0x0023C728 File Offset: 0x0023A928
		public override void RefreshPanelData()
		{
			base.RefreshPanelData();
			this.RefreshDataList();
			int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
			if (nowSelectID < 1)
			{
				this._HyText.text = "";
				return;
			}
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_8_{0}", nowSelectID);
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
					stringBuilder.Append(string.Format("#c[W]<sprite n=MCS_TJ_Cast_{0}>#n", list[i]));
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
			this._HyText.text = stringBuilder.ToString();
			base.SetSkillIcon(nowSelectID, TuJianDB.ShenTongMiShuQualityData[nowSelectID]);
		}
	}
}
