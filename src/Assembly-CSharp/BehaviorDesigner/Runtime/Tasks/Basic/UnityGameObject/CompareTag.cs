using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001133 RID: 4403
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if tags match, otherwise Failure.")]
	public class CompareTag : Conditional
	{
		// Token: 0x06007570 RID: 30064 RVA: 0x002B4618 File Offset: 0x002B2818
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).CompareTag(this.tag.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x002B4640 File Offset: 0x002B2840
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x04006100 RID: 24832
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006101 RID: 24833
		[Tooltip("The tag to compare against")]
		public SharedString tag;
	}
}
