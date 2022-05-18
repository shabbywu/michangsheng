using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001663 RID: 5731
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets a trigger parameter to active or inactive. Returns Success.")]
	public class SetTrigger : Action
	{
		// Token: 0x06008528 RID: 34088 RVA: 0x002D08C0 File Offset: 0x002CEAC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x0005C505 File Offset: 0x0005A705
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

		// Token: 0x0600852A RID: 34090 RVA: 0x0005C538 File Offset: 0x0005A738
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName.Value = "";
		}

		// Token: 0x040071E4 RID: 29156
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071E5 RID: 29157
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040071E6 RID: 29158
		private Animator animator;

		// Token: 0x040071E7 RID: 29159
		private GameObject prevGameObject;
	}
}
