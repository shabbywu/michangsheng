using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001657 RID: 5719
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Plays an animator state. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x060084ED RID: 34029 RVA: 0x002D032C File Offset: 0x002CE52C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x0005C1DD File Offset: 0x0005A3DD
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.Play(this.stateName.Value, this.layer, this.normalizedTime);
			return 2;
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x0005C21C File Offset: 0x0005A41C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x040071A2 RID: 29090
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071A3 RID: 29091
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x040071A4 RID: 29092
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x040071A5 RID: 29093
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x040071A6 RID: 29094
		private Animator animator;

		// Token: 0x040071A7 RID: 29095
		private GameObject prevGameObject;
	}
}
