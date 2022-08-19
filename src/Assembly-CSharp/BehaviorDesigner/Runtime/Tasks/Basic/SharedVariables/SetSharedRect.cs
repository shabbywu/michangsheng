using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001068 RID: 4200
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedRect variable to the specified object. Returns Success.")]
	public class SetSharedRect : Action
	{
		// Token: 0x06007295 RID: 29333 RVA: 0x002AE335 File Offset: 0x002AC535
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x002AE350 File Offset: 0x002AC550
		public override void OnReset()
		{
			this.targetValue = default(Rect);
			this.targetVariable = default(Rect);
		}

		// Token: 0x04005E51 RID: 24145
		[Tooltip("The value to set the SharedRect to")]
		public SharedRect targetValue;

		// Token: 0x04005E52 RID: 24146
		[RequiredField]
		[Tooltip("The SharedRect to set")]
		public SharedRect targetVariable;
	}
}
