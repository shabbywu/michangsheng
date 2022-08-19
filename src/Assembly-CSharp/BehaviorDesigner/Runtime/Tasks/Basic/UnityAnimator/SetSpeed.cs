using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A0 RID: 4512
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the playback speed of the Animator. 1 is normal playback speed. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x06007718 RID: 30488 RVA: 0x002B82E0 File Offset: 0x002B64E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x002B8320 File Offset: 0x002B6520
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.speed = this.speed.Value;
			return 2;
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x002B8353 File Offset: 0x002B6553
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x040062B1 RID: 25265
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062B2 RID: 25266
		[Tooltip("The playback speed of the Animator")]
		public SharedFloat speed;

		// Token: 0x040062B3 RID: 25267
		private Animator animator;

		// Token: 0x040062B4 RID: 25268
		private GameObject prevGameObject;
	}
}
