using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001157 RID: 4439
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Stores the radius of the CapsuleCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x060075F3 RID: 30195 RVA: 0x002B577C File Offset: 0x002B397C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x002B57BC File Offset: 0x002B39BC
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.storeValue.Value = this.capsuleCollider.radius;
			return 2;
		}

		// Token: 0x060075F5 RID: 30197 RVA: 0x002B57EF File Offset: 0x002B39EF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006179 RID: 24953
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400617A RID: 24954
		[Tooltip("The radius of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400617B RID: 24955
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400617C RID: 24956
		private GameObject prevGameObject;
	}
}
