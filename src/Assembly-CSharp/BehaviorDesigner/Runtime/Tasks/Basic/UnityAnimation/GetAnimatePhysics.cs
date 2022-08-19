using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011A9 RID: 4521
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Stores the animate physics value. Returns Success.")]
	public class GetAnimatePhysics : Action
	{
		// Token: 0x0600773C RID: 30524 RVA: 0x002B8838 File Offset: 0x002B6A38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x002B8878 File Offset: 0x002B6A78
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

		// Token: 0x0600773E RID: 30526 RVA: 0x002B88AB File Offset: 0x002B6AAB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue.Value = false;
		}

		// Token: 0x040062D9 RID: 25305
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062DA RID: 25306
		[Tooltip("Are the if animations are executed in the physics loop?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040062DB RID: 25307
		private Animation animation;

		// Token: 0x040062DC RID: 25308
		private GameObject prevGameObject;
	}
}
