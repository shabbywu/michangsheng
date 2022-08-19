using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x0200113A RID: 4410
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Stores the GameObject tag. Returns Success.")]
	public class GetTag : Action
	{
		// Token: 0x06007585 RID: 30085 RVA: 0x002B47EA File Offset: 0x002B29EA
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).tag;
			return 2;
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x002B480E File Offset: 0x002B2A0E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = "";
		}

		// Token: 0x0400610E RID: 24846
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400610F RID: 24847
		[Tooltip("Active state of the GameObject")]
		[RequiredField]
		public SharedString storeValue;
	}
}
