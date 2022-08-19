using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD6 RID: 4054
	[TaskDescription("Restarts a behavior tree, returns success after it has been restarted.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=66")]
	[TaskIcon("{SkinColor}RestartBehaviorTreeIcon.png")]
	public class RestartBehaviorTree : Action
	{
		// Token: 0x06007043 RID: 28739 RVA: 0x002A9398 File Offset: 0x002A7598
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

		// Token: 0x06007044 RID: 28740 RVA: 0x002A9417 File Offset: 0x002A7617
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

		// Token: 0x06007045 RID: 28741 RVA: 0x002A9440 File Offset: 0x002A7640
		public override void OnReset()
		{
			this.behavior = null;
		}

		// Token: 0x04005C90 RID: 23696
		[Tooltip("The GameObject of the behavior tree that should be restarted. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04005C91 RID: 23697
		[Tooltip("The group of the behavior tree that should be restarted")]
		public SharedInt group;

		// Token: 0x04005C92 RID: 23698
		private Behavior behavior;
	}
}
