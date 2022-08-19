using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001196 RID: 4502
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified parameter is controlled by an additional curve on an animation.")]
	public class IsParameterControlledByCurve : Conditional
	{
		// Token: 0x060076EB RID: 30443 RVA: 0x002B7A48 File Offset: 0x002B5C48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076EC RID: 30444 RVA: 0x002B7A88 File Offset: 0x002B5C88
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

		// Token: 0x060076ED RID: 30445 RVA: 0x002B7ABF File Offset: 0x002B5CBF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
		}

		// Token: 0x04006271 RID: 25201
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006272 RID: 25202
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04006273 RID: 25203
		private Animator animator;

		// Token: 0x04006274 RID: 25204
		private GameObject prevGameObject;
	}
}
