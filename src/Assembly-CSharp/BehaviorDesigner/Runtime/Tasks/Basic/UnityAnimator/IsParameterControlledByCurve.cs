using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001655 RID: 5717
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified parameter is controlled by an additional curve on an animation.")]
	public class IsParameterControlledByCurve : Conditional
	{
		// Token: 0x060084E5 RID: 34021 RVA: 0x002D01D8 File Offset: 0x002CE3D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x0005C17A File Offset: 0x0005A37A
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.animator.IsParameterControlledByCurve(this.paramaterName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060084E7 RID: 34023 RVA: 0x0005C1B1 File Offset: 0x0005A3B1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
		}

		// Token: 0x04007194 RID: 29076
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007195 RID: 29077
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04007196 RID: 29078
		private Animator animator;

		// Token: 0x04007197 RID: 29079
		private GameObject prevGameObject;
	}
}
