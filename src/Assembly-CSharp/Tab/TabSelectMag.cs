using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A53 RID: 2643
	[Serializable]
	public class TabSelectMag : UIBase
	{
		// Token: 0x06004432 RID: 17458 RVA: 0x00030D39 File Offset: 0x0002EF39
		public TabSelectMag(GameObject go)
		{
			this._list = new List<TabSelectCell>();
			this._go = go;
			this.Init();
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00030D59 File Offset: 0x0002EF59
		public void SetDeafultSelect(int index = 0)
		{
			this._list[index].Click();
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x001D25F8 File Offset: 0x001D07F8
		private void Init()
		{
			for (int i = 0; i < this._go.transform.childCount; i++)
			{
				Transform child = this._go.transform.GetChild(i);
				if (!Tools.instance.IsInDF || !(child.name == "声望"))
				{
					if (child.name == "Panel")
					{
						break;
					}
					this._list.Add(new TabSelectCell(child.gameObject, SingletonMono<TabUIMag>.Instance.PanelList[i]));
				}
			}
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x001D268C File Offset: 0x001D088C
		public void UpdateAll(TabSelectCell curSelectCell)
		{
			foreach (TabSelectCell tabSelectCell in this._list)
			{
				if (curSelectCell == tabSelectCell)
				{
					tabSelectCell.SetIsSelect(true);
				}
				else
				{
					tabSelectCell.SetIsSelect(false);
				}
			}
		}

		// Token: 0x04003C3E RID: 15422
		private List<TabSelectCell> _list;
	}
}
