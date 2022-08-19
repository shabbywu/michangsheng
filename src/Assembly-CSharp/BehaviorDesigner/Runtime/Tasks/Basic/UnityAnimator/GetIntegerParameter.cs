using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118F RID: 4495
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the integer parameter on an animator. Returns Success.")]
	public class GetIntegerParameter : Action
	{
		// Token: 0x060076D0 RID: 30416 RVA: 0x002B7660 File Offset: 0x002B5860
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x002B76A0 File Offset: 0x002B58A0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetInteger(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x002B76DE File Offset: 0x002B58DE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0;
		}

		// Token: 0x04006254 RID: 25172
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006255 RID: 25173
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04006256 RID: 25174
		[Tooltip("The value of the integer parameter")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04006257 RID: 25175
		private Animator animator;

		// Token: 0x04006258 RID: 25176
		private GameObject prevGameObject;
	}
}
