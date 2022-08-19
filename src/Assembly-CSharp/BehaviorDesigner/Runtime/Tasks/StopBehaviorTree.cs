using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD9 RID: 4057
	[TaskDescription("Pause or disable a behavior tree and return success after it has been stopped.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=21")]
	[TaskIcon("{SkinColor}StopBehaviorTreeIcon.png")]
	public class StopBehaviorTree : Action
	{
		// Token: 0x06007051 RID: 28753 RVA: 0x002A9794 File Offset: 0x002A7994
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

		// Token: 0x06007052 RID: 28754 RVA: 0x002A9813 File Offset: 0x002A7A13
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return 1;
			}
			this.behavior.DisableBehavior(this.pauseBehavior.Value);
			return 2;
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x002A983C File Offset: 0x002A7A3C
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.pauseBehavior = false;
		}

		// Token: 0x04005CA0 RID: 23712
		[Tooltip("The GameObject of the behavior tree that should be stopped. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04005CA1 RID: 23713
		[Tooltip("The group of the behavior tree that should be stopped")]
		public SharedInt group;

		// Token: 0x04005CA2 RID: 23714
		[Tooltip("Should the behavior be paused or completely disabled")]
		public SharedBool pauseBehavior = false;

		// Token: 0x04005CA3 RID: 23715
		private Behavior behavior;
	}
}
