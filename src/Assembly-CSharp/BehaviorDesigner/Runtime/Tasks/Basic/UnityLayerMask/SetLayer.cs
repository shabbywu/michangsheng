using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLayerMask
{
	// Token: 0x020015E1 RID: 5601
	[TaskCategory("Basic/LayerMask")]
	[TaskDescription("Sets the layer of a GameObject.")]
	public class SetLayer : Action
	{
		// Token: 0x06008337 RID: 33591 RVA: 0x0005A2E4 File Offset: 0x000584E4
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).layer = LayerMask.NameToLayer(this.layerName.Value);
			return 2;
		}

		// Token: 0x06008338 RID: 33592 RVA: 0x0005A30D File Offset: 0x0005850D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.layerName = "Default";
		}

		// Token: 0x04007009 RID: 28681
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400700A RID: 28682
		[Tooltip("The name of the layer to set")]
		public SharedString layerName = "Default";
	}
}
