using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015FC RID: 5628
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Activates/Deactivates the GameObject. Returns Success.")]
	public class SetActive : Action
	{
		// Token: 0x06008388 RID: 33672 RVA: 0x0005A7F9 File Offset: 0x000589F9
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).SetActive(this.active.Value);
			return 2;
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x0005A81D File Offset: 0x00058A1D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.active = false;
		}

		// Token: 0x0400703A RID: 28730
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400703B RID: 28731
		[Tooltip("Active state of the GameObject")]
		public SharedBool active;
	}
}
