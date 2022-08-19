using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001195 RID: 4501
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified name matches the name of the active state.")]
	public class IsName : Conditional
	{
		// Token: 0x060076E7 RID: 30439 RVA: 0x002B798C File Offset: 0x002B5B8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076E8 RID: 30440 RVA: 0x002B79CC File Offset: 0x002B5BCC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.animator.GetCurrentAnimatorStateInfo(this.index.Value).IsName(this.name.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060076E9 RID: 30441 RVA: 0x002B7A21 File Offset: 0x002B5C21
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.name = "";
		}

		// Token: 0x0400626C RID: 25196
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400626D RID: 25197
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x0400626E RID: 25198
		[Tooltip("The state name to compare")]
		public SharedString name;

		// Token: 0x0400626F RID: 25199
		private Animator animator;

		// Token: 0x04006270 RID: 25200
		private GameObject prevGameObject;
	}
}
