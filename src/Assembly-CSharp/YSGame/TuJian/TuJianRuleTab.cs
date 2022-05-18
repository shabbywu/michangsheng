using System;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DEE RID: 3566
	public class TuJianRuleTab : TuJianTab
	{
		// Token: 0x06005607 RID: 22023 RVA: 0x0023DEFC File Offset: 0x0023C0FC
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

		// Token: 0x06005608 RID: 22024 RVA: 0x0003D819 File Offset: 0x0003BA19
		public override void Show()
		{
			base.Show();
			this.RefreshPanel(false);
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x0003D828 File Offset: 0x0003BA28
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x0003D8BC File Offset: 0x0003BABC
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
			this.RuleInfoPanel.OnHyperlink(args);
			this.TypeSSV.NowSelectID = args[1];
			this.RefreshPanel(true);
			this.FilterSSV.NowSelectID = args[2];
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x0003D830 File Offset: 0x0003BA30
		public override void OnButtonClick()
		{
			base.OnButtonClick();
			this.RefreshPanel(false);
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x0023DF74 File Offset: 0x0023C174
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

		// Token: 0x040055AD RID: 21933
		[HideInInspector]
		public static TuJianRuleTab Inst;

		// Token: 0x040055AE RID: 21934
		private RuleInfoPanel RuleInfoPanel;

		// Token: 0x040055AF RID: 21935
		public SuperScrollView TypeSSV;

		// Token: 0x040055B0 RID: 21936
		public SuperScrollView FilterSSV;

		// Token: 0x040055B1 RID: 21937
		private int NowType = -1;
	}
}
