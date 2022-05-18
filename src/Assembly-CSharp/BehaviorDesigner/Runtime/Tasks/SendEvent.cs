using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200148F RID: 5263
	[TaskDescription("Sends an event to the behavior tree, returns success after sending the event.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=121")]
	[TaskIcon("{SkinColor}SendEventIcon.png")]
	public class SendEvent : Action
	{
		// Token: 0x06007E41 RID: 32321 RVA: 0x002C8804 File Offset: 0x002C6A04
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

		// Token: 0x06007E42 RID: 32322 RVA: 0x002C8884 File Offset: 0x002C6A84
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

		// Token: 0x06007E43 RID: 32323 RVA: 0x000555EB File Offset: 0x000537EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eventName = "";
		}

		// Token: 0x04006B8B RID: 27531
		[Tooltip("The GameObject of the behavior tree that should have the event sent to it. If null use the current behavior")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006B8C RID: 27532
		[Tooltip("The event to send")]
		public SharedString eventName;

		// Token: 0x04006B8D RID: 27533
		[Tooltip("The group of the behavior tree that the event should be sent to")]
		public SharedInt group;

		// Token: 0x04006B8E RID: 27534
		[Tooltip("Optionally specify a first argument to send")]
		[SharedRequired]
		public SharedVariable argument1;

		// Token: 0x04006B8F RID: 27535
		[Tooltip("Optionally specify a second argument to send")]
		[SharedRequired]
		public SharedVariable argument2;

		// Token: 0x04006B90 RID: 27536
		[Tooltip("Optionally specify a third argument to send")]
		[SharedRequired]
		public SharedVariable argument3;

		// Token: 0x04006B91 RID: 27537
		private BehaviorTree behaviorTree;
	}
}
