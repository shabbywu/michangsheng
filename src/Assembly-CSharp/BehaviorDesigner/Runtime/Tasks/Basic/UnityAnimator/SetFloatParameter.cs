using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200165B RID: 5723
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the float parameter on an animator. Returns Success.")]
	public class SetFloatParameter : Action
	{
		// Token: 0x06008500 RID: 34048 RVA: 0x002D04CC File Offset: 0x002CE6CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x002D050C File Offset: 0x002CE70C
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

		// Token: 0x06008502 RID: 34050 RVA: 0x0005C2FB File Offset: 0x0005A4FB
		public IEnumerator ResetValue(float origVale)
		{
			yield return null;
			this.animator.SetFloat(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x0005C311 File Offset: 0x0005A511
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.floatValue = 0f;
		}

		// Token: 0x040071B7 RID: 29111
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071B8 RID: 29112
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040071B9 RID: 29113
		[Tooltip("The value of the float parameter")]
		public SharedFloat floatValue;

		// Token: 0x040071BA RID: 29114
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x040071BB RID: 29115
		private int hashID;

		// Token: 0x040071BC RID: 29116
		private Animator animator;

		// Token: 0x040071BD RID: 29117
		private GameObject prevGameObject;
	}
}
