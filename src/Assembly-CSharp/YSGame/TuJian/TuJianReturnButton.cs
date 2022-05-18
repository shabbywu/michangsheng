using System;
using UnityEngine;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DED RID: 3565
	public class TuJianReturnButton : MonoBehaviour
	{
		// Token: 0x06005604 RID: 22020 RVA: 0x0003D898 File Offset: 0x0003BA98
		private void Start()
		{
			this._HyText = base.gameObject.GetComponent<SymbolText>();
			this.isShow = this._HyText.enabled;
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x0023DEAC File Offset: 0x0023C0AC
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

		// Token: 0x040055AB RID: 21931
		private SymbolText _HyText;

		// Token: 0x040055AC RID: 21932
		private bool isShow;
	}
}
