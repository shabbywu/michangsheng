using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F2 RID: 5618
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if tags match, otherwise Failure.")]
	public class CompareTag : Conditional
	{
		// Token: 0x0600836A RID: 33642 RVA: 0x0005A5E3 File Offset: 0x000587E3
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).CompareTag(this.tag.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600836B RID: 33643 RVA: 0x0005A60B File Offset: 0x0005880B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x04007023 RID: 28707
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007024 RID: 28708
		[Tooltip("The tag to compare against")]
		public SharedString tag;
	}
}
