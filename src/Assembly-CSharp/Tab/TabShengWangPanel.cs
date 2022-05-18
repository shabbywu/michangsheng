using System;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A41 RID: 2625
	[Serializable]
	public class TabShengWangPanel : ITabPanelBase
	{
		// Token: 0x060043D7 RID: 17367 RVA: 0x00030824 File Offset: 0x0002EA24
		public TabShengWangPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000042DD File Offset: 0x000024DD
		private void Init()
		{
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0003083A File Offset: 0x0002EA3A
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this._go.SetActive(true);
		}

		// Token: 0x04003BD7 RID: 15319
		private bool _isInit;
	}
}
