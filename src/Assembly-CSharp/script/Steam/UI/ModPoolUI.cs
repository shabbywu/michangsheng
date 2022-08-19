using System;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using UnityEngine;

namespace script.Steam.UI
{
	// Token: 0x020009E3 RID: 2531
	public class ModPoolUI : BasePanel
	{
		// Token: 0x0600461E RID: 17950 RVA: 0x001DAA45 File Offset: 0x001D8C45
		public ModPoolUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Ctr = new ModUIPoolCtr();
			this.ModPrefab = base.Get("Scroll View/Viewport/Content/Mod", true);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x001DAA71 File Offset: 0x001D8C71
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			base.Show();
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x00004095 File Offset: 0x00002295
		private void Init()
		{
		}

		// Token: 0x040047A9 RID: 18345
		private bool isInit;

		// Token: 0x040047AA RID: 18346
		public GameObject ModPrefab;

		// Token: 0x040047AB RID: 18347
		public ModUIPoolCtr Ctr;

		// Token: 0x040047AC RID: 18348
		public Transform Load;
	}
}
