using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DE6 RID: 3558
	public class KuangShiInfoPanel : InfoPanelBase1
	{
		// Token: 0x060055D1 RID: 21969 RVA: 0x0023BC70 File Offset: 0x00239E70
		public override void RefreshDataList()
		{
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
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[2])
					{
						int key = source.First<KeyValuePair<int, string>>().Key;
						string value = source.First<KeyValuePair<int, string>>().Value;
						JSONObject jsonobject = key.ItemJson();
						bool flag = true;
						if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && jsonobject["quality"].I != TuJianItemTab.Inst.PinJieDropdown.value)
						{
							flag = false;
						}
						if (TuJianItemTab.Inst.ShuXingDropdown.value > 0)
						{
							if (KuangShiInfoPanel.ShuXingDropdownDict[jsonobject["ShuXingType"].I] != TuJianItemTab.Inst.ShuXingDropdown.value)
							{
								flag = false;
							}
							else if (!TuJianManager.IsDebugMode && !TuJianManager.Inst.IsUnlockedItem(key))
							{
								flag = false;
							}
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

		// Token: 0x060055D2 RID: 21970 RVA: 0x0023BF78 File Offset: 0x0023A178
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
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_2_{0}", nowSelectID);
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
			stringBuilder.Append("#c449491类型：#n矿石\n\n");
			int i = jsonobject["WuWeiType"].I;
			string lqshuXingTypeName = TuJianDB.GetLQShuXingTypeName(jsonobject["ShuXingType"].I);
			float num = 0.61f - (float)(lqshuXingTypeName.Length - 4) * 0.05f;
			string lqwuWeiTypeName = TuJianDB.GetLQWuWeiTypeName(i);
			if (flag)
			{
				stringBuilder.Append(string.Concat(new string[]
				{
					"#c449491种类：#n",
					lqwuWeiTypeName,
					" <pos v=",
					num.ToString("f2"),
					" t=1>#c449491属性：#n",
					lqshuXingTypeName,
					"\n\n"
				}));
			}
			else
			{
				stringBuilder.Append("#c449491种类：#n未知 <pos v=0.72 t=1>#c449491属性：#n未知\n\n");
			}
			stringBuilder.Append("#c449491产地：#n#c" + ColorUtility.ToHtmlStringRGB(this.HyTextColor));
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			foreach (JSONObject jsonobject2 in jsonData.instance.CaiYaoDiaoLuo.list)
			{
				int j = 1;
				while (j <= 8)
				{
					if (jsonobject2[string.Format("value{0}", j)].I == nowSelectID)
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
						j++;
					}
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
					stringBuilder.Append(string.Concat(new string[]
					{
						"<hy t=",
						list2[k],
						" l=",
						list3[k],
						" fhc=#",
						ColorUtility.ToHtmlStringRGB(this.HyTextHoverColor),
						" ul=1>"
					}));
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
				stringBuilder.Append(jsonobject["desc2"].str.ToCN() ?? "");
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

		// Token: 0x04005587 RID: 21895
		private static Dictionary<int, int> ShuXingDropdownDict = new Dictionary<int, int>
		{
			{
				1,
				1
			},
			{
				11,
				2
			},
			{
				21,
				3
			},
			{
				31,
				4
			},
			{
				41,
				5
			},
			{
				71,
				6
			},
			{
				51,
				7
			},
			{
				61,
				8
			},
			{
				2,
				1
			},
			{
				12,
				2
			},
			{
				22,
				3
			},
			{
				32,
				4
			},
			{
				42,
				5
			},
			{
				72,
				6
			},
			{
				52,
				7
			},
			{
				62,
				8
			}
		};
	}
}
