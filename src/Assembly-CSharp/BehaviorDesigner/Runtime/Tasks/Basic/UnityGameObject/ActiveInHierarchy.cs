using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001130 RID: 4400
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveInHierarchy : Conditional
	{
		// Token: 0x06007567 RID: 30055 RVA: 0x002B459A File Offset: 0x002B279A
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeInHierarchy)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x002B45B7 File Offset: 0x002B27B7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040060FC RID: 24828
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
