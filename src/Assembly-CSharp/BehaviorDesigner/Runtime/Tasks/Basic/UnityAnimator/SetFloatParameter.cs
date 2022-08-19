using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119B RID: 4507
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the float parameter on an animator. Returns Success.")]
	public class SetFloatParameter : Action
	{
		// Token: 0x06007700 RID: 30464 RVA: 0x002B7E4C File Offset: 0x002B604C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x002B7E8C File Offset: 0x002B608C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			float @float = this.animator.GetFloat(this.hashID);
			this.animator.SetFloat(this.hashID, this.floatValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@float));
			}
			return 2;
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x002B7F0E File Offset: 0x002B610E
		public IEnumerator ResetValue(float origVale)
		{
			yield return null;
			this.animator.SetFloat(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06007703 RID: 30467 RVA: 0x002B7F24 File Offset: 0x002B6124
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.floatValue = 0f;
		}

		// Token: 0x04006290 RID: 25232
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006291 RID: 25233
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04006292 RID: 25234
		[Tooltip("The value of the float parameter")]
		public SharedFloat floatValue;

		// Token: 0x04006293 RID: 25235
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04006294 RID: 25236
		private int hashID;

		// Token: 0x04006295 RID: 25237
		private Animator animator;

		// Token: 0x04006296 RID: 25238
		private GameObject prevGameObject;
	}
}
