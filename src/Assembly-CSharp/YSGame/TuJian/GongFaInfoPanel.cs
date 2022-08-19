using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AA5 RID: 2725
	public class GongFaInfoPanel : InfoPanelBase2
	{
		// Token: 0x06004C6B RID: 19563 RVA: 0x0020A2C4 File Offset: 0x002084C4
		public override void RefreshDataList()
		{
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
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[7])
					{
						int key = source.First<KeyValuePair<int, string>>().Key;
						string value = source.First<KeyValuePair<int, string>>().Value;
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

		// Token: 0x06004C6C RID: 19564 RVA: 0x0020A5C4 File Offset: 0x002087C4
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
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_7_{0}", nowSelectID);
			object obj = TuJianManager.Inst.IsUnlockedGongFa(nowSelectID) || TuJianManager.IsDebugMode;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("#c449491名称：#n" + TuJianDB.GongFaNameData[nowSelectID]);
			stringBuilder.Append("\n\n#c449491品级：#n" + TuJianDB.GongFaPinJiData[nowSelectID]);
			string str = "未知";
			object obj2 = obj;
			if (obj2 != null)
			{
				str = TuJianDB.GongFaShuXingData[nowSelectID];
			}
			stringBuilder.Append("\n\n#c449491属性：#n" + str);
			stringBuilder.Append("\n\n#c449491基础修炼速度：#n");
			if (obj2 != null)
			{
				int num = TuJianDB.GongFaSpeedData[nowSelectID];
				stringBuilder.Append(string.Format("{0}/月", num));
			}
			else
			{
				stringBuilder.Append("未知");
			}
			stringBuilder.Append("\n\n#c449491#s34介绍：#n#s24");
			if (obj2 != null)
			{
				if (TuJianDB.GongFaDesc1Data.ContainsKey(nowSelectID))
				{
					stringBuilder.Append(TuJianDB.GongFaDesc1Data[nowSelectID] ?? "");
				}
				else
				{
					stringBuilder.Append(string.Format("找不到id为{0}的功法描述，请反馈官方", nowSelectID));
				}
			}
			else
			{
				stringBuilder.Append("未知");
			}
			this._HyText.text = stringBuilder.ToString();
			stringBuilder.Clear();
			if (obj2 != null)
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
			this._HyText2.text = stringBuilder.ToString();
			base.SetGongFaIcon(nowSelectID, TuJianDB.GongFaQualityData[nowSelectID]);
		}
	}
}
