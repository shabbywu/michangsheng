using System;
using System.Collections.Generic;
using script.MenPaiTask.ZhangLao.UI.UI;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI.Ctr
{
	// Token: 0x02000A11 RID: 2577
	public class ElderTaskCtr
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x001E33D1 File Offset: 0x001E15D1
		public ElderTaskUI UI
		{
			get
			{
				return ElderTaskUIMag.Inst.ElderTaskUI;
			}
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x001E33E0 File Offset: 0x001E15E0
		public void CreateTaskList()
		{
			ElderTaskMag elderTaskMag = Tools.instance.getPlayer().ElderTaskMag;
			this.Clear();
			foreach (ElderTask data in elderTaskMag.GetCompleteTaskList())
			{
				GameObject gameObject = this.UI.已完成.Inst(this.UI.任务列表列表);
				BaseElderTask item = BaseElderTask.Create<CompleteElderTask>(data, gameObject);
				this.TaskList.Add(item);
			}
			foreach (ElderTask data2 in elderTaskMag.GetWaitAcceptTaskList())
			{
				GameObject gameObject2 = this.UI.待接取.Inst(this.UI.任务列表列表);
				BaseElderTask item2 = BaseElderTask.Create<WaitAcceptElderTask>(data2, gameObject2);
				this.TaskList.Add(item2);
			}
			foreach (ElderTask data3 in elderTaskMag.GetExecutingTaskList())
			{
				GameObject gameObject3 = this.UI.执行中.Inst(this.UI.任务列表列表);
				BaseElderTask item3 = BaseElderTask.Create<ExecutingElderTask>(data3, gameObject3);
				this.TaskList.Add(item3);
			}
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x001E354C File Offset: 0x001E174C
		private void Clear()
		{
			for (int i = this.TaskList.Count - 1; i >= 0; i--)
			{
				this.TaskList[i].DestroySelf();
			}
		}

		// Token: 0x0400487C RID: 18556
		public List<BaseElderTask> TaskList = new List<BaseElderTask>();
	}
}
