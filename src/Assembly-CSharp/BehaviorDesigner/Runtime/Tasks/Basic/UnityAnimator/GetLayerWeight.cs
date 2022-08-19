using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001190 RID: 4496
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the layer's weight. Returns Success.")]
	public class GetLayerWeight : Action
	{
		// Token: 0x060076D4 RID: 30420 RVA: 0x002B7704 File Offset: 0x002B5904
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x002B7744 File Offset: 0x002B5944
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

		// Token: 0x060076D6 RID: 30422 RVA: 0x002B7782 File Offset: 0x002B5982
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = 0f;
		}

		// Token: 0x04006259 RID: 25177
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400625A RID: 25178
		[Tooltip("The index of the layer")]
		public SharedInt index;

		// Token: 0x0400625B RID: 25179
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400625C RID: 25180
		private Animator animator;

		// Token: 0x0400625D RID: 25181
		private GameObject prevGameObject;
	}
}
