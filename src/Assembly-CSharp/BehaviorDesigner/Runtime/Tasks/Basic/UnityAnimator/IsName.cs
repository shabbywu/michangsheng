using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001654 RID: 5716
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified name matches the name of the active state.")]
	public class IsName : Conditional
	{
		// Token: 0x060084E1 RID: 34017 RVA: 0x002D0140 File Offset: 0x002CE340
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x002D0180 File Offset: 0x002CE380
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

		// Token: 0x060084E3 RID: 34019 RVA: 0x0005C155 File Offset: 0x0005A355
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.name = "";
		}

		// Token: 0x0400718F RID: 29071
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007190 RID: 29072
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x04007191 RID: 29073
		[Tooltip("The state name to compare")]
		public SharedString name;

		// Token: 0x04007192 RID: 29074
		private Animator animator;

		// Token: 0x04007193 RID: 29075
		private GameObject prevGameObject;
	}
}
