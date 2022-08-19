using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000AAE RID: 2734
	public class TuJianItemTab : TuJianTab
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x0020C883 File Offset: 0x0020AA83
		// (set) Token: 0x06004CA7 RID: 19623 RVA: 0x0020C88B File Offset: 0x0020AA8B
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

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0020C8B8 File Offset: 0x0020AAB8
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

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0020CB7F File Offset: 0x0020AD7F
		public override void Show()
		{
			base.Show();
			this.RefreshPanel(false);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0020CB8E File Offset: 0x0020AD8E
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0020CB98 File Offset: 0x0020AD98
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

		// Token: 0x06004CAC RID: 19628 RVA: 0x0020CC88 File Offset: 0x0020AE88
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
			TuJianManager.Inst.ClearSearch();
			this.TypeSSV.NowSelectID = args[1];
			this.PanelDict[args[1]].OnHyperlink(args);
			this.RefreshPanel(true);
			this.FilterSSV.ResetToTopByHyperlink();
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0020CCDA File Offset: 0x0020AEDA
		public override void OnButtonClick()
		{
			base.OnButtonClick();
			this.RefreshPanel(false);
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0020CCEC File Offset: 0x0020AEEC
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

		// Token: 0x04004BB9 RID: 19385
		[HideInInspector]
		public static TuJianItemTab Inst;

		// Token: 0x04004BBA RID: 19386
		private MiShuInfoPanel MiShuInfoPanel;

		// Token: 0x04004BBB RID: 19387
		private CaoYaoInfoPanel CaoYaoInfoPanel;

		// Token: 0x04004BBC RID: 19388
		private GongFaInfoPanel GongFaInfoPanel;

		// Token: 0x04004BBD RID: 19389
		private DanYaoInfoPanel DanYaoInfoPanel;

		// Token: 0x04004BBE RID: 19390
		private YaoShouInfoPanel YaoShouInfoPanel;

		// Token: 0x04004BBF RID: 19391
		private ShenTongInfoPanel ShenTongInfoPanel;

		// Token: 0x04004BC0 RID: 19392
		private KuangShiInfoPanel KuangShiInfoPanel;

		// Token: 0x04004BC1 RID: 19393
		private YaoShouCaiLiaoInfoPanel YaoShouCaiLiaoInfoPanel;

		// Token: 0x04004BC2 RID: 19394
		private GameObject ZheZhaoDown;

		// Token: 0x04004BC3 RID: 19395
		[HideInInspector]
		public Dropdown PinJieDropdown;

		// Token: 0x04004BC4 RID: 19396
		[HideInInspector]
		public Dropdown ShuXingDropdown;

		// Token: 0x04004BC5 RID: 19397
		private Dictionary<int, InfoPanelBase> PanelDict = new Dictionary<int, InfoPanelBase>();

		// Token: 0x04004BC6 RID: 19398
		[HideInInspector]
		public SuperScrollView TypeSSV;

		// Token: 0x04004BC7 RID: 19399
		[HideInInspector]
		public SuperScrollView FilterSSV;

		// Token: 0x04004BC8 RID: 19400
		private int nowPanelID = -1;

		// Token: 0x04004BC9 RID: 19401
		private int nowPinJieDropdownType = -1;

		// Token: 0x04004BCA RID: 19402
		private int nowShuXingDropdownTyp = -1;

		// Token: 0x04004BCB RID: 19403
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
