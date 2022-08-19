using System;

namespace Tab
{
	// Token: 0x020006EB RID: 1771
	public abstract class ITabPanelBase : UIBase
	{
		// Token: 0x06003906 RID: 14598 RVA: 0x0018563B File Offset: 0x0018383B
		protected ITabPanelBase()
		{
			SingletonMono<TabUIMag>.Instance.PanelList.Add(this);
		}

		// Token: 0x06003907 RID: 14599
		public abstract void Show();

		// Token: 0x06003908 RID: 14600 RVA: 0x00185653 File Offset: 0x00183853
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}

		// Token: 0x04003113 RID: 12563
		public bool HasHp;
	}
}
