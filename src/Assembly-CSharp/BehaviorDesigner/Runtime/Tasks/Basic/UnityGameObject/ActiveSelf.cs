using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F0 RID: 5616
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveSelf : Conditional
	{
		// Token: 0x06008364 RID: 33636 RVA: 0x0005A58B File Offset: 0x0005878B
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeSelf)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008365 RID: 33637 RVA: 0x0005A5A8 File Offset: 0x000587A8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007020 RID: 28704
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
