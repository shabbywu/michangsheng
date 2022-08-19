using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000AB5 RID: 2741
	public class YaoShouInfoPanel : InfoPanelBase
	{
		// Token: 0x06004CD7 RID: 19671 RVA: 0x0020DC01 File Offset: 0x0020BE01
		public void Start()
		{
			this.Init();
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0020DC09 File Offset: 0x0020BE09
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0020DC18 File Offset: 0x0020BE18
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

		// Token: 0x06004CDA RID: 19674 RVA: 0x0020DE38 File Offset: 0x0020C038
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

		// Token: 0x06004CDB RID: 19675 RVA: 0x0020E054 File Offset: 0x0020C254
		public void Init()
		{
			this._YaoShouImage = base.transform.Find("YaoShouMask/YaoShouImage").GetComponent<Image>();
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0020E0B4 File Offset: 0x0020C2B4
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
		}

		// Token: 0x04004BE2 RID: 19426
		private Image _YaoShouImage;

		// Token: 0x04004BE3 RID: 19427
		protected RectTransform _HyContentTransform;

		// Token: 0x04004BE4 RID: 19428
		protected SymbolText _HyText;

		// Token: 0x04004BE5 RID: 19429
		public Color HyTextColor1 = new Color(0.12156863f, 0.37254903f, 0.54901963f);

		// Token: 0x04004BE6 RID: 19430
		public Color HyTextHoverColor1 = new Color(0.105882354f, 0.3137255f, 0.4627451f);

		// Token: 0x04004BE7 RID: 19431
		public Color HyTextColor2 = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x04004BE8 RID: 19432
		public Color HyTextHoverColor2 = new Color(0.3764706f, 0.21960784f, 0.02745098f);

		// Token: 0x04004BE9 RID: 19433
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
