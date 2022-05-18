using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DEB RID: 3563
	public class TuJianItemTab : TuJianTab
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x0003D7E6 File Offset: 0x0003B9E6
		// (set) Token: 0x060055F4 RID: 22004 RVA: 0x0003D7EE File Offset: 0x0003B9EE
		private int NowPanelID
		{
			get
			{
				return this.nowPanelID;
			}
			set
			{
				this.nowPanelID = value;
				if (this.nowPanelID == 4)
				{
					this.ZheZhaoDown.SetActive(false);
					return;
				}
				this.ZheZhaoDown.SetActive(true);
			}
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x0023D740 File Offset: 0x0023B940
		public override void Awake()
		{
			TuJianItemTab.Inst = this;
			this.TabType = TuJianTabType.Item;
			this.TypeSSV = base.transform.Find("Root/TuJianTypeSV").GetComponent<SuperScrollView>();
			this.FilterSSV = base.transform.Find("Root/TuJianItemsSV").GetComponent<SuperScrollView>();
			this.ZheZhaoDown = base.transform.Find("Root/InfoRoot/ZheZhaoDown").gameObject;
			this.PinJieDropdown = base.transform.Find("Root/PinJieDropdown").GetComponent<Dropdown>();
			this.ShuXingDropdown = base.transform.Find("Root/ShuXingDropdown").GetComponent<Dropdown>();
			this.MiShuInfoPanel = base.transform.GetComponentInChildren<MiShuInfoPanel>(true);
			this.CaoYaoInfoPanel = base.transform.GetComponentInChildren<CaoYaoInfoPanel>(true);
			this.GongFaInfoPanel = base.transform.GetComponentInChildren<GongFaInfoPanel>(true);
			this.DanYaoInfoPanel = base.transform.GetComponentInChildren<DanYaoInfoPanel>(true);
			this.YaoShouInfoPanel = base.transform.GetComponentInChildren<YaoShouInfoPanel>(true);
			this.ShenTongInfoPanel = base.transform.GetComponentInChildren<ShenTongInfoPanel>(true);
			this.KuangShiInfoPanel = base.transform.GetComponentInChildren<KuangShiInfoPanel>(true);
			this.YaoShouCaiLiaoInfoPanel = base.transform.GetComponentInChildren<YaoShouCaiLiaoInfoPanel>(true);
			this.PanelDict.Add(1, this.CaoYaoInfoPanel);
			this.PanelDict.Add(2, this.KuangShiInfoPanel);
			this.PanelDict.Add(3, this.YaoShouCaiLiaoInfoPanel);
			this.PanelDict.Add(4, this.DanYaoInfoPanel);
			this.PanelDict.Add(5, this.YaoShouInfoPanel);
			this.PanelDict.Add(6, this.ShenTongInfoPanel);
			this.PanelDict.Add(7, this.GongFaInfoPanel);
			this.PanelDict.Add(8, this.MiShuInfoPanel);
			this.TypeSSV.DataList = new List<Dictionary<int, string>>
			{
				new Dictionary<int, string>
				{
					{
						1,
						"草药"
					}
				},
				new Dictionary<int, string>
				{
					{
						2,
						"矿物"
					}
				},
				new Dictionary<int, string>
				{
					{
						3,
						"材料"
					}
				},
				new Dictionary<int, string>
				{
					{
						4,
						"丹药"
					}
				},
				new Dictionary<int, string>
				{
					{
						5,
						"妖兽"
					}
				},
				new Dictionary<int, string>
				{
					{
						6,
						"神通"
					}
				},
				new Dictionary<int, string>
				{
					{
						7,
						"功法"
					}
				},
				new Dictionary<int, string>
				{
					{
						8,
						"秘术"
					}
				}
			};
			this.PinJieDropdown.onValueChanged.AddListener(delegate(int i)
			{
				TuJianManager.Inst.NeedRefreshDataList = true;
				this.RefreshPanel(false);
			});
			this.ShuXingDropdown.onValueChanged.AddListener(delegate(int i)
			{
				TuJianManager.Inst.NeedRefreshDataList = true;
				this.RefreshPanel(false);
				this.FilterSSV.NeedResetToTop = true;
			});
			base.Awake();
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x0003D819 File Offset: 0x0003BA19
		public override void Show()
		{
			base.Show();
			this.RefreshPanel(false);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x0003D828 File Offset: 0x0003BA28
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x0023DA08 File Offset: 0x0023BC08
		public void SetDropdown(int pinJie, int shuXing)
		{
			if (this.nowPinJieDropdownType != pinJie)
			{
				this.nowPinJieDropdownType = pinJie;
				if (pinJie == 0)
				{
					this.PinJieDropdown.gameObject.SetActive(false);
				}
				else
				{
					this.PinJieDropdown.gameObject.SetActive(true);
					this.PinJieDropdown.ClearOptions();
					this.PinJieDropdown.AddOptions(this.options[pinJie]);
					this.PinJieDropdown.value = 0;
				}
				TuJianManager.Inst.NeedRefreshDataList = true;
			}
			if (this.nowShuXingDropdownTyp != shuXing)
			{
				this.nowShuXingDropdownTyp = shuXing;
				if (shuXing == 0)
				{
					this.ShuXingDropdown.gameObject.SetActive(false);
				}
				else
				{
					this.ShuXingDropdown.gameObject.SetActive(true);
					this.ShuXingDropdown.ClearOptions();
					this.ShuXingDropdown.AddOptions(this.options[shuXing]);
					this.ShuXingDropdown.value = 0;
				}
				TuJianManager.Inst.NeedRefreshDataList = true;
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x0023DAF8 File Offset: 0x0023BCF8
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
			TuJianManager.Inst.ClearSearch();
			this.TypeSSV.NowSelectID = args[1];
			this.PanelDict[args[1]].OnHyperlink(args);
			this.RefreshPanel(true);
			this.FilterSSV.ResetToTopByHyperlink();
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x0003D830 File Offset: 0x0003BA30
		public override void OnButtonClick()
		{
			base.OnButtonClick();
			this.RefreshPanel(false);
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x0023DB4C File Offset: 0x0023BD4C
		public override void RefreshPanel(bool isHyperLink = false)
		{
			if (this.NowPanelID != this.TypeSSV.NowSelectID)
			{
				this.NowPanelID = this.TypeSSV.NowSelectID;
				foreach (InfoPanelBase infoPanelBase in this.PanelDict.Values)
				{
					infoPanelBase.gameObject.SetActive(false);
				}
				if (this.nowPanelID >= 0)
				{
					this.PanelDict[this.NowPanelID].gameObject.SetActive(true);
					TuJianManager.Inst.NeedRefreshDataList = true;
				}
			}
			if (this.nowPanelID >= 0)
			{
				this.PanelDict[this.NowPanelID].NeedRefresh = true;
			}
		}

		// Token: 0x04005597 RID: 21911
		[HideInInspector]
		public static TuJianItemTab Inst;

		// Token: 0x04005598 RID: 21912
		private MiShuInfoPanel MiShuInfoPanel;

		// Token: 0x04005599 RID: 21913
		private CaoYaoInfoPanel CaoYaoInfoPanel;

		// Token: 0x0400559A RID: 21914
		private GongFaInfoPanel GongFaInfoPanel;

		// Token: 0x0400559B RID: 21915
		private DanYaoInfoPanel DanYaoInfoPanel;

		// Token: 0x0400559C RID: 21916
		private YaoShouInfoPanel YaoShouInfoPanel;

		// Token: 0x0400559D RID: 21917
		private ShenTongInfoPanel ShenTongInfoPanel;

		// Token: 0x0400559E RID: 21918
		private KuangShiInfoPanel KuangShiInfoPanel;

		// Token: 0x0400559F RID: 21919
		private YaoShouCaiLiaoInfoPanel YaoShouCaiLiaoInfoPanel;

		// Token: 0x040055A0 RID: 21920
		private GameObject ZheZhaoDown;

		// Token: 0x040055A1 RID: 21921
		[HideInInspector]
		public Dropdown PinJieDropdown;

		// Token: 0x040055A2 RID: 21922
		[HideInInspector]
		public Dropdown ShuXingDropdown;

		// Token: 0x040055A3 RID: 21923
		private Dictionary<int, InfoPanelBase> PanelDict = new Dictionary<int, InfoPanelBase>();

		// Token: 0x040055A4 RID: 21924
		[HideInInspector]
		public SuperScrollView TypeSSV;

		// Token: 0x040055A5 RID: 21925
		[HideInInspector]
		public SuperScrollView FilterSSV;

		// Token: 0x040055A6 RID: 21926
		private int nowPanelID = -1;

		// Token: 0x040055A7 RID: 21927
		private int nowPinJieDropdownType = -1;

		// Token: 0x040055A8 RID: 21928
		private int nowShuXingDropdownTyp = -1;

		// Token: 0x040055A9 RID: 21929
		private Dictionary<int, List<string>> options = new Dictionary<int, List<string>>
		{
			{
				1,
				new List<string>
				{
					"全品阶",
					"一品",
					"二品",
					"三品",
					"四品",
					"五品",
					"六品"
				}
			},
			{
				2,
				new List<string>
				{
					"全品阶",
					"人阶",
					"地阶",
					"天阶"
				}
			},
			{
				3,
				new List<string>
				{
					"全境界",
					"炼气境",
					"筑基境",
					"金丹境",
					"元婴境",
					"化神境"
				}
			},
			{
				11,
				new List<string>
				{
					"全属性",
					"金",
					"木",
					"水",
					"火",
					"土",
					"剑",
					"混元",
					"神念"
				}
			},
			{
				12,
				new List<string>
				{
					"全属性",
					"金",
					"木",
					"水",
					"火",
					"土",
					"气",
					"神",
					"剑",
					"阵"
				}
			},
			{
				13,
				new List<string>
				{
					"全属性",
					"金",
					"木",
					"水",
					"火",
					"土",
					"气",
					"遁",
					"神",
					"剑",
					"体"
				}
			}
		};
	}
}
