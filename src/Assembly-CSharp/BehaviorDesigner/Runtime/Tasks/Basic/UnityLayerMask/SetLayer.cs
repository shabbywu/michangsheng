using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLayerMask
{
	// Token: 0x02001122 RID: 4386
	[TaskCategory("Basic/LayerMask")]
	[TaskDescription("Sets the layer of a GameObject.")]
	public class SetLayer : Action
	{
		// Token: 0x0600753D RID: 30013 RVA: 0x002B428B File Offset: 0x002B248B
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).layer = LayerMask.NameToLayer(this.layerName.Value);
			return 2;
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x002B42B4 File Offset: 0x002B24B4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.layerName = "Default";
		}

		// Token: 0x040060E6 RID: 24806
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060E7 RID: 24807
		[Tooltip("The name of the layer to set")]
		public SharedString layerName = "Default";
	}
}
