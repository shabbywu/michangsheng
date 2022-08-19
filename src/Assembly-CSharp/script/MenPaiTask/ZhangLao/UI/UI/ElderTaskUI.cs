using System;
using script.MenPaiTask.ZhangLao.UI.Ctr;
using script.NewLianDan.Base;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI.UI
{
	// Token: 0x02000A0F RID: 2575
	public class ElderTaskUI : BasePanel
	{
		// Token: 0x0600474C RID: 18252 RVA: 0x001E2DFD File Offset: 0x001E0FFD
		public ElderTaskUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Ctr = new ElderTaskCtr();
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x001E2E18 File Offset: 0x001E1018
		private void Init()
		{
			this.已完成 = base.Get("任务列表/Viewport/Content/已完成", true);
			this.执行中 = base.Get("任务列表/Viewport/Content/执行中", true);
			this.待接取 = base.Get("任务列表/Viewport/Content/待接取", true);
			this.任务列表列表 = base.Get("任务列表/Viewport/Content", true).transform;
			this.Ctr.CreateTaskList();
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x001E2E7D File Offset: 0x001E107D
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			base.Show();
		}

		// Token: 0x0400486F RID: 18543
		public ElderTaskCtr Ctr;

		// Token: 0x04004870 RID: 18544
		public GameObject 已完成;

		// Token: 0x04004871 RID: 18545
		public GameObject 执行中;

		// Token: 0x04004872 RID: 18546
		public GameObject 待接取;

		// Token: 0x04004873 RID: 18547
		public Transform 任务列表列表;

		// Token: 0x04004874 RID: 18548
		private bool isInit;
	}
}
