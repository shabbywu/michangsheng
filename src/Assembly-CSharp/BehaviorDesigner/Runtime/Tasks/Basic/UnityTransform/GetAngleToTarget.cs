using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014D9 RID: 5337
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Gets the Angle between a GameObject's forward direction and a target. Returns Success.")]
	public class GetAngleToTarget : Action
	{
		// Token: 0x06007F91 RID: 32657 RVA: 0x002CA3D0 File Offset: 0x002C85D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x002CA410 File Offset: 0x002C8610
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			Vector3 vector;
			if (this.targetObject.Value != null)
			{
				vector = this.targetObject.Value.transform.InverseTransformPoint(this.targetPosition.Value);
			}
			else
			{
				vector = this.targetPosition.Value;
			}
			if (this.ignoreHeight.Value)
			{
				vector.y = this.targetTransform.position.y;
			}
			Vector3 vector2 = vector - this.targetTransform.position;
			this.storeValue.Value = Vector3.Angle(vector2, this.targetTransform.forward);
			return 2;
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x0005677A File Offset: 0x0005497A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetObject = null;
			this.targetPosition = Vector3.zero;
			this.ignoreHeight = true;
			this.storeValue = 0f;
		}

		// Token: 0x04006C6F RID: 27759
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C70 RID: 27760
		[Tooltip("The target object to measure the angle to. If null the targetPosition will be used.")]
		public SharedGameObject targetObject;

		// Token: 0x04006C71 RID: 27761
		[Tooltip("The world position to measure an angle to. If the targetObject is also not null, this value is used as an offset from that object's position.")]
		public SharedVector3 targetPosition;

		// Token: 0x04006C72 RID: 27762
		[Tooltip("Ignore height differences when calculating the angle?")]
		public SharedBool ignoreHeight = true;

		// Token: 0x04006C73 RID: 27763
		[Tooltip("The angle to the target")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006C74 RID: 27764
		private Transform targetTransform;

		// Token: 0x04006C75 RID: 27765
		private GameObject prevGameObject;
	}
}
