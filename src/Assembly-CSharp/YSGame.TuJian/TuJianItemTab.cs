using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class TuJianItemTab : TuJianTab
{
	[HideInInspector]
	public static TuJianItemTab Inst;

	private MiShuInfoPanel MiShuInfoPanel;

	private CaoYaoInfoPanel CaoYaoInfoPanel;

	private GongFaInfoPanel GongFaInfoPanel;

	private DanYaoInfoPanel DanYaoInfoPanel;

	private YaoShouInfoPanel YaoShouInfoPanel;

	private ShenTongInfoPanel ShenTongInfoPanel;

	private KuangShiInfoPanel KuangShiInfoPanel;

	private YaoShouCaiLiaoInfoPanel YaoShouCaiLiaoInfoPanel;

	private GameObject ZheZhaoDown;

	[HideInInspector]
	public Dropdown PinJieDropdown;

	[HideInInspector]
	public Dropdown ShuXingDropdown;

	private Dictionary<int, InfoPanelBase> PanelDict = new Dictionary<int, InfoPanelBase>();

	[HideInInspector]
	public SuperScrollView TypeSSV;

	[HideInInspector]
	public SuperScrollView FilterSSV;

	private int nowPanelID = -1;

	private int nowPinJieDropdownType = -1;

	private int nowShuXingDropdownTyp = -1;

	private Dictionary<int, List<string>> options = new Dictionary<int, List<string>>
	{
		{
			1,
			new List<string> { "全品阶", "一品", "二品", "三品", "四品", "五品", "六品" }
		},
		{
			2,
			new List<string> { "全品阶", "人阶", "地阶", "天阶" }
		},
		{
			3,
			new List<string> { "全境界", "炼气境", "筑基境", "金丹境", "元婴境", "化神境" }
		},
		{
			11,
			new List<string> { "全属性", "金", "木", "水", "火", "土", "剑", "混元", "神念" }
		},
		{
			12,
			new List<string> { "全属性", "金", "木", "水", "火", "土", "气", "神", "剑", "阵" }
		},
		{
			13,
			new List<string>
			{
				"全属性", "金", "木", "水", "火", "土", "气", "遁", "神", "剑",
				"体"
			}
		}
	};

	private int NowPanelID
	{
		get
		{
			return nowPanelID;
		}
		set
		{
			nowPanelID = value;
			if (nowPanelID == 4)
			{
				ZheZhaoDown.SetActive(false);
			}
			else
			{
				ZheZhaoDown.SetActive(true);
			}
		}
	}

	public override void Awake()
	{
		Inst = this;
		TabType = TuJianTabType.Item;
		TypeSSV = ((Component)((Component)this).transform.Find("Root/TuJianTypeSV")).GetComponent<SuperScrollView>();
		FilterSSV = ((Component)((Component)this).transform.Find("Root/TuJianItemsSV")).GetComponent<SuperScrollView>();
		ZheZhaoDown = ((Component)((Component)this).transform.Find("Root/InfoRoot/ZheZhaoDown")).gameObject;
		PinJieDropdown = ((Component)((Component)this).transform.Find("Root/PinJieDropdown")).GetComponent<Dropdown>();
		ShuXingDropdown = ((Component)((Component)this).transform.Find("Root/ShuXingDropdown")).GetComponent<Dropdown>();
		MiShuInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<MiShuInfoPanel>(true);
		CaoYaoInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<CaoYaoInfoPanel>(true);
		GongFaInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<GongFaInfoPanel>(true);
		DanYaoInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<DanYaoInfoPanel>(true);
		YaoShouInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<YaoShouInfoPanel>(true);
		ShenTongInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<ShenTongInfoPanel>(true);
		KuangShiInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<KuangShiInfoPanel>(true);
		YaoShouCaiLiaoInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<YaoShouCaiLiaoInfoPanel>(true);
		PanelDict.Add(1, CaoYaoInfoPanel);
		PanelDict.Add(2, KuangShiInfoPanel);
		PanelDict.Add(3, YaoShouCaiLiaoInfoPanel);
		PanelDict.Add(4, DanYaoInfoPanel);
		PanelDict.Add(5, YaoShouInfoPanel);
		PanelDict.Add(6, ShenTongInfoPanel);
		PanelDict.Add(7, GongFaInfoPanel);
		PanelDict.Add(8, MiShuInfoPanel);
		TypeSSV.DataList = new List<Dictionary<int, string>>
		{
			new Dictionary<int, string> { { 1, "草药" } },
			new Dictionary<int, string> { { 2, "矿物" } },
			new Dictionary<int, string> { { 3, "材料" } },
			new Dictionary<int, string> { { 4, "丹药" } },
			new Dictionary<int, string> { { 5, "妖兽" } },
			new Dictionary<int, string> { { 6, "神通" } },
			new Dictionary<int, string> { { 7, "功法" } },
			new Dictionary<int, string> { { 8, "秘术" } }
		};
		((UnityEvent<int>)(object)PinJieDropdown.onValueChanged).AddListener((UnityAction<int>)delegate
		{
			TuJianManager.Inst.NeedRefreshDataList = true;
			RefreshPanel();
		});
		((UnityEvent<int>)(object)ShuXingDropdown.onValueChanged).AddListener((UnityAction<int>)delegate
		{
			TuJianManager.Inst.NeedRefreshDataList = true;
			RefreshPanel();
			FilterSSV.NeedResetToTop = true;
		});
		base.Awake();
	}

	public override void Show()
	{
		base.Show();
		RefreshPanel();
	}

	public override void Hide()
	{
		base.Hide();
	}

	public void SetDropdown(int pinJie, int shuXing)
	{
		if (nowPinJieDropdownType != pinJie)
		{
			nowPinJieDropdownType = pinJie;
			if (pinJie == 0)
			{
				((Component)PinJieDropdown).gameObject.SetActive(false);
			}
			else
			{
				((Component)PinJieDropdown).gameObject.SetActive(true);
				PinJieDropdown.ClearOptions();
				PinJieDropdown.AddOptions(options[pinJie]);
				PinJieDropdown.value = 0;
			}
			TuJianManager.Inst.NeedRefreshDataList = true;
		}
		if (nowShuXingDropdownTyp != shuXing)
		{
			nowShuXingDropdownTyp = shuXing;
			if (shuXing == 0)
			{
				((Component)ShuXingDropdown).gameObject.SetActive(false);
			}
			else
			{
				((Component)ShuXingDropdown).gameObject.SetActive(true);
				ShuXingDropdown.ClearOptions();
				ShuXingDropdown.AddOptions(options[shuXing]);
				ShuXingDropdown.value = 0;
			}
			TuJianManager.Inst.NeedRefreshDataList = true;
		}
	}

	public override void OnHyperlink(int[] args)
	{
		base.OnHyperlink(args);
		TuJianManager.Inst.ClearSearch();
		TypeSSV.NowSelectID = args[1];
		PanelDict[args[1]].OnHyperlink(args);
		RefreshPanel(isHyperLink: true);
		FilterSSV.ResetToTopByHyperlink();
	}

	public override void OnButtonClick()
	{
		base.OnButtonClick();
		RefreshPanel();
	}

	public override void RefreshPanel(bool isHyperLink = false)
	{
		if (NowPanelID != TypeSSV.NowSelectID)
		{
			NowPanelID = TypeSSV.NowSelectID;
			foreach (InfoPanelBase value in PanelDict.Values)
			{
				((Component)value).gameObject.SetActive(false);
			}
			if (nowPanelID >= 0)
			{
				((Component)PanelDict[NowPanelID]).gameObject.SetActive(true);
				TuJianManager.Inst.NeedRefreshDataList = true;
			}
		}
		if (nowPanelID >= 0)
		{
			PanelDict[NowPanelID].NeedRefresh = true;
		}
	}
}
