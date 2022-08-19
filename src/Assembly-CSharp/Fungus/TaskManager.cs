using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F09 RID: 3849
	internal class TaskManager : MonoBehaviour
	{
		// Token: 0x06006C57 RID: 27735 RVA: 0x002988FE File Offset: 0x00296AFE
		public static TaskManager.TaskState CreateTask(IEnumerator coroutine)
		{
			if (TaskManager.singleton == null)
			{
				TaskManager.singleton = new GameObject("TaskManager").AddComponent<TaskManager>();
			}
			return new TaskManager.TaskState(coroutine);
		}

		// Token: 0x04005AF9 RID: 23289
		private static TaskManager singleton;

		// Token: 0x02001719 RID: 5913
		public class TaskState
		{
			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06008922 RID: 35106 RVA: 0x002EA22A File Offset: 0x002E842A
			public bool Running
			{
				get
				{
					return this.running;
				}
			}

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x06008923 RID: 35107 RVA: 0x002EA232 File Offset: 0x002E8432
			public bool Paused
			{
				get
				{
					return this.paused;
				}
			}

			// Token: 0x1400006E RID: 110
			// (add) Token: 0x06008924 RID: 35108 RVA: 0x002EA23C File Offset: 0x002E843C
			// (remove) Token: 0x06008925 RID: 35109 RVA: 0x002EA274 File Offset: 0x002E8474
			public event TaskManager.TaskState.FinishedHandler Finished;

			// Token: 0x06008926 RID: 35110 RVA: 0x002EA2A9 File Offset: 0x002E84A9
			public TaskState(IEnumerator c)
			{
				this.coroutine = c;
			}

			// Token: 0x06008927 RID: 35111 RVA: 0x002EA2B8 File Offset: 0x002E84B8
			public void Pause()
			{
				this.paused = true;
			}

			// Token: 0x06008928 RID: 35112 RVA: 0x002EA2C1 File Offset: 0x002E84C1
			public void Unpause()
			{
				this.paused = false;
			}

			// Token: 0x06008929 RID: 35113 RVA: 0x002EA2CA File Offset: 0x002E84CA
			public void Start()
			{
				this.running = true;
				TaskManager.singleton.StartCoroutine(this.CallWrapper());
			}

			// Token: 0x0600892A RID: 35114 RVA: 0x002EA2E4 File Offset: 0x002E84E4
			public void Stop()
			{
				this.stopped = true;
				this.running = false;
			}

			// Token: 0x0600892B RID: 35115 RVA: 0x002EA2F4 File Offset: 0x002E84F4
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

			// Token: 0x040074D0 RID: 29904
			private IEnumerator coroutine;

			// Token: 0x040074D1 RID: 29905
			private bool running;

			// Token: 0x040074D2 RID: 29906
			private bool paused;

			// Token: 0x040074D3 RID: 29907
			private bool stopped;

			// Token: 0x02001761 RID: 5985
			// (Invoke) Token: 0x0600899D RID: 35229
			public delegate void FinishedHandler(bool manual);
		}
	}
}
