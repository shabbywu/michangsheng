using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x0200161E RID: 5662
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Stores the size of the BoxCollider. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x0600840D RID: 33805 RVA: 0x002CF320 File Offset: 0x002CD520
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600840E RID: 33806 RVA: 0x0005B1EA File Offset: 0x000593EA
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

		// Token: 0x0600840F RID: 33807 RVA: 0x0005B21D File Offset: 0x0005941D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040070BC RID: 28860
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070BD RID: 28861
		[Tooltip("The size of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040070BE RID: 28862
		private BoxCollider boxCollider;

		// Token: 0x040070BF RID: 28863
		private GameObject prevGameObject;
	}
}
