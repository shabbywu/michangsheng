using System;

namespace Tab
{
	// Token: 0x02000A2C RID: 2604
	public abstract class ITabPanelBase : UIBase
	{
		// Token: 0x06004371 RID: 17265 RVA: 0x00030349 File Offset: 0x0002E549
		protected ITabPanelBase()
		{
			SingletonMono<TabUIMag>.Instance.PanelList.Add(this);
		}

		// Token: 0x06004372 RID: 17266
		public abstract void Show();

		// Token: 0x06004373 RID: 17267 RVA: 0x00030361 File Offset: 0x0002E561
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}

		// Token: 0x04003B78 RID: 15224
		public bool HasHp;
	}
}
