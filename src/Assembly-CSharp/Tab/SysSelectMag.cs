using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x020006FD RID: 1789
	public class SysSelectMag : UIBase
	{
		// Token: 0x06003972 RID: 14706 RVA: 0x00189561 File Offset: 0x00187761
		public SysSelectMag(GameObject go)
		{
			this._list = new List<SysSelectCell>();
			this._go = go;
			this.Init();
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00189581 File Offset: 0x00187781
		public void SetDeafultSelect(int index = 0)
		{
			this._list[index].Click();
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00189594 File Offset: 0x00187794
		private void Init()
		{
			for (int i = 0; i < this._go.transform.childCount; i++)
			{
				Transform child = this._go.transform.GetChild(i);
				if (Tools.instance.IsInDF && (child.name == "保存" || child.name == "读取"))
				{
					child.gameObject.SetActive(false);
				}
				else
				{
					if (child.name == "返回标题")
					{
						break;
					}
					this._list.Add(new SysSelectCell(child.gameObject, SingletonMono<TabUIMag>.Instance.SystemPanel.PanelList[i]));
				}
			}
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x00189654 File Offset: 0x00187854
		public void UpdateAll(SysSelectCell curSelectCell)
		{
			foreach (SysSelectCell sysSelectCell in this._list)
			{
				if (curSelectCell == sysSelectCell)
				{
					sysSelectCell.SetIsSelect(true);
				}
				else
				{
					sysSelectCell.SetIsSelect(false);
				}
			}
		}

		// Token: 0x04003190 RID: 12688
		private List<SysSelectCell> _list;
	}
}
