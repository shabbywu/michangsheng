using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A48 RID: 2632
	public class SysSelectMag : UIBase
	{
		// Token: 0x060043EF RID: 17391 RVA: 0x00030970 File Offset: 0x0002EB70
		public SysSelectMag(GameObject go)
		{
			this._list = new List<SysSelectCell>();
			this._go = go;
			this.Init();
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00030990 File Offset: 0x0002EB90
		public void SetDeafultSelect(int index = 0)
		{
			this._list[index].Click();
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x001D0D04 File Offset: 0x001CEF04
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

		// Token: 0x060043F2 RID: 17394 RVA: 0x001D0DC4 File Offset: 0x001CEFC4
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

		// Token: 0x04003C08 RID: 15368
		private List<SysSelectCell> _list;
	}
}
