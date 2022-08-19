using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF1 RID: 4081
	[TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not return success then the child task is not run and a failure status is immediately returned.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=146")]
	[TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
	public class ConditionalEvaluator : Decorator
	{
		// Token: 0x060070F4 RID: 28916 RVA: 0x002AAF54 File Offset: 0x002A9154
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

		// Token: 0x060070F5 RID: 28917 RVA: 0x002AAFA7 File Offset: 0x002A91A7
		public override void OnStart()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnStart();
			}
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x002AAFBC File Offset: 0x002A91BC
		public override bool CanExecute()
		{
			if (this.checkConditionalTask)
			{
				this.checkConditionalTask = false;
				this.OnUpdate();
			}
			return !this.conditionalTaskFailed && (this.executionStatus == null || this.executionStatus == 3);
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x002AAFF1 File Offset: 0x002A91F1
		public override bool CanReevaluate()
		{
			return this.reevaluate.Value;
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x002AB000 File Offset: 0x002A9200
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.conditionalTask.OnUpdate();
			this.conditionalTaskFailed = (this.conditionalTask == null || taskStatus == 1);
			return taskStatus;
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x002AB02F File Offset: 0x002A922F
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override TaskStatus OverrideStatus()
		{
			return 1;
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x002AB038 File Offset: 0x002A9238
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.conditionalTaskFailed)
			{
				return 1;
			}
			return status;
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x002AB045 File Offset: 0x002A9245
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

		// Token: 0x060070FD RID: 28925 RVA: 0x002AB06F File Offset: 0x002A926F
		public override void OnReset()
		{
			this.conditionalTask = null;
		}

		// Token: 0x04005CF5 RID: 23797
		[Tooltip("Should the conditional task be reevaluated every tick?")]
		public SharedBool reevaluate;

		// Token: 0x04005CF6 RID: 23798
		[InspectTask]
		[Tooltip("The conditional task to evaluate")]
		public Conditional conditionalTask;

		// Token: 0x04005CF7 RID: 23799
		private TaskStatus executionStatus;

		// Token: 0x04005CF8 RID: 23800
		private bool checkConditionalTask = true;

		// Token: 0x04005CF9 RID: 23801
		private bool conditionalTaskFailed;
	}
}
