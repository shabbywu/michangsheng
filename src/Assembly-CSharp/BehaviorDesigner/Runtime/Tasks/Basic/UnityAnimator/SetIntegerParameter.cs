using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200165D RID: 5725
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the int parameter on an animator. Returns Success.")]
	public class SetIntegerParameter : Action
	{
		// Token: 0x0600850B RID: 34059 RVA: 0x002D05EC File Offset: 0x002CE7EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600850C RID: 34060 RVA: 0x002D062C File Offset: 0x002CE82C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			int integer = this.animator.GetInteger(this.hashID);
			this.animator.SetInteger(this.hashID, this.intValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(integer));
			}
			return 2;
		}

		// Token: 0x0600850D RID: 34061 RVA: 0x0005C351 File Offset: 0x0005A551
		public IEnumerator ResetValue(int origVale)
		{
			yield return null;
			this.animator.SetInteger(this.hashID, origVale);
			yield break;
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x0005C367 File Offset: 0x0005A567
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.intValue = 0;
		}

		// Token: 0x040071C2 RID: 29122
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071C3 RID: 29123
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040071C4 RID: 29124
		[Tooltip("The value of the int parameter")]
		public SharedInt intValue;

		// Token: 0x040071C5 RID: 29125
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x040071C6 RID: 29126
		private int hashID;

		// Token: 0x040071C7 RID: 29127
		private Animator animator;

		// Token: 0x040071C8 RID: 29128
		private GameObject prevGameObject;
	}
}
