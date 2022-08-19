using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200101E RID: 4126
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Finds a transform by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x06007193 RID: 29075 RVA: 0x002AC118 File Offset: 0x002AA318
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x002AC158 File Offset: 0x002AA358
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.Find(this.transformName.Value);
			return 2;
		}

		// Token: 0x06007195 RID: 29077 RVA: 0x002AC196 File Offset: 0x002AA396
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
			this.storeValue = null;
		}

		// Token: 0x04005D6A RID: 23914
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D6B RID: 23915
		[Tooltip("The transform name to find")]
		public SharedString transformName;

		// Token: 0x04005D6C RID: 23916
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x04005D6D RID: 23917
		private Transform targetTransform;

		// Token: 0x04005D6E RID: 23918
		private GameObject prevGameObject;
	}
}
