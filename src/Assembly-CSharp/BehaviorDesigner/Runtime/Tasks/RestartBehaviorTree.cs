using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148E RID: 5262
	[TaskDescription("Restarts a behavior tree, returns success after it has been restarted.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=66")]
	[TaskIcon("{SkinColor}RestartBehaviorTreeIcon.png")]
	public class RestartBehaviorTree : Action
	{
		// Token: 0x06007E3D RID: 32317 RVA: 0x002C8784 File Offset: 0x002C6984
		public override void OnAwake()
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

		// Token: 0x06007E3E RID: 32318 RVA: 0x000555B9 File Offset: 0x000537B9
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return 1;
			}
			this.behavior.DisableBehavior();
			this.behavior.EnableBehavior();
			return 2;
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x000555E2 File Offset: 0x000537E2
		public override void OnReset()
		{
			this.behavior = null;
		}

		// Token: 0x04006B88 RID: 27528
		[Tooltip("The GameObject of the behavior tree that should be restarted. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04006B89 RID: 27529
		[Tooltip("The group of the behavior tree that should be restarted")]
		public SharedInt group;

		// Token: 0x04006B8A RID: 27530
		private Behavior behavior;
	}
}
