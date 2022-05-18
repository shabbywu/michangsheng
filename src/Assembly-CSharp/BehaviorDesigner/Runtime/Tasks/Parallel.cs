using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001493 RID: 5267
	[TaskDescription("Similar to the sequence task, the parallel task will run each child task until a child task returns failure. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. Like the sequence class, the parallel task will return success once all of its children tasks have return success. If one tasks returns failure the parallel task will end all of the child tasks and return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=27")]
	[TaskIcon("{SkinColor}ParallelIcon.png")]
	public class Parallel : Composite
	{
		// Token: 0x06007E54 RID: 32340 RVA: 0x0005578B File Offset: 0x0005398B
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x000557A3 File Offset: 0x000539A3
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x000557BC File Offset: 0x000539BC
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x000557C4 File Offset: 0x000539C4
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x000557D9 File Offset: 0x000539D9
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x002C8BB0 File Offset: 0x002C6DB0
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 3)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == 1)
				{
					return 1;
				}
			}
			if (!flag)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x002C8BF4 File Offset: 0x002C6DF4
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x002C8C24 File Offset: 0x002C6E24
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BA3 RID: 27555
		private int currentChildIndex;

		// Token: 0x04006BA4 RID: 27556
		private TaskStatus[] executionStatus;
	}
}
