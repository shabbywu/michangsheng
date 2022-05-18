using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164C RID: 5708
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the float parameter on an animator. Returns Success.")]
	public class GetFloatParameter : Action
	{
		// Token: 0x060084C2 RID: 33986 RVA: 0x002CFF80 File Offset: 0x002CE180
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084C3 RID: 33987 RVA: 0x0005BEBB File Offset: 0x0005A0BB
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetFloat(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x0005BEF9 File Offset: 0x0005A0F9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0f;
		}

		// Token: 0x0400716E RID: 29038
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400716F RID: 29039
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04007170 RID: 29040
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04007171 RID: 29041
		private Animator animator;

		// Token: 0x04007172 RID: 29042
		private GameObject prevGameObject;
	}
}
