using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001136 RID: 4406
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x06007579 RID: 30073 RVA: 0x002B46D3 File Offset: 0x002B28D3
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.Find(this.gameObjectName.Value);
			return 2;
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x002B46F1 File Offset: 0x002B28F1
		public override void OnReset()
		{
			this.gameObjectName = null;
			this.storeValue = null;
		}

		// Token: 0x04006105 RID: 24837
		[Tooltip("The GameObject name to find")]
		public SharedString gameObjectName;

		// Token: 0x04006106 RID: 24838
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
