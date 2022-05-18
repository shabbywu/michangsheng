using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001490 RID: 5264
	[TaskDescription("Start a new behavior tree and return success after it has been started.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=20")]
	[TaskIcon("{SkinColor}StartBehaviorTreeIcon.png")]
	public class StartBehaviorTree : Action
	{
		// Token: 0x06007E45 RID: 32325 RVA: 0x002C8974 File Offset: 0x002C6B74
		public override void OnStart()
		{
			Behavior[] components = base.GetDefaultGameObject(this.behaviorGameObject.Value).GetComponents<Behavior>();
			if (components.Length == 1)
			{
				this.behavior = components[0];
			}
			else if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behavior = components[i];
						break;
					}
				}
				if (this.behavior == null)
				{
					this.behavior = components[0];
				}
			}
			if (this.behavior != null)
			{
				List<SharedVariable> allVariables = base.Owner.GetAllVariables();
				if (allVariables != null && this.synchronizeVariables.Value)
				{
					for (int j = 0; j < allVariables.Count; j++)
					{
						this.behavior.SetVariableValue(allVariables[j].Name, allVariables[j]);
					}
				}
				this.behavior.EnableBehavior();
				if (this.waitForCompletion.Value)
				{
					this.behaviorComplete = false;
					this.behavior.OnBehaviorEnd += new Behavior.BehaviorHandler(this.BehaviorEnded);
				}
			}
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x00055604 File Offset: 0x00053804
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return 1;
			}
			if (this.waitForCompletion.Value && !this.behaviorComplete)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x0005562E File Offset: 0x0005382E
		private void BehaviorEnded(Behavior behavior)
		{
			this.behaviorComplete = true;
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x00055637 File Offset: 0x00053837
		public override void OnEnd()
		{
			if (this.behavior != null && this.waitForCompletion.Value)
			{
				this.behavior.OnBehaviorEnd -= new Behavior.BehaviorHandler(this.BehaviorEnded);
			}
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x0005566B File Offset: 0x0005386B
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.waitForCompletion = false;
			this.synchronizeVariables = false;
		}

		// Token: 0x04006B92 RID: 27538
		[Tooltip("The GameObject of the behavior tree that should be started. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04006B93 RID: 27539
		[Tooltip("The group of the behavior tree that should be started")]
		public SharedInt group;

		// Token: 0x04006B94 RID: 27540
		[Tooltip("Should this task wait for the behavior tree to complete?")]
		public SharedBool waitForCompletion = false;

		// Token: 0x04006B95 RID: 27541
		[Tooltip("Should the variables be synchronized?")]
		public SharedBool synchronizeVariables;

		// Token: 0x04006B96 RID: 27542
		private bool behaviorComplete;

		// Token: 0x04006B97 RID: 27543
		private Behavior behavior;
	}
}
