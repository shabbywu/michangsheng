using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001069 RID: 4201
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedString variable to the specified object. Returns Success.")]
	public class SetSharedString : Action
	{
		// Token: 0x06007298 RID: 29336 RVA: 0x002AE385 File Offset: 0x002AC585
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x002AE39E File Offset: 0x002AC59E
		public override void OnReset()
		{
			this.targetValue = "";
			this.targetVariable = "";
		}

		// Token: 0x04005E53 RID: 24147
		[Tooltip("The value to set the SharedString to")]
		public SharedString targetValue;

		// Token: 0x04005E54 RID: 24148
		[RequiredField]
		[Tooltip("The SharedString to set")]
		public SharedString targetVariable;
	}
}
