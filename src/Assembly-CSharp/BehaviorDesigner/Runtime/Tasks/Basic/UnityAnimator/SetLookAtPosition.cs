using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001660 RID: 5728
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the look at position. Returns Success.")]
	public class SetLookAtPosition : Action
	{
		// Token: 0x0600851A RID: 34074 RVA: 0x002D074C File Offset: 0x002CE94C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
			this.positionSet = false;
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x0005C406 File Offset: 0x0005A606
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.positionSet)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x0005C42D File Offset: 0x0005A62D
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtPosition(this.position.Value);
			this.positionSet = true;
		}

		// Token: 0x0600851D RID: 34077 RVA: 0x0005C45B File Offset: 0x0005A65B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x040071D2 RID: 29138
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071D3 RID: 29139
		[Tooltip("The position to lookAt")]
		public SharedVector3 position;

		// Token: 0x040071D4 RID: 29140
		private Animator animator;

		// Token: 0x040071D5 RID: 29141
		private GameObject prevGameObject;

		// Token: 0x040071D6 RID: 29142
		private bool positionSet;
	}
}
