using System;
using System.Collections;

namespace Fungus
{
	// Token: 0x020013AD RID: 5037
	public class Task
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06007A01 RID: 31233 RVA: 0x000533E2 File Offset: 0x000515E2
		public bool Running
		{
			get
			{
				return this.task.Running;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06007A02 RID: 31234 RVA: 0x000533EF File Offset: 0x000515EF
		public bool Paused
		{
			get
			{
				return this.task.Paused;
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06007A03 RID: 31235 RVA: 0x002B9868 File Offset: 0x002B7A68
		// (remove) Token: 0x06007A04 RID: 31236 RVA: 0x002B98A0 File Offset: 0x002B7AA0
		public event Task.FinishedHandler Finished;

		// Token: 0x06007A05 RID: 31237 RVA: 0x000533FC File Offset: 0x000515FC
		public Task(IEnumerator c, bool autoStart = true)
		{
			this.task = TaskManager.CreateTask(c);
			this.task.Finished += this.TaskFinished;
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x00053430 File Offset: 0x00051630
		public void Start()
		{
			this.task.Start();
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x0005343D File Offset: 0x0005163D
		public void Stop()
		{
			this.task.Stop();
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x0005344A File Offset: 0x0005164A
		public void Pause()
		{
			this.task.Pause();
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x00053457 File Offset: 0x00051657
		public void Unpause()
		{
			this.task.Unpause();
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x002B98D8 File Offset: 0x002B7AD8
		private void TaskFinished(bool manual)
		{
			Task.FinishedHandler finished = this.Finished;
			if (finished != null)
			{
				finished(manual);
			}
		}

		// Token: 0x04006971 RID: 26993
		private TaskManager.TaskState task;

		// Token: 0x020013AE RID: 5038
		// (Invoke) Token: 0x06007A0C RID: 31244
		public delegate void FinishedHandler(bool manual);
	}
}
