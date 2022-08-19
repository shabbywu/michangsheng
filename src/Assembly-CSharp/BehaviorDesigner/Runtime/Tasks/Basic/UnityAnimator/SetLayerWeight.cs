using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119D RID: 4509
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the layer's current weight. Returns Success.")]
	public class SetLayerWeight : Action
	{
		// Token: 0x0600770A RID: 30474 RVA: 0x002B8050 File Offset: 0x002B6250
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600770B RID: 30475 RVA: 0x002B8090 File Offset: 0x002B6290
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.SetLayerWeight(this.index.Value, this.weight.Value);
			return 2;
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x002B80CE File Offset: 0x002B62CE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.weight = 0f;
		}

		// Token: 0x0400629E RID: 25246
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400629F RID: 25247
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x040062A0 RID: 25248
		[Tooltip("The weight of the layer")]
		public SharedFloat weight;

		// Token: 0x040062A1 RID: 25249
		private Animator animator;

		// Token: 0x040062A2 RID: 25250
		private GameObject prevGameObject;
	}
}
