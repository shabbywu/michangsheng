using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DF2 RID: 3570
	public class YaoShouInfoPanel : InfoPanelBase
	{
		// Token: 0x06005624 RID: 22052 RVA: 0x0003DAB5 File Offset: 0x0003BCB5
		public void Start()
		{
			this.Init();
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x0003DABD File Offset: 0x0003BCBD
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x0023E7F0 File Offset: 0x0023C9F0
		public override void RefreshDataList()
		{
			base.RefreshDataList();
			TuJianItemTab.Inst.SetDropdown(3, 0);
			if (TuJianManager.Inst.NeedRefreshDataList)
			{
				if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
				{
					TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[5];
				}
				else
				{
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[5])
					{
						int key = source.First<KeyValuePair<int, string>>().Key;
						string value = source.First<KeyValuePair<int, string>>().Value;
						bool flag = true;
						if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && YaoShouInfoPanel.LevelDropdownDict[TuJianDB.YaoShouLevelNameData[key]] != TuJianItemTab.Inst.PinJieDropdown.value)
						{
							flag = false;
						}
						if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(TuJianDB.YaoShouDescData[key]))
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
					this._YaoShouImage.color = new Color(0f, 0f, 0f, 0f);
				}
				else
				{
					this._YaoShouImage.color = Color.white;
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

		// Token: 0x06005627 RID: 22055 RVA: 0x0023EA10 File Offset: 0x0023CC10
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
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_5_{0}", nowSelectID);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("#c449491名称：#n" + TuJianDB.YaoShouNameData[nowSelectID] + " <pos v=0.62 t=1>#c449491境界：#n" + TuJianDB.YaoShouLevelNameData[nowSelectID]);
			stringBuilder.Append("\n\n#c449491产出：#n");
			for (int i = 0; i < TuJianDB.YaoShouChanChuData[nowSelectID].Count; i++)
			{
				int num = TuJianDB.YaoShouChanChuData[nowSelectID][i];
				JSONObject jsonobject = num.ItemJson();
				string text = jsonobject["name"].str.ToCN();
				stringBuilder.Append(string.Concat(new string[]
				{
					"<hy t=",
					text,
					" l=",
					string.Format("1_{0}_{1}", jsonobject["TuJianType"].I, num),
					" fc=#",
					ColorUtility.ToHtmlStringRGB(this.HyTextColor1),
					" fhc=#",
					ColorUtility.ToHtmlStringRGB(this.HyTextHoverColor1),
					" ul=1>"
				}));
				if (i != TuJianDB.YaoShouChanChuData[nowSelectID].Count - 1)
				{
					stringBuilder.Append("，");
				}
			}
			stringBuilder.Append("\n\n#c449491栖息：#n");
			string mapID = TuJianDB.YaoShouQiXiMapData[nowSelectID];
			stringBuilder.Append("#c" + ColorUtility.ToHtmlStringRGB(this.HyTextColor2) + TuJianDB.GetMapNameByID(mapID) + "#n");
			stringBuilder.Append("\n\n#c449491介绍：#n#s24");
			stringBuilder.Append(TuJianDB.YaoShouDescData[nowSelectID] ?? "");
			this._HyText.text = stringBuilder.ToString();
			this._YaoShouImage.sprite = TuJianDB.GetYaoShouFace(nowSelectID);
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0023EC2C File Offset: 0x0023CE2C
		public void Init()
		{
			this._YaoShouImage = base.transform.Find("YaoShouMask/YaoShouImage").GetComponent<Image>();
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x0023EC8C File Offset: 0x0023CE8C
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
		}

		// Token: 0x040055C0 RID: 21952
		private Image _YaoShouImage;

		// Token: 0x040055C1 RID: 21953
		protected RectTransform _HyContentTransform;

		// Token: 0x040055C2 RID: 21954
		protected SymbolText _HyText;

		// Token: 0x040055C3 RID: 21955
		public Color HyTextColor1 = new Color(0.12156863f, 0.37254903f, 0.54901963f);

		// Token: 0x040055C4 RID: 21956
		public Color HyTextHoverColor1 = new Color(0.105882354f, 0.3137255f, 0.4627451f);

		// Token: 0x040055C5 RID: 21957
		public Color HyTextColor2 = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x040055C6 RID: 21958
		public Color HyTextHoverColor2 = new Color(0.3764706f, 0.21960784f, 0.02745098f);

		// Token: 0x040055C7 RID: 21959
		private static Dictionary<string, int> LevelDropdownDict = new Dictionary<string, int>
		{
			{
				"炼气期",
				1
			},
			{
				"筑基期",
				2
			},
			{
				"金丹期",
				3
			},
			{
				"元婴期",
				4
			},
			{
				"化神期",
				5
			}
		};
	}
}
