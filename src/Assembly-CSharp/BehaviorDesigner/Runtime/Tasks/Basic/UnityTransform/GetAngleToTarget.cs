using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200101F RID: 4127
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Gets the Angle between a GameObject's forward direction and a target. Returns Success.")]
	public class GetAngleToTarget : Action
	{
		// Token: 0x06007197 RID: 29079 RVA: 0x002AC1B0 File Offset: 0x002AA3B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x002AC1F0 File Offset: 0x002AA3F0
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

		// Token: 0x06007199 RID: 29081 RVA: 0x002AC2AC File Offset: 0x002AA4AC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetObject = null;
			this.targetPosition = Vector3.zero;
			this.ignoreHeight = true;
			this.storeValue = 0f;
		}

		// Token: 0x04005D6F RID: 23919
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D70 RID: 23920
		[Tooltip("The target object to measure the angle to. If null the targetPosition will be used.")]
		public SharedGameObject targetObject;

		// Token: 0x04005D71 RID: 23921
		[Tooltip("The world position to measure an angle to. If the targetObject is also not null, this value is used as an offset from that object's position.")]
		public SharedVector3 targetPosition;

		// Token: 0x04005D72 RID: 23922
		[Tooltip("Ignore height differences when calculating the angle?")]
		public SharedBool ignoreHeight = true;

		// Token: 0x04005D73 RID: 23923
		[Tooltip("The angle to the target")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005D74 RID: 23924
		private Transform targetTransform;

		// Token: 0x04005D75 RID: 23925
		private GameObject prevGameObject;
	}
}
