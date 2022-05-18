using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149C RID: 5276
	[TaskDescription("The utility selector task evaluates the child tasks using Utility Theory AI. The child task can override the GetUtility method and return the utility value at that particular time. The task with the highest utility value will be selected and the existing running task will be aborted. The utility selector task reevaluates its children every tick.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=134")]
	[TaskIcon("{SkinColor}UtilitySelectorIcon.png")]
	public class UtilitySelector : Composite
	{
		// Token: 0x06007EA5 RID: 32421 RVA: 0x002C9074 File Offset: 0x002C7274
		public override void OnStart()
		{
			this.highestUtility = float.MinValue;
			this.availableChildren.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float utility = this.children[i].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = i;
				}
				this.availableChildren.Add(i);
			}
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x00055BDF File Offset: 0x00053DDF
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x00055BE7 File Offset: 0x00053DE7
		public override void OnChildStarted(int childIndex)
		{
			this.executionStatus = 3;
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x00055BF0 File Offset: 0x00053DF0
		public override bool CanExecute()
		{
			return this.executionStatus != 2 && this.executionStatus != 3 && !this.reevaluating && this.availableChildren.Count > 0;
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x002C90E4 File Offset: 0x002C72E4
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus != null && childStatus != 3)
			{
				this.executionStatus = childStatus;
				if (this.executionStatus == 1)
				{
					this.availableChildren.Remove(childIndex);
					this.highestUtility = float.MinValue;
					for (int i = 0; i < this.availableChildren.Count; i++)
					{
						float utility = this.children[this.availableChildren[i]].GetUtility();
						if (utility > this.highestUtility)
						{
							this.highestUtility = utility;
							this.currentChildIndex = this.availableChildren[i];
						}
					}
				}
			}
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x00055C1C File Offset: 0x00053E1C
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x00055C2C File Offset: 0x00053E2C
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x00055C3C File Offset: 0x00053E3C
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x00055C44 File Offset: 0x00053E44
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == null)
			{
				return false;
			}
			this.reevaluating = true;
			return true;
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x002C9178 File Offset: 0x002C7378
		public override void OnReevaluationEnded(TaskStatus status)
		{
			this.reevaluating = false;
			int num = this.currentChildIndex;
			this.highestUtility = float.MinValue;
			for (int i = 0; i < this.availableChildren.Count; i++)
			{
				float utility = this.children[this.availableChildren[i]].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = this.availableChildren[i];
				}
			}
			if (num != this.currentChildIndex)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[num], this);
				this.executionStatus = 0;
			}
		}

		// Token: 0x04006BBE RID: 27582
		private int currentChildIndex;

		// Token: 0x04006BBF RID: 27583
		private float highestUtility;

		// Token: 0x04006BC0 RID: 27584
		private TaskStatus executionStatus;

		// Token: 0x04006BC1 RID: 27585
		private bool reevaluating;

		// Token: 0x04006BC2 RID: 27586
		private List<int> availableChildren = new List<int>();
	}
}
