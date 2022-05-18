using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001522 RID: 5410
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedRect variable to the specified object. Returns Success.")]
	public class SetSharedRect : Action
	{
		// Token: 0x0600808F RID: 32911 RVA: 0x00057793 File Offset: 0x00055993
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x002CB524 File Offset: 0x002C9724
		public override void OnReset()
		{
			this.targetValue = default(Rect);
			this.targetVariable = default(Rect);
		}

		// Token: 0x04006D51 RID: 27985
		[Tooltip("The value to set the SharedRect to")]
		public SharedRect targetValue;

		// Token: 0x04006D52 RID: 27986
		[RequiredField]
		[Tooltip("The SharedRect to set")]
		public SharedRect targetVariable;
	}
}
