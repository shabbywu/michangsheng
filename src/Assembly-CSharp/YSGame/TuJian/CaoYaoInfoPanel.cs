using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AA2 RID: 2722
	public class CaoYaoInfoPanel : InfoPanelBase1
	{
		// Token: 0x06004C57 RID: 19543 RVA: 0x00208BEC File Offset: 0x00206DEC
		public override void RefreshDataList()
		{
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
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[1])
					{
						int key = source.First<KeyValuePair<int, string>>().Key;
						string value = source.First<KeyValuePair<int, string>>().Value;
						JSONObject jsonobject = key.ItemJson();
						bool flag = true;
						if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && jsonobject["quality"].I != TuJianItemTab.Inst.PinJieDropdown.value)
						{
							flag = false;
						}
						if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(jsonobject["desc2"].Str))
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

		// Token: 0x06004C58 RID: 19544 RVA: 0x00208E7C File Offset: 0x0020707C
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
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_1_{0}", nowSelectID);
			JSONObject jsonobject = nowSelectID.ItemJson();
			bool flag = TuJianManager.Inst.IsUnlockedItem(nowSelectID) || TuJianManager.IsDebugMode;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new string[]
			{
				"#c449491名称：#n",
				jsonobject["name"].str.ToCN(),
				"<pos v=0.72 t=1>#c449491品级：#n",
				jsonobject["quality"].I.ToCNNumber(),
				"品\n\n"
			}));
			stringBuilder.Append("#c449491类型：#n草药\n\n");
			string liDanLeiXinStr = Tools.getLiDanLeiXinStr(jsonobject["yaoZhi2"].I);
			string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(jsonobject["yaoZhi3"].I);
			string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr(jsonobject["yaoZhi1"].I);
			bool flag2 = TuJianManager.Inst.IsUnlockedZhuYao(nowSelectID) || TuJianManager.IsDebugMode;
			bool flag3 = TuJianManager.Inst.IsUnlockedFuYao(nowSelectID) || TuJianManager.IsDebugMode;
			bool flag4 = TuJianManager.Inst.IsUnlockedYaoYin(nowSelectID) || TuJianManager.IsDebugMode;
			stringBuilder.Append(string.Concat(new string[]
			{
				"#c449491主药：#n",
				flag2 ? liDanLeiXinStr : "未知",
				" <pos v=0.36 t=1>#c449491辅药：#n",
				flag3 ? liDanLeiXinStr2 : "未知",
				" <pos v=0.72 t=1>#c449491药引：#n",
				flag4 ? liDanLeiXinStr3 : "未知",
				"\n\n"
			}));
			stringBuilder.Append("#c449491产地：#n#c" + ColorUtility.ToHtmlStringRGB(this.HyTextColor));
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			foreach (JSONObject jsonobject2 in jsonData.instance.CaiYaoDiaoLuo.list)
			{
				int i = 1;
				while (i <= 8)
				{
					if (jsonobject2[string.Format("value{0}", i)].I == nowSelectID)
					{
						string str = jsonobject2["FuBen"].str;
						if (!list.Contains(str))
						{
							if (TuJianManager.IsDebugMode || TuJianManager.Inst.IsUnlockedMap(str))
							{
								list.Add(jsonobject2["FuBen"].str);
								list2.Add(TuJianDB.GetMapNameByID(str));
							}
							else
							{
								list.Add(str);
								list2.Add("None");
							}
							list3.Add(string.Format("Map{0}", TuJianDB.GetMapHighlightIDByMapID(str)));
							break;
						}
						break;
					}
					else
					{
						i++;
					}
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
					stringBuilder.Append(string.Concat(new string[]
					{
						"<hy t=",
						list2[j],
						" l=",
						list3[j],
						" fhc=#",
						ColorUtility.ToHtmlStringRGB(this.HyTextHoverColor),
						" ul=1>"
					}));
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
				stringBuilder.Append(jsonobject["desc2"].Str ?? "");
			}
			else
			{
				stringBuilder.Append("未知");
			}
			if (TuJianManager.IsDebugMode)
			{
				stringBuilder.Append(string.Format("\n\n#s34#RID:{0}", nowSelectID));
			}
			this._HyText.text = stringBuilder.ToString();
			base.SetItemIcon(nowSelectID);
		}
	}
}
