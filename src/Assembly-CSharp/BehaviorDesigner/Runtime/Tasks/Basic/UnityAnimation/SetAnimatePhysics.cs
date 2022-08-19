using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AF RID: 4527
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Sets animate physics to the specified value. Returns Success.")]
	public class SetAnimatePhysics : Action
	{
		// Token: 0x06007754 RID: 30548 RVA: 0x002B8C1C File Offset: 0x002B6E1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x002B8C5C File Offset: 0x002B6E5C
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

		// Token: 0x06007756 RID: 30550 RVA: 0x002B8C8F File Offset: 0x002B6E8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animatePhysics.Value = false;
		}

		// Token: 0x040062F3 RID: 25331
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062F4 RID: 25332
		[Tooltip("Are animations executed in the physics loop?")]
		public SharedBool animatePhysics;

		// Token: 0x040062F5 RID: 25333
		private Animation animation;

		// Token: 0x040062F6 RID: 25334
		private GameObject prevGameObject;
	}
}
