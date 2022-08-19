using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x0200113C RID: 4412
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Sends a message to the target GameObject. Returns Success.")]
	public class SendMessage : Action
	{
		// Token: 0x0600758B RID: 30091 RVA: 0x002B489C File Offset: 0x002B2A9C
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

		// Token: 0x0600758C RID: 30092 RVA: 0x002B4910 File Offset: 0x002B2B10
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.message = "";
		}

		// Token: 0x04006114 RID: 24852
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006115 RID: 24853
		[Tooltip("The message to send")]
		public SharedString message;

		// Token: 0x04006116 RID: 24854
		[Tooltip("The value to send")]
		public SharedGenericVariable value;
	}
}
