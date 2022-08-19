using System;
using UnityEngine;

namespace Tab
{
	// Token: 0x020006F9 RID: 1785
	[Serializable]
	public class TabShengWangPanel : ITabPanelBase
	{
		// Token: 0x0600395E RID: 14686 RVA: 0x00188464 File Offset: 0x00186664
		public TabShengWangPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x00004095 File Offset: 0x00002295
		private void Init()
		{
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x0018847A File Offset: 0x0018667A
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this._go.SetActive(true);
		}

		// Token: 0x04003164 RID: 12644
		private bool _isInit;
	}
}
