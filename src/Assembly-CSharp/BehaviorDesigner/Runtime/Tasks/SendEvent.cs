using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD7 RID: 4055
	[TaskDescription("Sends an event to the behavior tree, returns success after sending the event.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=121")]
	[TaskIcon("{SkinColor}SendEventIcon.png")]
	public class SendEvent : Action
	{
		// Token: 0x06007047 RID: 28743 RVA: 0x002A944C File Offset: 0x002A764C
		public override void OnStart()
		{
			BehaviorTree[] components = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponents<BehaviorTree>();
			if (components.Length == 1)
			{
				this.behaviorTree = components[0];
				return;
			}
			if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behaviorTree = components[i];
						break;
					}
				}
				if (this.behaviorTree == null)
				{
					this.behaviorTree = components[0];
				}
			}
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x002A94CC File Offset: 0x002A76CC
		public override TaskStatus OnUpdate()
		{
			if (this.argument1 == null || this.argument1.IsNone)
			{
				this.behaviorTree.SendEvent(this.eventName.Value);
			}
			else if (this.argument2 == null || this.argument2.IsNone)
			{
				this.behaviorTree.SendEvent<object>(this.eventName.Value, this.argument1.GetValue());
			}
			else if (this.argument3 == null || this.argument3.IsNone)
			{
				this.behaviorTree.SendEvent<object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue());
			}
			else
			{
				this.behaviorTree.SendEvent<object, object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue(), this.argument3.GetValue());
			}
			return 2;
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x002A95BC File Offset: 0x002A77BC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eventName = "";
		}

		// Token: 0x04005C93 RID: 23699
		[Tooltip("The GameObject of the behavior tree that should have the event sent to it. If null use the current behavior")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005C94 RID: 23700
		[Tooltip("The event to send")]
		public SharedString eventName;

		// Token: 0x04005C95 RID: 23701
		[Tooltip("The group of the behavior tree that the event should be sent to")]
		public SharedInt group;

		// Token: 0x04005C96 RID: 23702
		[Tooltip("Optionally specify a first argument to send")]
		[SharedRequired]
		public SharedVariable argument1;

		// Token: 0x04005C97 RID: 23703
		[Tooltip("Optionally specify a second argument to send")]
		[SharedRequired]
		public SharedVariable argument2;

		// Token: 0x04005C98 RID: 23704
		[Tooltip("Optionally specify a third argument to send")]
		[SharedRequired]
		public SharedVariable argument3;

		// Token: 0x04005C99 RID: 23705
		private BehaviorTree behaviorTree;
	}
}
