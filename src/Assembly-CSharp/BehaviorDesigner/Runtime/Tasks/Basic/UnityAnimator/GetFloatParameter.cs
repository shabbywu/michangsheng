using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118D RID: 4493
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the float parameter on an animator. Returns Success.")]
	public class GetFloatParameter : Action
	{
		// Token: 0x060076C8 RID: 30408 RVA: 0x002B752C File Offset: 0x002B572C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076C9 RID: 30409 RVA: 0x002B756C File Offset: 0x002B576C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetFloat(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x002B75AA File Offset: 0x002B57AA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0f;
		}

		// Token: 0x0400624B RID: 25163
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400624C RID: 25164
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400624D RID: 25165
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400624E RID: 25166
		private Animator animator;

		// Token: 0x0400624F RID: 25167
		private GameObject prevGameObject;
	}
}
