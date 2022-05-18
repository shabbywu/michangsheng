using System;

namespace Tab
{
	// Token: 0x02000A47 RID: 2631
	public abstract class ISysPanelBase : UIBase
	{
		// Token: 0x060043EC RID: 17388 RVA: 0x00030946 File Offset: 0x0002EB46
		protected ISysPanelBase()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.SystemPanel.PanelList.Add(this);
			}
		}

		// Token: 0x060043ED RID: 17389
		public abstract void Show();

		// Token: 0x060043EE RID: 17390 RVA: 0x00030361 File Offset: 0x0002E561
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}
	}
}
