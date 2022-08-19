using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118A RID: 4490
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the bool parameter on an animator. Returns Success.")]
	public class GetBoolParameter : Action
	{
		// Token: 0x060076BC RID: 30396 RVA: 0x002B736C File Offset: 0x002B556C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076BD RID: 30397 RVA: 0x002B73AC File Offset: 0x002B55AC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetBool(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x002B73EA File Offset: 0x002B55EA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = false;
		}

		// Token: 0x0400623E RID: 25150
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400623F RID: 25151
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04006240 RID: 25152
		[Tooltip("The value of the bool parameter")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04006241 RID: 25153
		private Animator animator;

		// Token: 0x04006242 RID: 25154
		private GameObject prevGameObject;
	}
}
