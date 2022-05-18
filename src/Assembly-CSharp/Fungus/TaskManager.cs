using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013AF RID: 5039
	internal class TaskManager : MonoBehaviour
	{
		// Token: 0x06007A0F RID: 31247 RVA: 0x00053464 File Offset: 0x00051664
		public static TaskManager.TaskState CreateTask(IEnumerator coroutine)
		{
			if (TaskManager.singleton == null)
			{
				TaskManager.singleton = new GameObject("TaskManager").AddComponent<TaskManager>();
			}
			return new TaskManager.TaskState(coroutine);
		}

		// Token: 0x04006972 RID: 26994
		private static TaskManager singleton;

		// Token: 0x020013B0 RID: 5040
		public class TaskState
		{
			// Token: 0x17000B8E RID: 2958
			// (get) Token: 0x06007A11 RID: 31249 RVA: 0x0005348D File Offset: 0x0005168D
			public bool Running
			{
				get
				{
					return this.running;
				}
			}

			// Token: 0x17000B8F RID: 2959
			// (get) Token: 0x06007A12 RID: 31250 RVA: 0x00053495 File Offset: 0x00051695
			public bool Paused
			{
				get
				{
					return this.paused;
				}
			}

			// Token: 0x1400006E RID: 110
			// (add) Token: 0x06007A13 RID: 31251 RVA: 0x002B98F8 File Offset: 0x002B7AF8
			// (remove) Token: 0x06007A14 RID: 31252 RVA: 0x002B9930 File Offset: 0x002B7B30
			public event TaskManager.TaskState.FinishedHandler Finished;

			// Token: 0x06007A15 RID: 31253 RVA: 0x0005349D File Offset: 0x0005169D
			public TaskState(IEnumerator c)
			{
				this.coroutine = c;
			}

			// Token: 0x06007A16 RID: 31254 RVA: 0x000534AC File Offset: 0x000516AC
			public void Pause()
			{
				this.paused = true;
			}

			// Token: 0x06007A17 RID: 31255 RVA: 0x000534B5 File Offset: 0x000516B5
			public void Unpause()
			{
				this.paused = false;
			}

			// Token: 0x06007A18 RID: 31256 RVA: 0x000534BE File Offset: 0x000516BE
			public void Start()
			{
				this.running = true;
				TaskManager.singleton.StartCoroutine(this.CallWrapper());
			}

			// Token: 0x06007A19 RID: 31257 RVA: 0x000534D8 File Offset: 0x000516D8
			public void Stop()
			{
				this.stopped = true;
				this.running = false;
			}

			// Token: 0x06007A1A RID: 31258 RVA: 0x000534E8 File Offset: 0x000516E8
			private IEnumerator CallWrapper()
			{
				yield return null;
				IEnumerator e = this.coroutine;
				while (this.running)
				{
					if (this.paused)
					{
						yield return null;
					}
					else if (e != null && e.MoveNext())
					{
						yield return e.Current;
					}
					else
					{
						this.running = false;
					}
				}
				TaskManager.TaskState.FinishedHandler finished = this.Finished;
				if (finished != null)
				{
					finished(this.stopped);
				}
				yield break;
			}

			// Token: 0x04006974 RID: 26996
			private IEnumerator coroutine;

			// Token: 0x04006975 RID: 26997
			private bool running;

			// Token: 0x04006976 RID: 26998
			private bool paused;

			// Token: 0x04006977 RID: 26999
			private bool stopped;

			// Token: 0x020013B1 RID: 5041
			// (Invoke) Token: 0x06007A1C RID: 31260
			public delegate void FinishedHandler(bool manual);
		}
	}
}
