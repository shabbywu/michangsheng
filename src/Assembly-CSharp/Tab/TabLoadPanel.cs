using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000700 RID: 1792
	public class TabLoadPanel : ISysPanelBase
	{
		// Token: 0x06003980 RID: 14720 RVA: 0x00189B14 File Offset: 0x00187D14
		public TabLoadPanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
			this.SaveList = new List<TabDataBase>();
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00189B35 File Offset: 0x00187D35
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this.UpdateUI();
			this._go.SetActive(true);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00189B60 File Offset: 0x00187D60
		private void Init()
		{
			Transform transform = base.Get("LoadList/ViewPort/Content", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				this.SaveList.Add(new TabDataBase(transform.GetChild(i).gameObject, 1));
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00189BB0 File Offset: 0x00187DB0
		private void UpdateUI()
		{
			foreach (TabDataBase tabDataBase in this.SaveList)
			{
				tabDataBase.UpdateDate();
			}
		}

		// Token: 0x0400319E RID: 12702
		private bool _isInit;

		// Token: 0x0400319F RID: 12703
		public List<TabDataBase> SaveList;
	}
}
