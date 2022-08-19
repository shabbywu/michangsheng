using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119C RID: 4508
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the int parameter on an animator. Returns Success.")]
	public class SetIntegerParameter : Action
	{
		// Token: 0x06007705 RID: 30469 RVA: 0x002B7F50 File Offset: 0x002B6150
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x002B7F90 File Offset: 0x002B6190
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

		// Token: 0x06007707 RID: 30471 RVA: 0x002B8012 File Offset: 0x002B6212
		public IEnumerator ResetValue(int origVale)
		{
			yield return null;
			this.animator.SetInteger(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x002B8028 File Offset: 0x002B6228
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.intValue = 0;
		}

		// Token: 0x04006297 RID: 25239
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006298 RID: 25240
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04006299 RID: 25241
		[Tooltip("The value of the int parameter")]
		public SharedInt intValue;

		// Token: 0x0400629A RID: 25242
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x0400629B RID: 25243
		private int hashID;

		// Token: 0x0400629C RID: 25244
		private Animator animator;

		// Token: 0x0400629D RID: 25245
		private GameObject prevGameObject;
	}
}
