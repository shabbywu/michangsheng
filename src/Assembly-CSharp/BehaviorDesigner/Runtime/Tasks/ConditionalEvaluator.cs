using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A9 RID: 5289
	[TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not return success then the child task is not run and a failure status is immediately returned.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=146")]
	[TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
	public class ConditionalEvaluator : Decorator
	{
		// Token: 0x06007EEE RID: 32494 RVA: 0x002C9924 File Offset: 0x002C7B24
		public override void OnAwake()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.Owner = base.Owner;
				this.conditionalTask.GameObject = this.gameObject;
				this.conditionalTask.Transform = this.transform;
				this.conditionalTask.OnAwake();
			}
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x00055FC9 File Offset: 0x000541C9
		public override void OnStart()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnStart();
			}
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x00055FDE File Offset: 0x000541DE
		public override bool CanExecute()
		{
			if (this.checkConditionalTask)
			{
				this.checkConditionalTask = false;
				this.OnUpdate();
			}
			return !this.conditionalTaskFailed && (this.executionStatus == null || this.executionStatus == 3);
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x00056013 File Offset: 0x00054213
		public override bool CanReevaluate()
		{
			return this.reevaluate.Value;
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x002C9978 File Offset: 0x002C7B78
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.conditionalTask.OnUpdate();
			this.conditionalTaskFailed = (this.conditionalTask == null || taskStatus == 1);
			return taskStatus;
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x00056020 File Offset: 0x00054220
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x0000A093 File Offset: 0x00008293
		public override TaskStatus OverrideStatus()
		{
			return 1;
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x00056029 File Offset: 0x00054229
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.conditionalTaskFailed)
			{
				return 1;
			}
			return status;
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x00056036 File Offset: 0x00054236
		public override void OnEnd()
		{
			this.executionStatus = 0;
			this.checkConditionalTask = true;
			this.conditionalTaskFailed = false;
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnEnd();
			}
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x00056060 File Offset: 0x00054260
		public override void OnReset()
		{
			this.conditionalTask = null;
		}

		// Token: 0x04006BED RID: 27629
		[Tooltip("Should the conditional task be reevaluated every tick?")]
		public SharedBool reevaluate;

		// Token: 0x04006BEE RID: 27630
		[InspectTask]
		[Tooltip("The conditional task to evaluate")]
		public Conditional conditionalTask;

		// Token: 0x04006BEF RID: 27631
		private TaskStatus executionStatus;

		// Token: 0x04006BF0 RID: 27632
		private bool checkConditionalTask = true;

		// Token: 0x04006BF1 RID: 27633
		private bool conditionalTaskFailed;
	}
}
