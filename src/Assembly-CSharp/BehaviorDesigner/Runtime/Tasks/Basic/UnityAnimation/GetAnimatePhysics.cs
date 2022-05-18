using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166B RID: 5739
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Stores the animate physics value. Returns Success.")]
	public class GetAnimatePhysics : Action
	{
		// Token: 0x06008548 RID: 34120 RVA: 0x002D0B14 File Offset: 0x002CED14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x0005C770 File Offset: 0x0005A970
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.storeValue.Value = this.animation.animatePhysics;
			return 2;
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x0005C7A3 File Offset: 0x0005A9A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue.Value = false;
		}

		// Token: 0x04007208 RID: 29192
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007209 RID: 29193
		[Tooltip("Are the if animations are executed in the physics loop?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400720A RID: 29194
		private Animation animation;

		// Token: 0x0400720B RID: 29195
		private GameObject prevGameObject;
	}
}
