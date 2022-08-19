using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x0200115F RID: 4447
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Stores the size of the BoxCollider. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06007613 RID: 30227 RVA: 0x002B5BD8 File Offset: 0x002B3DD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x002B5C18 File Offset: 0x002B3E18
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return 1;
			}
			this.storeValue.Value = this.boxCollider.size;
			return 2;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x002B5C4B File Offset: 0x002B3E4B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006199 RID: 24985
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400619A RID: 24986
		[Tooltip("The size of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400619B RID: 24987
		private BoxCollider boxCollider;

		// Token: 0x0400619C RID: 24988
		private GameObject prevGameObject;
	}
}
