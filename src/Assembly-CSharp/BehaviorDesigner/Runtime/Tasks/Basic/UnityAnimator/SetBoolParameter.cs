using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119A RID: 4506
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
	public class SetBoolParameter : Action
	{
		// Token: 0x060076FB RID: 30459 RVA: 0x002B7D4C File Offset: 0x002B5F4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x002B7D8C File Offset: 0x002B5F8C
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

		// Token: 0x060076FD RID: 30461 RVA: 0x002B7E0E File Offset: 0x002B600E
		public IEnumerator ResetValue(bool origVale)
		{
			yield return null;
			this.animator.SetBool(this.hashID, origVale);
			yield break;
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x002B7E24 File Offset: 0x002B6024
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.boolValue = false;
		}

		// Token: 0x04006289 RID: 25225
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400628A RID: 25226
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400628B RID: 25227
		[Tooltip("The value of the bool parameter")]
		public SharedBool boolValue;

		// Token: 0x0400628C RID: 25228
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x0400628D RID: 25229
		private int hashID;

		// Token: 0x0400628E RID: 25230
		private Animator animator;

		// Token: 0x0400628F RID: 25231
		private GameObject prevGameObject;
	}
}
