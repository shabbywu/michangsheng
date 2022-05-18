using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001665 RID: 5733
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the animator in recording mode. Returns Success.")]
	public class StartRecording : Action
	{
		// Token: 0x06008530 RID: 34096 RVA: 0x002D0940 File Offset: 0x002CEB40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008531 RID: 34097 RVA: 0x0005C582 File Offset: 0x0005A782
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

		// Token: 0x06008532 RID: 34098 RVA: 0x0005C5B0 File Offset: 0x0005A7B0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.frameCount = 0;
		}

		// Token: 0x040071EB RID: 29163
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071EC RID: 29164
		[Tooltip("The number of frames (updates) that will be recorded")]
		public int frameCount;

		// Token: 0x040071ED RID: 29165
		private Animator animator;

		// Token: 0x040071EE RID: 29166
		private GameObject prevGameObject;
	}
}
