using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x0200161D RID: 5661
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Stores the center of the BoxCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06008409 RID: 33801 RVA: 0x002CF2E0 File Offset: 0x002CD4E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x0005B19E File Offset: 0x0005939E
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return 1;
			}
			this.storeValue.Value = this.boxCollider.center;
			return 2;
		}

		// Token: 0x0600840B RID: 33803 RVA: 0x0005B1D1 File Offset: 0x000593D1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040070B8 RID: 28856
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070B9 RID: 28857
		[Tooltip("The center of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040070BA RID: 28858
		private BoxCollider boxCollider;

		// Token: 0x040070BB RID: 28859
		private GameObject prevGameObject;
	}
}
