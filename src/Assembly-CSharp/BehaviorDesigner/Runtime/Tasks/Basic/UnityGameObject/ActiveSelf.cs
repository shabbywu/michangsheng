using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001131 RID: 4401
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveSelf : Conditional
	{
		// Token: 0x0600756A RID: 30058 RVA: 0x002B45C0 File Offset: 0x002B27C0
		public override TaskStatus OnUpdate()
		{
			if (!base.GetDefaultGameObject(this.targetGameObject.Value).activeSelf)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x002B45DD File Offset: 0x002B27DD
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040060FD RID: 24829
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
