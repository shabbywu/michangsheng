using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015FB RID: 5627
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Sends a message to the target GameObject. Returns Success.")]
	public class SendMessage : Action
	{
		// Token: 0x06008385 RID: 33669 RVA: 0x002CE9F8 File Offset: 0x002CCBF8
		public override TaskStatus OnUpdate()
		{
			if (this.value.Value != null)
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value, this.value.Value.value.GetValue());
			}
			else
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value);
			}
			return 2;
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x0005A7E0 File Offset: 0x000589E0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.message = "";
		}

		// Token: 0x04007037 RID: 28727
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007038 RID: 28728
		[Tooltip("The message to send")]
		public SharedString message;

		// Token: 0x04007039 RID: 28729
		[Tooltip("The value to send")]
		public SharedGenericVariable value;
	}
}
