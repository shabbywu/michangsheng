using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLayerMask
{
	// Token: 0x020015E0 RID: 5600
	[TaskCategory("Basic/LayerMask")]
	[TaskDescription("Gets the layer of a GameObject.")]
	public class GetLayer : Action
	{
		// Token: 0x06008334 RID: 33588 RVA: 0x002CE8AC File Offset: 0x002CCAAC
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			this.storeResult.Value = LayerMask.LayerToName(defaultGameObject.layer);
			return 2;
		}

		// Token: 0x06008335 RID: 33589 RVA: 0x0005A2CB File Offset: 0x000584CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = "";
		}

		// Token: 0x04007007 RID: 28679
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007008 RID: 28680
		[Tooltip("The name of the layer to get")]
		[RequiredField]
		public SharedString storeResult;
	}
}
