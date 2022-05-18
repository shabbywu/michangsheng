using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164F RID: 5711
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the layer's weight. Returns Success.")]
	public class GetLayerWeight : Action
	{
		// Token: 0x060084CE RID: 33998 RVA: 0x002D0040 File Offset: 0x002CE240
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x0005BFD1 File Offset: 0x0005A1D1
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetLayerWeight(this.index.Value);
			return 2;
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x0005C00F File Offset: 0x0005A20F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = 0f;
		}

		// Token: 0x0400717C RID: 29052
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400717D RID: 29053
		[Tooltip("The index of the layer")]
		public SharedInt index;

		// Token: 0x0400717E RID: 29054
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400717F RID: 29055
		private Animator animator;

		// Token: 0x04007180 RID: 29056
		private GameObject prevGameObject;
	}
}
