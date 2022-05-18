using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164B RID: 5707
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Gets the avatar delta rotation for the last evaluated frame. Returns Success.")]
	public class GetDeltaRotation : Action
	{
		// Token: 0x060084BE RID: 33982 RVA: 0x002CFF40 File Offset: 0x002CE140
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084BF RID: 33983 RVA: 0x0005BE6E File Offset: 0x0005A06E
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

		// Token: 0x060084C0 RID: 33984 RVA: 0x0005BEA1 File Offset: 0x0005A0A1
		public override void OnReset()
		{
			if (this.storeValue != null)
			{
				this.storeValue.Value = Quaternion.identity;
			}
		}

		// Token: 0x0400716A RID: 29034
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400716B RID: 29035
		[Tooltip("The avatar delta rotation")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x0400716C RID: 29036
		private Animator animator;

		// Token: 0x0400716D RID: 29037
		private GameObject prevGameObject;
	}
}
