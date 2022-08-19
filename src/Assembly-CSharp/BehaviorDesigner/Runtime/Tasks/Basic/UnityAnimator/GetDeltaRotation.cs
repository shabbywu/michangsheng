using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118C RID: 4492
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Gets the avatar delta rotation for the last evaluated frame. Returns Success.")]
	public class GetDeltaRotation : Action
	{
		// Token: 0x060076C4 RID: 30404 RVA: 0x002B749C File Offset: 0x002B569C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x002B74DC File Offset: 0x002B56DC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.deltaRotation;
			return 2;
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x002B750F File Offset: 0x002B570F
		public override void OnReset()
		{
			if (this.storeValue != null)
			{
				this.storeValue.Value = Quaternion.identity;
			}
		}

		// Token: 0x04006247 RID: 25159
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006248 RID: 25160
		[Tooltip("The avatar delta rotation")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04006249 RID: 25161
		private Animator animator;

		// Token: 0x0400624A RID: 25162
		private GameObject prevGameObject;
	}
}
