using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001191 RID: 4497
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the playback speed of the animator. 1 is normal playback speed. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x060076D8 RID: 30424 RVA: 0x002B77A8 File Offset: 0x002B59A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x002B77E8 File Offset: 0x002B59E8
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.speed;
			return 2;
		}

		// Token: 0x060076DA RID: 30426 RVA: 0x002B781B File Offset: 0x002B5A1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400625E RID: 25182
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400625F RID: 25183
		[Tooltip("The playback speed of the Animator")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006260 RID: 25184
		private Animator animator;

		// Token: 0x04006261 RID: 25185
		private GameObject prevGameObject;
	}
}
