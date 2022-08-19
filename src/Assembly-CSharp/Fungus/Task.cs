using System;
using System.Collections;

namespace Fungus
{
	// Token: 0x02000F08 RID: 3848
	public class Task
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06006C4D RID: 27725 RVA: 0x002987EC File Offset: 0x002969EC
		public bool Running
		{
			get
			{
				return this.task.Running;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06006C4E RID: 27726 RVA: 0x002987F9 File Offset: 0x002969F9
		public bool Paused
		{
			get
			{
				return this.task.Paused;
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06006C4F RID: 27727 RVA: 0x00298808 File Offset: 0x00296A08
		// (remove) Token: 0x06006C50 RID: 27728 RVA: 0x00298840 File Offset: 0x00296A40
		public event Task.FinishedHandler Finished;

		// Token: 0x06006C51 RID: 27729 RVA: 0x00298875 File Offset: 0x00296A75
		public Task(IEnumerator c, bool autoStart = true)
		{
			this.task = TaskManager.CreateTask(c);
			this.task.Finished += this.TaskFinished;
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x002988A9 File Offset: 0x00296AA9
		public void Start()
		{
			this.task.Start();
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x002988B6 File Offset: 0x00296AB6
		public void Stop()
		{
			this.task.Stop();
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x002988C3 File Offset: 0x00296AC3
		public void Pause()
		{
			this.task.Pause();
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x002988D0 File Offset: 0x00296AD0
		public void Unpause()
		{
			this.task.Unpause();
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x002988E0 File Offset: 0x00296AE0
		private void TaskFinished(bool manual)
		{
			Task.FinishedHandler finished = this.Finished;
			if (finished != null)
			{
				finished(manual);
			}
		}

		// Token: 0x04005AF8 RID: 23288
		private TaskManager.TaskState task;

		// Token: 0x02001718 RID: 5912
		// (Invoke) Token: 0x0600891F RID: 35103
		public delegate void FinishedHandler(bool manual);
	}
}
