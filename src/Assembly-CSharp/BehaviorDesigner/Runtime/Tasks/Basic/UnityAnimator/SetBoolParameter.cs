using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001659 RID: 5721
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
	public class SetBoolParameter : Action
	{
		// Token: 0x060084F5 RID: 34037 RVA: 0x002D03AC File Offset: 0x002CE5AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084F6 RID: 34038 RVA: 0x002D03EC File Offset: 0x002CE5EC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			bool @bool = this.animator.GetBool(this.hashID);
			this.animator.SetBool(this.hashID, this.boolValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@bool));
			}
			return 2;
		}

		// Token: 0x060084F7 RID: 34039 RVA: 0x0005C2A9 File Offset: 0x0005A4A9
		public IEnumerator ResetValue(bool origVale)
		{
			yield return null;
			this.animator.SetBool(this.hashID, origVale);
			yield break;
		}

		// Token: 0x060084F8 RID: 34040 RVA: 0x0005C2BF File Offset: 0x0005A4BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.boolValue = false;
		}

		// Token: 0x040071AC RID: 29100
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071AD RID: 29101
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040071AE RID: 29102
		[Tooltip("The value of the bool parameter")]
		public SharedBool boolValue;

		// Token: 0x040071AF RID: 29103
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x040071B0 RID: 29104
		private int hashID;

		// Token: 0x040071B1 RID: 29105
		private Animator animator;

		// Token: 0x040071B2 RID: 29106
		private GameObject prevGameObject;
	}
}
