using System;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AB1 RID: 2737
	public class TuJianRuleTab : TuJianTab
	{
		// Token: 0x06004CBA RID: 19642 RVA: 0x0020D118 File Offset: 0x0020B318
		public override void Awake()
		{
			TuJianRuleTab.Inst = this;
			this.TabType = TuJianTabType.Rule;
			this.TypeSSV = base.transform.Find("Root/TuJianTypeSV").GetComponent<SuperScrollView>();
			this.FilterSSV = base.transform.Find("Root/TuJianItemsSV").GetComponent<SuperScrollView>();
			this.RuleInfoPanel = base.transform.GetComponentInChildren<RuleInfoPanel>(true);
			this.TypeSSV.DataList = TuJianDB.RuleTuJianTypeNameData;
			base.Awake();
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0020CB7F File Offset: 0x0020AD7F
		public override void Show()
		{
			base.Show();
			this.RefreshPanel(false);
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0020CB8E File Offset: 0x0020AD8E
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0020D190 File Offset: 0x0020B390
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
			this.RuleInfoPanel.OnHyperlink(args);
			this.TypeSSV.NowSelectID = args[1];
			this.RefreshPanel(true);
			this.FilterSSV.NowSelectID = args[2];
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0020CCDA File Offset: 0x0020AEDA
		public override void OnButtonClick()
		{
			base.OnButtonClick();
			this.RefreshPanel(false);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0020D1C8 File Offset: 0x0020B3C8
		public override void RefreshPanel(bool isHyperLink = false)
		{
			if (TuJianManager.Inst.Searcher.SearchCount > 0)
			{
				string text = this.RuleInfoPanel.FindSearch();
				if (!string.IsNullOrEmpty(text))
				{
					TuJianManager.Inst.Searcher.ClearSearchStrAndNoSearch();
					TuJianManager.Inst.OnHyperlink(text);
				}
			}
			if (this.NowType != this.TypeSSV.NowSelectID)
			{
				this.NowType = this.TypeSSV.NowSelectID;
				this.FilterSSV.DataList = TuJianDB.RuleTuJianFilterData[this.NowType];
				if (isHyperLink)
				{
					this.FilterSSV.NeedResetToTop = false;
				}
			}
			this.RuleInfoPanel.NeedRefresh = true;
		}

		// Token: 0x04004BCF RID: 19407
		[HideInInspector]
		public static TuJianRuleTab Inst;

		// Token: 0x04004BD0 RID: 19408
		private RuleInfoPanel RuleInfoPanel;

		// Token: 0x04004BD1 RID: 19409
		public SuperScrollView TypeSSV;

		// Token: 0x04004BD2 RID: 19410
		public SuperScrollView FilterSSV;

		// Token: 0x04004BD3 RID: 19411
		private int NowType = -1;
	}
}
