using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001496 RID: 5270
	[TaskDescription("Similar to the selector task, the priority selector task will return success as soon as a child task returns success. Instead of running the tasks sequentially from left to right within the tree, the priority selector will ask the task what its priority is to determine the order. The higher priority tasks have a higher chance at being run first.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=29")]
	[TaskIcon("{SkinColor}PrioritySelectorIcon.png")]
	public class PrioritySelector : Composite
	{
		// Token: 0x06007E72 RID: 32370 RVA: 0x002C8DA4 File Offset: 0x002C6FA4
		public override void OnStart()
		{
			this.childrenExecutionOrder.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float priority = this.children[i].GetPriority();
				int index = this.childrenExecutionOrder.Count;
				for (int j = 0; j < this.childrenExecutionOrder.Count; j++)
				{
					if (this.children[this.childrenExecutionOrder[j]].GetPriority() < priority)
					{
						index = j;
						break;
					}
				}
				this.childrenExecutionOrder.Insert(index, i);
			}
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x0005589E File Offset: 0x00053A9E
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder[this.currentChildIndex];
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x000558B1 File Offset: 0x00053AB1
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 2;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x000558D4 File Offset: 0x00053AD4
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x000558EB File Offset: 0x00053AEB
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x000558FB File Offset: 0x00053AFB
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BA9 RID: 27561
		private int currentChildIndex;

		// Token: 0x04006BAA RID: 27562
		private TaskStatus executionStatus;

		// Token: 0x04006BAB RID: 27563
		private List<int> childrenExecutionOrder = new List<int>();
	}
}
