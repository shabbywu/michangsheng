using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001649 RID: 5705
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the bool parameter on an animator. Returns Success.")]
	public class GetBoolParameter : Action
	{
		// Token: 0x060084B6 RID: 33974 RVA: 0x002CFEC0 File Offset: 0x002CE0C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084B7 RID: 33975 RVA: 0x0005BDBF File Offset: 0x00059FBF
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.GetBool(this.paramaterName.Value);
			return 2;
		}

		// Token: 0x060084B8 RID: 33976 RVA: 0x0005BDFD File Offset: 0x00059FFD
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = false;
		}

		// Token: 0x04007161 RID: 29025
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007162 RID: 29026
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04007163 RID: 29027
		[Tooltip("The value of the bool parameter")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04007164 RID: 29028
		private Animator animator;

		// Token: 0x04007165 RID: 29029
		private GameObject prevGameObject;
	}
}
