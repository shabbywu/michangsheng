using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001671 RID: 5745
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Sets animate physics to the specified value. Returns Success.")]
	public class SetAnimatePhysics : Action
	{
		// Token: 0x06008560 RID: 34144 RVA: 0x002D0DC4 File Offset: 0x002CEFC4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008561 RID: 34145 RVA: 0x0005C8A2 File Offset: 0x0005AAA2
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.animatePhysics = this.animatePhysics.Value;
			return 2;
		}

		// Token: 0x06008562 RID: 34146 RVA: 0x0005C8D5 File Offset: 0x0005AAD5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animatePhysics.Value = false;
		}

		// Token: 0x04007222 RID: 29218
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007223 RID: 29219
		[Tooltip("Are animations executed in the physics loop?")]
		public SharedBool animatePhysics;

		// Token: 0x04007224 RID: 29220
		private Animation animation;

		// Token: 0x04007225 RID: 29221
		private GameObject prevGameObject;
	}
}
