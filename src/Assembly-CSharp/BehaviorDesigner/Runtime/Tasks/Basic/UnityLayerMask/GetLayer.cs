using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLayerMask
{
	// Token: 0x02001121 RID: 4385
	[TaskCategory("Basic/LayerMask")]
	[TaskDescription("Gets the layer of a GameObject.")]
	public class GetLayer : Action
	{
		// Token: 0x0600753A RID: 30010 RVA: 0x002B423C File Offset: 0x002B243C
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			this.storeResult.Value = LayerMask.LayerToName(defaultGameObject.layer);
			return 2;
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x002B4272 File Offset: 0x002B2472
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = "";
		}

		// Token: 0x040060E4 RID: 24804
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060E5 RID: 24805
		[Tooltip("The name of the layer to get")]
		[RequiredField]
		public SharedString storeResult;
	}
}
