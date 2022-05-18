using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x02001619 RID: 5657
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the height of the CapsuleCollider. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x060083F9 RID: 33785 RVA: 0x002CF1E0 File Offset: 0x002CD3E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083FA RID: 33786 RVA: 0x0005B06E File Offset: 0x0005926E
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return 1;
			}
			this.capsuleCollider.height = this.direction.Value;
			return 2;
		}

		// Token: 0x060083FB RID: 33787 RVA: 0x0005B0A1 File Offset: 0x000592A1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0f;
		}

		// Token: 0x040070A8 RID: 28840
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070A9 RID: 28841
		[Tooltip("The height of the CapsuleCollider")]
		public SharedFloat direction;

		// Token: 0x040070AA RID: 28842
		private CapsuleCollider capsuleCollider;

		// Token: 0x040070AB RID: 28843
		private GameObject prevGameObject;
	}
}
