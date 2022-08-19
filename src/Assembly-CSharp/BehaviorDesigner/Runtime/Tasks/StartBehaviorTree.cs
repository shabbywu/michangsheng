using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD8 RID: 4056
	[TaskDescription("Start a new behavior tree and return success after it has been started.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=20")]
	[TaskIcon("{SkinColor}StartBehaviorTreeIcon.png")]
	public class StartBehaviorTree : Action
	{
		// Token: 0x0600704B RID: 28747 RVA: 0x002A95D8 File Offset: 0x002A77D8
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

		// Token: 0x0600704C RID: 28748 RVA: 0x002A96EA File Offset: 0x002A78EA
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

		// Token: 0x0600704D RID: 28749 RVA: 0x002A9714 File Offset: 0x002A7914
		private void BehaviorEnded(Behavior behavior)
		{
			this.behaviorComplete = true;
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x002A971D File Offset: 0x002A791D
		public override void OnEnd()
		{
			if (this.behavior != null && this.waitForCompletion.Value)
			{
				this.behavior.OnBehaviorEnd -= new Behavior.BehaviorHandler(this.BehaviorEnded);
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x002A9751 File Offset: 0x002A7951
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.waitForCompletion = false;
			this.synchronizeVariables = false;
		}

		// Token: 0x04005C9A RID: 23706
		[Tooltip("The GameObject of the behavior tree that should be started. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04005C9B RID: 23707
		[Tooltip("The group of the behavior tree that should be started")]
		public SharedInt group;

		// Token: 0x04005C9C RID: 23708
		[Tooltip("Should this task wait for the behavior tree to complete?")]
		public SharedBool waitForCompletion = false;

		// Token: 0x04005C9D RID: 23709
		[Tooltip("Should the variables be synchronized?")]
		public SharedBool synchronizeVariables;

		// Token: 0x04005C9E RID: 23710
		private bool behaviorComplete;

		// Token: 0x04005C9F RID: 23711
		private Behavior behavior;
	}
}
