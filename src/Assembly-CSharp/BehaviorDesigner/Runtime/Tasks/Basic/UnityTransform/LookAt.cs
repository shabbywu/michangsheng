using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102E RID: 4142
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Rotates the transform so the forward vector points at worldPosition. Returns Success.")]
	public class LookAt : Action
	{
		// Token: 0x060071D3 RID: 29139 RVA: 0x002ACAA4 File Offset: 0x002AACA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x002ACAE4 File Offset: 0x002AACE4
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			if (this.targetLookAt.Value != null)
			{
				this.targetTransform.LookAt(this.worldPosition.Value, this.worldUp);
			}
			else
			{
				this.targetTransform.LookAt(this.targetLookAt.Value.transform);
			}
			return 2;
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x002ACB58 File Offset: 0x002AAD58
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetLookAt = null;
			this.worldPosition = Vector3.up;
			this.worldUp = Vector3.up;
		}

		// Token: 0x04005DAF RID: 23983
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DB0 RID: 23984
		[Tooltip("The GameObject to look at. If null the world position will be used.")]
		public SharedGameObject targetLookAt;

		// Token: 0x04005DB1 RID: 23985
		[Tooltip("Point to look at")]
		public SharedVector3 worldPosition;

		// Token: 0x04005DB2 RID: 23986
		[Tooltip("Vector specifying the upward direction")]
		public Vector3 worldUp;

		// Token: 0x04005DB3 RID: 23987
		private Transform targetTransform;

		// Token: 0x04005DB4 RID: 23988
		private GameObject prevGameObject;
	}
}
