using System;
using System.Collections.Generic;

namespace script.Steam.Utils
{
	// Token: 0x020009DF RID: 2527
	public class UIToggleGroup
	{
		// Token: 0x06004606 RID: 17926 RVA: 0x001DA304 File Offset: 0x001D8504
		public void AddToggle(UIToggleA toggle)
		{
			this.toggles.Add(toggle);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x001DA312 File Offset: 0x001D8512
		public void RemoveToggle(UIToggleA toggle)
		{
			this.toggles.Remove(toggle);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x001DA324 File Offset: 0x001D8524
		public void Select(UIToggleA toggle)
		{
			foreach (UIToggleA uitoggleA in this.toggles)
			{
				if (uitoggleA != toggle)
				{
					uitoggleA.CanCelSelect();
				}
			}
			toggle.Select();
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x001DA380 File Offset: 0x001D8580
		public void SelectDefault()
		{
			this.Select(this.toggles[0]);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x001DA394 File Offset: 0x001D8594
		public void SetAllCanClick(bool flag)
		{
			foreach (UIToggleA uitoggleA in this.toggles)
			{
				uitoggleA.SetCanClick(flag);
			}
		}

		// Token: 0x04004798 RID: 18328
		private List<UIToggleA> toggles = new List<UIToggleA>();
	}
}
