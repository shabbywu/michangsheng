using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A4B RID: 2635
	public class TabLoadPanel : ISysPanelBase
	{
		// Token: 0x060043FE RID: 17406 RVA: 0x00030A3A File Offset: 0x0002EC3A
		public TabLoadPanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
			this.SaveList = new List<TabDataBase>();
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00030A5B File Offset: 0x0002EC5B
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

		// Token: 0x06004400 RID: 17408 RVA: 0x001D14C8 File Offset: 0x001CF6C8
		private void Init()
		{
			Transform transform = base.Get("LoadList/ViewPort/Content", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				this.SaveList.Add(new TabDataBase(transform.GetChild(i).gameObject, 1));
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x001D1518 File Offset: 0x001CF718
		private void UpdateUI()
		{
			foreach (TabDataBase tabDataBase in this.SaveList)
			{
				tabDataBase.UpdateDate();
			}
		}

		// Token: 0x04003C16 RID: 15382
		private bool _isInit;

		// Token: 0x04003C17 RID: 15383
		public List<TabDataBase> SaveList;
	}
}
