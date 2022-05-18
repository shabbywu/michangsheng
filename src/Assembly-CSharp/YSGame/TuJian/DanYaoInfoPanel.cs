using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DE1 RID: 3553
	public class DanYaoInfoPanel : InfoPanelBase
	{
		// Token: 0x060055AC RID: 21932 RVA: 0x0003D4B3 File Offset: 0x0003B6B3
		private void Start()
		{
			this.Init();
			this.SetDanFangNumber(1);
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0023A634 File Offset: 0x00238834
		public void Init()
		{
			this._QualityImage = base.transform.Find("ItemBG/QualityBg").GetComponent<Image>();
			this._ItemIconImage = base.transform.Find("ItemBG/QualityBg/ItemIcon").GetComponent<Image>();
			this._QualityUpImage = base.transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp").GetComponent<Image>();
			this._HyText1 = base.transform.Find("InfoText1").GetComponent<SymbolText>();
			this._HyText2 = base.transform.Find("InfoText2").GetComponent<SymbolText>();
			this._HyText3 = base.transform.Find("DanFangInfoBG/InfoText").GetComponent<SymbolText>();
			this._NumberText = base.transform.Find("DanFangInfoBG/NumberText").GetComponent<Text>();
			this._LeftButton = base.transform.Find("DanFangInfoBG/LeftBtn").GetComponent<Button>();
			this._RightButton = base.transform.Find("DanFangInfoBG/RightBtn").GetComponent<Button>();
			this._LeftButton.onClick.AddListener(new UnityAction(this.LastDanFang));
			this._RightButton.onClick.AddListener(new UnityAction(this.NextDanFang));
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0023A76C File Offset: 0x0023896C
		public override void RefreshDataList()
		{
			base.RefreshDataList();
			TuJianItemTab.Inst.SetDropdown(1, 0);
			if (TuJianManager.Inst.NeedRefreshDataList)
			{
				if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
				{
					TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[4];
				}
				else
				{
					this.DataList.Clear();
					foreach (Dictionary<int, string> source in TuJianDB.ItemTuJianFilterData[4])
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
					this._NumberText.gameObject.SetActive(false);
					this._LeftButton.gameObject.SetActive(false);
					this._RightButton.gameObject.SetActive(false);
				}
				else
				{
					this._ItemIconImage.color = Color.white;
					this._QualityImage.color = Color.white;
					this._QualityUpImage.color = Color.white;
					this._NumberText.gameObject.SetActive(true);
					this._LeftButton.gameObject.SetActive(true);
					this._RightButton.gameObject.SetActive(true);
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

		// Token: 0x060055AF RID: 21935 RVA: 0x0023AA68 File Offset: 0x00238C68
		public override void RefreshPanelData()
		{
			base.RefreshPanelData();
			this.RefreshDataList();
			int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
			if (nowSelectID < 1)
			{
				this._HyText1.text = "";
				this._HyText2.text = "";
				this._HyText3.text = "";
				return;
			}
			TuJianManager.Inst.NowPageHyperlink = string.Format("1_4_{0}", nowSelectID);
			JSONObject jsonobject = nowSelectID.ItemJson();
			bool flag = TuJianManager.Inst.IsUnlockedItem(nowSelectID) || TuJianManager.IsDebugMode;
			Avatar player = global::Tools.instance.getPlayer();
			bool flag2 = player != null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new string[]
			{
				"#c449491名称：#n",
				jsonobject["name"].Str,
				" <pos v=0.68 t=1>#c449491品级：#n",
				jsonobject["quality"].I.ToCNNumber(),
				"品"
			}));
			stringBuilder.Append("\n\n#c449491类型：#n丹药");
			int i = jsonobject["CanUse"].I;
			int i2 = jsonobject["DanDu"].I;
			if (flag)
			{
				if (flag2)
				{
					stringBuilder.Append(string.Format("\n\n#c449491耐药：#n{0}<pos v=0.68 t=1>#c449491丹毒：#n{1}", string.Format("{0}/{1}", global::Tools.getJsonobject(player.NaiYaoXin, nowSelectID.ToString()), i), i2));
				}
				else
				{
					stringBuilder.Append(string.Format("\n\n#c449491耐药：#n{0}<pos v=0.68 t=1>#c449491丹毒：#n{1}", i, i2));
				}
			}
			else
			{
				stringBuilder.Append("\n\n#c449491耐药：#n未知<pos v=0.68 t=1>#c449491丹毒：#n未知");
			}
			stringBuilder.Append("\n\n#c449491药效：#n");
			if (flag)
			{
				stringBuilder.Append(jsonobject["desc"].Str ?? "");
			}
			else
			{
				stringBuilder.Append("未知");
			}
			this._HyText1.text = stringBuilder.ToString();
			stringBuilder.Clear();
			if (flag2)
			{
				this.RefreshDanFangList(nowSelectID);
				if (this.playerDanFangList.Count > 0)
				{
					if (this.danFangNumber > this.playerDanFangList.Count)
					{
						this.danFangNumber = 1;
					}
					DanFangData danFangData = this.playerDanFangList[this.danFangNumber - 1];
					string value = "";
					if (danFangData.YaoCaiTypeCount == 4)
					{
						value = "#s13 \n";
					}
					else if (danFangData.YaoCaiTypeCount == 3)
					{
						value = "#s30 \n";
					}
					else if (danFangData.YaoCaiTypeCount == 2)
					{
						value = "\n#s13 \n";
					}
					stringBuilder.Append(value);
					stringBuilder.Append(this.YaoCaoStr(0, danFangData.YaoYinID, danFangData.YaoYinCount));
					stringBuilder.Append(this.YaoCaoStr(1, danFangData.ZhuYao1ID, danFangData.ZhuYao1Count));
					stringBuilder.Append(this.YaoCaoStr(1, danFangData.ZhuYao2ID, danFangData.ZhuYao2Count));
					stringBuilder.Append(this.YaoCaoStr(2, danFangData.FuYao1ID, danFangData.FuYao1Count));
					stringBuilder.Append(this.YaoCaoStr(2, danFangData.FuYao2ID, danFangData.FuYao2Count));
				}
				else
				{
					this.SetDanFangNumber(1);
					stringBuilder.Append("#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[0] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[1] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[1] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[2] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[2] + "：未知#n");
				}
			}
			else
			{
				DanFangData danFangData2 = TuJianDB.DanFangDataDict[nowSelectID];
				if (flag)
				{
					string value2 = "";
					if (danFangData2.YaoCaiTypeCount == 4)
					{
						value2 = "#s13 \n";
					}
					else if (danFangData2.YaoCaiTypeCount == 3)
					{
						value2 = "#s30 \n";
					}
					else if (danFangData2.YaoCaiTypeCount == 2)
					{
						value2 = "\n#s13 \n";
					}
					stringBuilder.Append(value2);
					stringBuilder.Append(this.YaoCaoStr(0, danFangData2.YaoYinID, danFangData2.YaoYinCount));
					stringBuilder.Append(this.YaoCaoStr(1, danFangData2.ZhuYao1ID, danFangData2.ZhuYao1Count));
					stringBuilder.Append(this.YaoCaoStr(1, danFangData2.ZhuYao2ID, danFangData2.ZhuYao2Count));
					stringBuilder.Append(this.YaoCaoStr(2, danFangData2.FuYao1ID, danFangData2.FuYao1Count));
					stringBuilder.Append(this.YaoCaoStr(2, danFangData2.FuYao2ID, danFangData2.FuYao2Count));
				}
				else
				{
					this.SetDanFangNumber(1);
					stringBuilder.Append("#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[0] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[1] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[1] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[2] + "：未知#n");
					stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + DanYaoInfoPanel.typeStr[2] + "：未知#n");
				}
			}
			this._HyText3.text = stringBuilder.ToString();
			stringBuilder.Clear();
			stringBuilder.Append("#c449491介绍：#n#s24");
			if (flag)
			{
				stringBuilder.Append(jsonobject["desc2"].Str ?? "");
			}
			else
			{
				stringBuilder.Append("未知");
			}
			this._HyText2.text = stringBuilder.ToString();
			this.SetItemIcon(nowSelectID);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0023B040 File Offset: 0x00239240
		private string YaoCaoStr(int strIndex, int id, int count)
		{
			if (count > 0)
			{
				JSONObject jsonobject = id.ItemJson();
				string text = TuJianDB.YaoCaoTypeData[jsonobject[string.Format("yaoZhi{0}", strIndex + 1)].I];
				return string.Concat(new string[]
				{
					"#s27<pos v=0.28 t=1>",
					DanYaoInfoPanel.typeStr[strIndex],
					"(",
					text,
					")：<hy t=",
					string.Format("{0}x{1}", jsonobject["name"].Str, count),
					" l=",
					string.Format("1_1_{0}", id),
					" fc=#",
					ColorUtility.ToHtmlStringRGB(this.HyTextColor),
					" fhc=#",
					ColorUtility.ToHtmlStringRGB(this.HyTextHoverColor),
					" ul=1>#n\n"
				});
			}
			return "";
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x0023B134 File Offset: 0x00239334
		private void RefreshDanFangList(int id)
		{
			List<JSONObject> list = global::Tools.instance.getPlayer().DanFang.list;
			this.playerDanFangList.Clear();
			foreach (JSONObject jsonobject in list)
			{
				if (jsonobject["ID"].I == id)
				{
					DanFangData danFangData = new DanFangData();
					danFangData.ItemID = jsonobject["ID"].I;
					danFangData.YaoYinID = jsonobject["Type"][0].I;
					danFangData.ZhuYao1ID = jsonobject["Type"][1].I;
					danFangData.ZhuYao2ID = jsonobject["Type"][2].I;
					danFangData.FuYao1ID = jsonobject["Type"][3].I;
					danFangData.FuYao2ID = jsonobject["Type"][4].I;
					danFangData.YaoYinCount = jsonobject["Num"][0].I;
					danFangData.ZhuYao1Count = jsonobject["Num"][1].I;
					danFangData.ZhuYao2Count = jsonobject["Num"][2].I;
					danFangData.FuYao1Count = jsonobject["Num"][3].I;
					danFangData.FuYao2Count = jsonobject["Num"][4].I;
					danFangData.CalcYaoCaiTypeCount();
					this.playerDanFangList.Add(danFangData);
				}
			}
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x0003D4C2 File Offset: 0x0003B6C2
		public void SetDanFangNumber(int number)
		{
			this._NumberText.text = string.Format("丹方{0}：", number);
			this.danFangNumber = number;
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x0023B308 File Offset: 0x00239508
		public void NextDanFang()
		{
			if (global::Tools.instance.getPlayer() == null)
			{
				return;
			}
			if (this.playerDanFangList.Count > 0)
			{
				this.danFangNumber++;
				if (this.danFangNumber > this.playerDanFangList.Count)
				{
					this.danFangNumber = 1;
				}
				this.SetDanFangNumber(this.danFangNumber);
				this.RefreshPanelData();
			}
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x0023B36C File Offset: 0x0023956C
		public void LastDanFang()
		{
			if (global::Tools.instance.getPlayer() == null)
			{
				return;
			}
			if (this.playerDanFangList.Count > 0)
			{
				this.danFangNumber--;
				if (this.danFangNumber < 1)
				{
					this.danFangNumber = this.playerDanFangList.Count;
				}
				this.SetDanFangNumber(this.danFangNumber);
				this.RefreshPanelData();
			}
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x0003D4E6 File Offset: 0x0003B6E6
		public void SetItemIcon(int id)
		{
			this._ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
			this._QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
		}

		// Token: 0x04005563 RID: 21859
		private Image _QualityImage;

		// Token: 0x04005564 RID: 21860
		private Image _QualityUpImage;

		// Token: 0x04005565 RID: 21861
		private Image _ItemIconImage;

		// Token: 0x04005566 RID: 21862
		private SymbolText _HyText1;

		// Token: 0x04005567 RID: 21863
		private SymbolText _HyText2;

		// Token: 0x04005568 RID: 21864
		private SymbolText _HyText3;

		// Token: 0x04005569 RID: 21865
		private Text _NumberText;

		// Token: 0x0400556A RID: 21866
		private Button _LeftButton;

		// Token: 0x0400556B RID: 21867
		private Button _RightButton;

		// Token: 0x0400556C RID: 21868
		public Color HyTextColor = new Color(0.21568628f, 0.5764706f, 0.4117647f);

		// Token: 0x0400556D RID: 21869
		public Color HyTextHoverColor = new Color(0.19607843f, 0.50980395f, 0.36862746f);

		// Token: 0x0400556E RID: 21870
		private static readonly string[] typeStr = new string[]
		{
			"药引",
			"主药",
			"辅药"
		};

		// Token: 0x0400556F RID: 21871
		private List<DanFangData> playerDanFangList = new List<DanFangData>();

		// Token: 0x04005570 RID: 21872
		private int danFangNumber;
	}
}
