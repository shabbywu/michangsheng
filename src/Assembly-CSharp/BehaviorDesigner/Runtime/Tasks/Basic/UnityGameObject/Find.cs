using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F5 RID: 5621
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x06008373 RID: 33651 RVA: 0x0005A65A File Offset: 0x0005885A
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.Find(this.gameObjectName.Value);
			return 2;
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x0005A678 File Offset: 0x00058878
		public override void OnReset()
		{
			this.gameObjectName = null;
			this.storeValue = null;
		}

		// Token: 0x04007028 RID: 28712
		[Tooltip("The GameObject name to find")]
		public SharedString gameObjectName;

		// Token: 0x04007029 RID: 28713
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
