using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001652 RID: 5714
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Interrupts the automatic target matching. Returns Success.")]
	public class InterruptMatchTarget : Action
	{
		// Token: 0x060084D9 RID: 34009 RVA: 0x002D00C0 File Offset: 0x002CE2C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084DA RID: 34010 RVA: 0x0005C0BC File Offset: 0x0005A2BC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.InterruptMatchTarget(this.completeMatch);
			return 2;
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x0005C0EA File Offset: 0x0005A2EA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.completeMatch = true;
		}

		// Token: 0x04007187 RID: 29063
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007188 RID: 29064
		[Tooltip("CompleteMatch will make the gameobject match the target completely at the next frame")]
		public bool completeMatch = true;

		// Token: 0x04007189 RID: 29065
		private Animator animator;

		// Token: 0x0400718A RID: 29066
		private GameObject prevGameObject;
	}
}
