using System;
using UnityEngine;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000AB0 RID: 2736
	public class TuJianReturnButton : MonoBehaviour
	{
		// Token: 0x06004CB7 RID: 19639 RVA: 0x0020D0A3 File Offset: 0x0020B2A3
		private void Start()
		{
			this._HyText = base.gameObject.GetComponent<SymbolText>();
			this.isShow = this._HyText.enabled;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0020D0C8 File Offset: 0x0020B2C8
		private void Update()
		{
			if (TuJianManager.Inst.CanReturn())
			{
				if (!this.isShow)
				{
					this._HyText.enabled = true;
					this.isShow = true;
					return;
				}
			}
			else if (this.isShow)
			{
				this._HyText.enabled = false;
				this.isShow = false;
			}
		}

		// Token: 0x04004BCD RID: 19405
		private SymbolText _HyText;

		// Token: 0x04004BCE RID: 19406
		private bool isShow;
	}
}
