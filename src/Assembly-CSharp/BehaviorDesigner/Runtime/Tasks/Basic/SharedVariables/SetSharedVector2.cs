using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106C RID: 4204
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector2 variable to the specified object. Returns Success.")]
	public class SetSharedVector2 : Action
	{
		// Token: 0x060072A1 RID: 29345 RVA: 0x002AE42D File Offset: 0x002AC62D
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x002AE446 File Offset: 0x002AC646
		public override void OnReset()
		{
			this.targetValue = Vector2.zero;
			this.targetVariable = Vector2.zero;
		}

		// Token: 0x04005E59 RID: 24153
		[Tooltip("The value to set the SharedVector2 to")]
		public SharedVector2 targetValue;

		// Token: 0x04005E5A RID: 24154
		[RequiredField]
		[Tooltip("The SharedVector2 to set")]
		public SharedVector2 targetVariable;
	}
}
