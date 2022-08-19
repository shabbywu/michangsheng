using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCapsuleCollider
{
	// Token: 0x0200115A RID: 4442
	[TaskCategory("Basic/CapsuleCollider")]
	[TaskDescription("Sets the height of the CapsuleCollider. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x060075FF RID: 30207 RVA: 0x002B591C File Offset: 0x002B3B1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007600 RID: 30208 RVA: 0x002B595C File Offset: 0x002B3B5C
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

		// Token: 0x06007601 RID: 30209 RVA: 0x002B598F File Offset: 0x002B3B8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0f;
		}

		// Token: 0x04006185 RID: 24965
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006186 RID: 24966
		[Tooltip("The height of the CapsuleCollider")]
		public SharedFloat direction;

		// Token: 0x04006187 RID: 24967
		private CapsuleCollider capsuleCollider;

		// Token: 0x04006188 RID: 24968
		private GameObject prevGameObject;
	}
}
