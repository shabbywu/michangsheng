using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE4 RID: 4068
	[TaskDescription("The utility selector task evaluates the child tasks using Utility Theory AI. The child task can override the GetUtility method and return the utility value at that particular time. The task with the highest utility value will be selected and the existing running task will be aborted. The utility selector task reevaluates its children every tick.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=134")]
	[TaskIcon("{SkinColor}UtilitySelectorIcon.png")]
	public class UtilitySelector : Composite
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x002AA2B8 File Offset: 0x002A84B8
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

		// Token: 0x060070AC RID: 28844 RVA: 0x002AA326 File Offset: 0x002A8526
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x002AA32E File Offset: 0x002A852E
		public override void OnChildStarted(int childIndex)
		{
			this.executionStatus = 3;
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x002AA337 File Offset: 0x002A8537
		public override bool CanExecute()
		{
			return this.executionStatus != 2 && this.executionStatus != 3 && !this.reevaluating && this.availableChildren.Count > 0;
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x002AA364 File Offset: 0x002A8564
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

		// Token: 0x060070B0 RID: 28848 RVA: 0x002AA3F8 File Offset: 0x002A85F8
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = 0;
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x002AA408 File Offset: 0x002A8608
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.currentChildIndex = 0;
		}

		// Token: 0x060070B2 RID: 28850 RVA: 0x002AA418 File Offset: 0x002A8618
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x002AA420 File Offset: 0x002A8620
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == null)
			{
				return false;
			}
			this.reevaluating = true;
			return true;
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x002AA434 File Offset: 0x002A8634
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

		// Token: 0x04005CC6 RID: 23750
		private int currentChildIndex;

		// Token: 0x04005CC7 RID: 23751
		private float highestUtility;

		// Token: 0x04005CC8 RID: 23752
		private TaskStatus executionStatus;

		// Token: 0x04005CC9 RID: 23753
		private bool reevaluating;

		// Token: 0x04005CCA RID: 23754
		private List<int> availableChildren = new List<int>();
	}
}
