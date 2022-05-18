using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F9 RID: 5625
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Stores the GameObject tag. Returns Success.")]
	public class GetTag : Action
	{
		// Token: 0x0600837F RID: 33663 RVA: 0x0005A72E File Offset: 0x0005892E
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).tag;
			return 2;
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x0005A752 File Offset: 0x00058952
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = "";
		}

		// Token: 0x04007031 RID: 28721
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007032 RID: 28722
		[Tooltip("Active state of the GameObject")]
		[RequiredField]
		public SharedString storeValue;
	}
}
