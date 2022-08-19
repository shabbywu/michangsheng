using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AA6 RID: 2726
	public class InfoPanelBase : MonoBehaviour, ITuJianHyperlink
	{
		// Token: 0x06004C6E RID: 19566 RVA: 0x0020A7F8 File Offset: 0x002089F8
		public virtual void Update()
		{
			if (this.NeedRefresh)
			{
				this.NeedRefresh = false;
				this.RefreshPanelData();
			}
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0020A80F File Offset: 0x00208A0F
		public virtual void OnHyperlink(int[] args)
		{
			this.isOnHyperlink = true;
			this.hylinkArgs = args;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void RefreshPanelData()
		{
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void RefreshDataList()
		{
		}

		// Token: 0x04004B93 RID: 19347
		[HideInInspector]
		public bool NeedRefresh;

		// Token: 0x04004B94 RID: 19348
		[HideInInspector]
		public List<Dictionary<int, string>> DataList = new List<Dictionary<int, string>>();

		// Token: 0x04004B95 RID: 19349
		protected int[] hylinkArgs;

		// Token: 0x04004B96 RID: 19350
		protected bool isOnHyperlink;
	}
}
