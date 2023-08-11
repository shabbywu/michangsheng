using UnityEngine;

namespace YSGame.TuJian;

public class TuJianRuleTab : TuJianTab
{
	[HideInInspector]
	public static TuJianRuleTab Inst;

	private RuleInfoPanel RuleInfoPanel;

	public SuperScrollView TypeSSV;

	public SuperScrollView FilterSSV;

	private int NowType = -1;

	public override void Awake()
	{
		Inst = this;
		TabType = TuJianTabType.Rule;
		TypeSSV = ((Component)((Component)this).transform.Find("Root/TuJianTypeSV")).GetComponent<SuperScrollView>();
		FilterSSV = ((Component)((Component)this).transform.Find("Root/TuJianItemsSV")).GetComponent<SuperScrollView>();
		RuleInfoPanel = ((Component)((Component)this).transform).GetComponentInChildren<RuleInfoPanel>(true);
		TypeSSV.DataList = TuJianDB.RuleTuJianTypeNameData;
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

	public override void OnHyperlink(int[] args)
	{
		base.OnHyperlink(args);
		RuleInfoPanel.OnHyperlink(args);
		TypeSSV.NowSelectID = args[1];
		RefreshPanel(isHyperLink: true);
		FilterSSV.NowSelectID = args[2];
	}

	public override void OnButtonClick()
	{
		base.OnButtonClick();
		RefreshPanel();
	}

	public override void RefreshPanel(bool isHyperLink = false)
	{
		if (TuJianManager.Inst.Searcher.SearchCount > 0)
		{
			string text = RuleInfoPanel.FindSearch();
			if (!string.IsNullOrEmpty(text))
			{
				TuJianManager.Inst.Searcher.ClearSearchStrAndNoSearch();
				TuJianManager.Inst.OnHyperlink(text);
			}
		}
		if (NowType != TypeSSV.NowSelectID)
		{
			NowType = TypeSSV.NowSelectID;
			FilterSSV.DataList = TuJianDB.RuleTuJianFilterData[NowType];
			if (isHyperLink)
			{
				FilterSSV.NeedResetToTop = false;
			}
		}
		RuleInfoPanel.NeedRefresh = true;
	}
}
