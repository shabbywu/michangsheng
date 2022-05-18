using System;

namespace script.NewLianDan.Base
{
	// Token: 0x02000AD3 RID: 2771
	public class BasePanel : UIBase
	{
		// Token: 0x060046B3 RID: 18099 RVA: 0x000326FF File Offset: 0x000308FF
		public virtual void Show()
		{
			this._go.SetActive(true);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00030361 File Offset: 0x0002E561
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}
	}
}
