using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001650 RID: 5712
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the playback speed of the animator. 1 is normal playback speed. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x060084D2 RID: 34002 RVA: 0x002D0080 File Offset: 0x002CE280
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x0005C034 File Offset: 0x0005A234
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.speed;
			return 2;
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x0005C067 File Offset: 0x0005A267
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007181 RID: 29057
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007182 RID: 29058
		[Tooltip("The playback speed of the Animator")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04007183 RID: 29059
		private Animator animator;

		// Token: 0x04007184 RID: 29060
		private GameObject prevGameObject;
	}
}
