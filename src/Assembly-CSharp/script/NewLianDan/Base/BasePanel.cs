using System;

namespace script.NewLianDan.Base
{
	// Token: 0x02000A04 RID: 2564
	public class BasePanel : UIBase
	{
		// Token: 0x06004703 RID: 18179 RVA: 0x001E1E51 File Offset: 0x001E0051
		public virtual void Show()
		{
			this._go.SetActive(true);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00185653 File Offset: 0x00183853
		public virtual void Hide()
		{
			this._go.SetActive(false);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x001E1E5F File Offset: 0x001E005F
		public bool IsActive()
		{
			return this._go.activeSelf;
		}
	}
}
