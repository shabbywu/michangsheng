using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001491 RID: 5265
	[TaskDescription("Pause or disable a behavior tree and return success after it has been stopped.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=21")]
	[TaskIcon("{SkinColor}StopBehaviorTreeIcon.png")]
	public class StopBehaviorTree : Action
	{
		// Token: 0x06007E4B RID: 32331 RVA: 0x002C8A88 File Offset: 0x002C6C88
		public override void OnStart()
		{
			Behavior[] components = base.GetDefaultGameObject(this.behaviorGameObject.Value).GetComponents<Behavior>();
			if (components.Length == 1)
			{
				this.behavior = components[0];
				return;
			}
			if (components.Length > 1)
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
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x000556AC File Offset: 0x000538AC
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return 1;
			}
			this.behavior.DisableBehavior(this.pauseBehavior.Value);
			return 2;
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x000556D5 File Offset: 0x000538D5
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.pauseBehavior = false;
		}

		// Token: 0x04006B98 RID: 27544
		[Tooltip("The GameObject of the behavior tree that should be stopped. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04006B99 RID: 27545
		[Tooltip("The group of the behavior tree that should be stopped")]
		public SharedInt group;

		// Token: 0x04006B9A RID: 27546
		[Tooltip("Should the behavior be paused or completely disabled")]
		public SharedBool pauseBehavior = false;

		// Token: 0x04006B9B RID: 27547
		private Behavior behavior;
	}
}
