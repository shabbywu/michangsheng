using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200151A RID: 5402
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedColor variable to the specified object. Returns Success.")]
	public class SetSharedColor : Action
	{
		// Token: 0x06008077 RID: 32887 RVA: 0x00057618 File Offset: 0x00055818
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x00057631 File Offset: 0x00055831
		public override void OnReset()
		{
			this.targetValue = Color.black;
			this.targetVariable = Color.black;
		}

		// Token: 0x04006D40 RID: 27968
		[Tooltip("The value to set the SharedColor to")]
		public SharedColor targetValue;

		// Token: 0x04006D41 RID: 27969
		[RequiredField]
		[Tooltip("The SharedColor to set")]
		public SharedColor targetVariable;
	}
}
