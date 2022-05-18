using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200164E RID: 5710
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the integer parameter on an animator. Returns Success.")]
	public class GetIntegerParameter : Action
	{
		// Token: 0x060084CA RID: 33994 RVA: 0x002D0000 File Offset: 0x002CE200
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x0005BF6E File Offset: 0x0005A16E
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

		// Token: 0x060084CC RID: 33996 RVA: 0x0005BFAC File Offset: 0x0005A1AC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0;
		}

		// Token: 0x04007177 RID: 29047
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007178 RID: 29048
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04007179 RID: 29049
		[Tooltip("The value of the integer parameter")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x0400717A RID: 29050
		private Animator animator;

		// Token: 0x0400717B RID: 29051
		private GameObject prevGameObject;
	}
}
