using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001060 RID: 4192
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedColor variable to the specified object. Returns Success.")]
	public class SetSharedColor : Action
	{
		// Token: 0x0600727D RID: 29309 RVA: 0x002AE16D File Offset: 0x002AC36D
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x002AE186 File Offset: 0x002AC386
		public override void OnReset()
		{
			this.targetValue = Color.black;
			this.targetVariable = Color.black;
		}

		// Token: 0x04005E40 RID: 24128
		[Tooltip("The value to set the SharedColor to")]
		public SharedColor targetValue;

		// Token: 0x04005E41 RID: 24129
		[RequiredField]
		[Tooltip("The SharedColor to set")]
		public SharedColor targetVariable;
	}
}
