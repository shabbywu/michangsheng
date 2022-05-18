using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DE3 RID: 3555
	public class InfoPanelBase : MonoBehaviour, ITuJianHyperlink
	{
		// Token: 0x060055BB RID: 21947 RVA: 0x0003D548 File Offset: 0x0003B748
		public virtual void Update()
		{
			if (this.NeedRefresh)
			{
				this.NeedRefresh = false;
				this.RefreshPanelData();
			}
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0003D55F File Offset: 0x0003B75F
		public virtual void OnHyperlink(int[] args)
		{
			this.isOnHyperlink = true;
			this.hylinkArgs = args;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void RefreshPanelData()
		{
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void RefreshDataList()
		{
		}

		// Token: 0x04005571 RID: 21873
		[HideInInspector]
		public bool NeedRefresh;

		// Token: 0x04005572 RID: 21874
		[HideInInspector]
		public List<Dictionary<int, string>> DataList = new List<Dictionary<int, string>>();

		// Token: 0x04005573 RID: 21875
		protected int[] hylinkArgs;

		// Token: 0x04005574 RID: 21876
		protected bool isOnHyperlink;
	}
}
