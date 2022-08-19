using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000701 RID: 1793
	public class TabSavePanel : ISysPanelBase
	{
		// Token: 0x06003984 RID: 14724 RVA: 0x00189C00 File Offset: 0x00187E00
		public TabSavePanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
			this.SaveList = new List<TabDataBase>();
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00189C21 File Offset: 0x00187E21
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

		// Token: 0x06003986 RID: 14726 RVA: 0x00189C4C File Offset: 0x00187E4C
		private void Init()
		{
			Transform transform = base.Get("SaveList/ViewPort/Content", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				this.SaveList.Add(new TabDataBase(transform.GetChild(i).gameObject, 0));
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00189C9C File Offset: 0x00187E9C
		private void UpdateUI()
		{
			foreach (TabDataBase tabDataBase in this.SaveList)
			{
				tabDataBase.UpdateDate();
			}
		}

		// Token: 0x040031A0 RID: 12704
		private bool _isInit;

		// Token: 0x040031A1 RID: 12705
		public List<TabDataBase> SaveList;
	}
}
