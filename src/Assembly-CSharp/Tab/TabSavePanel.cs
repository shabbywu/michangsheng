using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A4C RID: 2636
	public class TabSavePanel : ISysPanelBase
	{
		// Token: 0x06004402 RID: 17410 RVA: 0x00030A84 File Offset: 0x0002EC84
		public TabSavePanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
			this.SaveList = new List<TabDataBase>();
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x00030AA5 File Offset: 0x0002ECA5
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

		// Token: 0x06004404 RID: 17412 RVA: 0x001D1568 File Offset: 0x001CF768
		private void Init()
		{
			Transform transform = base.Get("SaveList/ViewPort/Content", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				this.SaveList.Add(new TabDataBase(transform.GetChild(i).gameObject, 0));
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x001D15B8 File Offset: 0x001CF7B8
		private void UpdateUI()
		{
			foreach (TabDataBase tabDataBase in this.SaveList)
			{
				tabDataBase.UpdateDate();
			}
		}

		// Token: 0x04003C18 RID: 15384
		private bool _isInit;

		// Token: 0x04003C19 RID: 15385
		public List<TabDataBase> SaveList;
	}
}
