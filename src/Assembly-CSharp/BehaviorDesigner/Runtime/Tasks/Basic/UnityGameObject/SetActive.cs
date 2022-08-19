using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x0200113D RID: 4413
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Activates/Deactivates the GameObject. Returns Success.")]
	public class SetActive : Action
	{
		// Token: 0x0600758E RID: 30094 RVA: 0x002B4929 File Offset: 0x002B2B29
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).SetActive(this.active.Value);
			return 2;
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x002B494D File Offset: 0x002B2B4D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.active = false;
		}

		// Token: 0x04006117 RID: 24855
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006118 RID: 24856
		[Tooltip("Active state of the GameObject")]
		public SharedBool active;
	}
}
