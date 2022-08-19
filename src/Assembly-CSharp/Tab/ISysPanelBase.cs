using System;

namespace Tab
{
	// Token: 0x020006FC RID: 1788
	public abstract class ISysPanelBase : UIBase
	{
		// Token: 0x0600396F RID: 14703 RVA: 0x00189537 File Offset: 0x00187737
		protected ISysPanelBase()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.SystemPanel.PanelList.Add(this);
			}
		}

		// Token: 0x06003970 RID: 14704
		public abstract void Show();

		// Token: 0x06003971 RID: 14705 RVA: 0x00185653 File Offset: 0x00183853
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}
	}
}
