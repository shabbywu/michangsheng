using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164A RID: 5706
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Gets the avatar delta position for the last evaluated frame. Returns Success.")]
	public class GetDeltaPosition : Action
	{
		// Token: 0x060084BA RID: 33978 RVA: 0x002CFF00 File Offset: 0x002CE100
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x0005BE22 File Offset: 0x0005A022
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.deltaPosition;
			return 2;
		}

		// Token: 0x060084BC RID: 33980 RVA: 0x0005BE55 File Offset: 0x0005A055
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04007166 RID: 29030
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007167 RID: 29031
		[Tooltip("The avatar delta position")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04007168 RID: 29032
		private Animator animator;

		// Token: 0x04007169 RID: 29033
		private GameObject prevGameObject;
	}
}
