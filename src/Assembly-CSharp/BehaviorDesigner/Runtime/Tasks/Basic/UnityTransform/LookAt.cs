using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E8 RID: 5352
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Rotates the transform so the forward vector points at worldPosition. Returns Success.")]
	public class LookAt : Action
	{
		// Token: 0x06007FCD RID: 32717 RVA: 0x002CA84C File Offset: 0x002C8A4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x002CA88C File Offset: 0x002C8A8C
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

		// Token: 0x06007FCF RID: 32719 RVA: 0x00056BEE File Offset: 0x00054DEE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetLookAt = null;
			this.worldPosition = Vector3.up;
			this.worldUp = Vector3.up;
		}

		// Token: 0x04006CAF RID: 27823
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CB0 RID: 27824
		[Tooltip("The GameObject to look at. If null the world position will be used.")]
		public SharedGameObject targetLookAt;

		// Token: 0x04006CB1 RID: 27825
		[Tooltip("Point to look at")]
		public SharedVector3 worldPosition;

		// Token: 0x04006CB2 RID: 27826
		[Tooltip("Vector specifying the upward direction")]
		public Vector3 worldUp;

		// Token: 0x04006CB3 RID: 27827
		private Transform targetTransform;

		// Token: 0x04006CB4 RID: 27828
		private GameObject prevGameObject;
	}
}
