using System;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;

namespace script.NewLianDan.PutDanLu
{
	// Token: 0x02000AC4 RID: 2756
	public class PutDanLuPanel : BasePanel
	{
		// Token: 0x06004661 RID: 18017 RVA: 0x0003241D File Offset: 0x0003061D
		public PutDanLuPanel(GameObject go)
		{
			this._go = go;
			base.Get<FpBtn>("放入按钮").mouseUpEvent.AddListener(new UnityAction(LianDanUIMag.Instance.DanLuBag.Open));
		}
	}
}
