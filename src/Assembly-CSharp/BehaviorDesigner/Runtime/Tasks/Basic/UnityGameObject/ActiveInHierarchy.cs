using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015EF RID: 5615
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveInHierarchy : Conditional
	{
		// Token: 0x06008361 RID: 33633 RVA: 0x0005A565 File Offset: 0x00058765
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeInHierarchy)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008362 RID: 33634 RVA: 0x0005A582 File Offset: 0x00058782
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400701F RID: 28703
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
