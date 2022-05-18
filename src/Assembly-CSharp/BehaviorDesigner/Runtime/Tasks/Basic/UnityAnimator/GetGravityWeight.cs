using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164D RID: 5709
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the current gravity weight based on current animations that are played. Returns Success.")]
	public class GetGravityWeight : Action
	{
		// Token: 0x060084C6 RID: 33990 RVA: 0x002CFFC0 File Offset: 0x002CE1C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084C7 RID: 33991 RVA: 0x0005BF22 File Offset: 0x0005A122
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.gravityWeight;
			return 2;
		}

		// Token: 0x060084C8 RID: 33992 RVA: 0x0005BF55 File Offset: 0x0005A155
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007173 RID: 29043
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007174 RID: 29044
		[Tooltip("The value of the gravity weight")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04007175 RID: 29045
		private Animator animator;

		// Token: 0x04007176 RID: 29046
		private GameObject prevGameObject;
	}
}
