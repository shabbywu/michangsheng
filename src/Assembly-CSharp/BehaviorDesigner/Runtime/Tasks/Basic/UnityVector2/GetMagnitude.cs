using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CA RID: 5322
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the magnitude of the Vector2.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06007F66 RID: 32614 RVA: 0x002CA058 File Offset: 0x002C8258
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.magnitude;
			return 2;
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x0005656E File Offset: 0x0005476E
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04006C45 RID: 27717
		[Tooltip("The Vector2 to get the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C46 RID: 27718
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
