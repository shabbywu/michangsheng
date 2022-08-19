using System;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;

namespace script.NewLianDan.PutDanLu
{
	// Token: 0x020009FA RID: 2554
	public class PutDanLuPanel : BasePanel
	{
		// Token: 0x060046BB RID: 18107 RVA: 0x001DE69D File Offset: 0x001DC89D
		public PutDanLuPanel(GameObject go)
		{
			this._go = go;
			base.Get<FpBtn>("放入按钮").mouseUpEvent.AddListener(new UnityAction(LianDanUIMag.Instance.DanLuBag.Open));
		}
	}
}
