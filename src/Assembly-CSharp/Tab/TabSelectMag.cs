using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000706 RID: 1798
	[Serializable]
	public class TabSelectMag : UIBase
	{
		// Token: 0x060039AE RID: 14766 RVA: 0x0018AE60 File Offset: 0x00189060
		public TabSelectMag(GameObject go)
		{
			this._list = new List<TabSelectCell>();
			this._go = go;
			this.Init();
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0018AE80 File Offset: 0x00189080
		public void SetDeafultSelect(int index = 0)
		{
			this._list[index].Click();
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0018AE94 File Offset: 0x00189094
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

		// Token: 0x060039B1 RID: 14769 RVA: 0x0018AF28 File Offset: 0x00189128
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

		// Token: 0x040031C2 RID: 12738
		private List<TabSelectCell> _list;
	}
}
