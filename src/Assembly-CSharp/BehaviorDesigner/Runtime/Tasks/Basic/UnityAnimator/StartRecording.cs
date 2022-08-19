using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A3 RID: 4515
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the animator in recording mode. Returns Success.")]
	public class StartRecording : Action
	{
		// Token: 0x06007724 RID: 30500 RVA: 0x002B846C File Offset: 0x002B666C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x002B84AC File Offset: 0x002B66AC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.StartRecording(this.frameCount);
			return 2;
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x002B84DA File Offset: 0x002B66DA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.frameCount = 0;
		}

		// Token: 0x040062BC RID: 25276
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062BD RID: 25277
		[Tooltip("The number of frames (updates) that will be recorded")]
		public int frameCount;

		// Token: 0x040062BE RID: 25278
		private Animator animator;

		// Token: 0x040062BF RID: 25279
		private GameObject prevGameObject;
	}
}
