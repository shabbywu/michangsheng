using System;
using System.Collections.Generic;
using script.Steam.UI;
using script.Steam.UI.Base;
using UnityEngine;

namespace script.Steam.Ctr
{
	// Token: 0x020009E9 RID: 2537
	public class ModUIPoolCtr
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x001DC09F File Offset: 0x001DA29F
		public ModPoolUI UI
		{
			get
			{
				return WorkShopMag.Inst.ModPoolUI;
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x001DC0AB File Offset: 0x001DA2AB
		public ModUIPoolCtr()
		{
			this.ModUIPools = new List<ModUI>(this.Max);
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x001DC0CC File Offset: 0x001DA2CC
		public ModUI GetModUI(int type = 0)
		{
			foreach (ModUI modUI in this.ModUIPools)
			{
				if (!modUI.IsUsed)
				{
					modUI.SetType(type);
					return modUI;
				}
			}
			if (this.CurNum == this.Max)
			{
				Debug.LogError("超出最大限制");
				return null;
			}
			this.CurNum++;
			ModUI modUI2 = new ModUI(this.UI.ModPrefab.Inst(this.UI.ModPrefab.transform.parent));
			modUI2.SetType(type);
			this.ModUIPools.Add(modUI2);
			return modUI2;
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x001DC198 File Offset: 0x001DA398
		public void BackMod(ModUI modUI)
		{
			modUI.UnBindingInfo();
		}

		// Token: 0x040047DE RID: 18398
		private int CurNum;

		// Token: 0x040047DF RID: 18399
		private int Max = 50;

		// Token: 0x040047E0 RID: 18400
		private readonly List<ModUI> ModUIPools;
	}
}
