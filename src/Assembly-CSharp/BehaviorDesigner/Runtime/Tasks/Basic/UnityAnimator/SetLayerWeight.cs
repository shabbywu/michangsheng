using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200165F RID: 5727
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the layer's current weight. Returns Success.")]
	public class SetLayerWeight : Action
	{
		// Token: 0x06008516 RID: 34070 RVA: 0x002D070C File Offset: 0x002CE90C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x0005C3A3 File Offset: 0x0005A5A3
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.SetLayerWeight(this.index.Value, this.weight.Value);
			return 2;
		}

		// Token: 0x06008518 RID: 34072 RVA: 0x0005C3E1 File Offset: 0x0005A5E1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.weight = 0f;
		}

		// Token: 0x040071CD RID: 29133
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071CE RID: 29134
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x040071CF RID: 29135
		[Tooltip("The weight of the layer")]
		public SharedFloat weight;

		// Token: 0x040071D0 RID: 29136
		private Animator animator;

		// Token: 0x040071D1 RID: 29137
		private GameObject prevGameObject;
	}
}
