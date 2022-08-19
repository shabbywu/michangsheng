using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A1 RID: 4513
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets a trigger parameter to active or inactive. Returns Success.")]
	public class SetTrigger : Action
	{
		// Token: 0x0600771C RID: 30492 RVA: 0x002B836C File Offset: 0x002B656C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x002B83AC File Offset: 0x002B65AC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.SetTrigger(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x002B83DF File Offset: 0x002B65DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName.Value = "";
		}

		// Token: 0x040062B5 RID: 25269
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062B6 RID: 25270
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040062B7 RID: 25271
		private Animator animator;

		// Token: 0x040062B8 RID: 25272
		private GameObject prevGameObject;
	}
}
