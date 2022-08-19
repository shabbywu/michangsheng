using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDE RID: 4062
	[TaskDescription("Similar to the selector task, the priority selector task will return success as soon as a child task returns success. Instead of running the tasks sequentially from left to right within the tree, the priority selector will ask the task what its priority is to determine the order. The higher priority tasks have a higher chance at being run first.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=29")]
	[TaskIcon("{SkinColor}PrioritySelectorIcon.png")]
	public class PrioritySelector : Composite
	{
		// Token: 0x06007078 RID: 28792 RVA: 0x002A9CAC File Offset: 0x002A7EAC
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

		// Token: 0x06007079 RID: 28793 RVA: 0x002A9D3E File Offset: 0x002A7F3E
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder[this.currentChildIndex];
		}

		// Token: 0x0600707A RID: 28794 RVA: 0x002A9D51 File Offset: 0x002A7F51
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != 2;
		}

		// Token: 0x0600707B RID: 28795 RVA: 0x002A9D74 File Offset: 0x002A7F74
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x0600707C RID: 28796 RVA: 0x002A9D8B File Offset: 0x002A7F8B
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x0600707D RID: 28797 RVA: 0x002A9D9B File Offset: 0x002A7F9B
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x04005CB1 RID: 23729
		private int currentChildIndex;

		// Token: 0x04005CB2 RID: 23730
		private TaskStatus executionStatus;

		// Token: 0x04005CB3 RID: 23731
		private List<int> childrenExecutionOrder = new List<int>();
	}
}
