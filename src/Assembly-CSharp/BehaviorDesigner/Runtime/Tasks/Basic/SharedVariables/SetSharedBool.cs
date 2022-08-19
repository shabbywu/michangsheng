using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105F RID: 4191
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
	public class SetSharedBool : Action
	{
		// Token: 0x0600727A RID: 29306 RVA: 0x002AE13A File Offset: 0x002AC33A
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600727B RID: 29307 RVA: 0x002AE153 File Offset: 0x002AC353
		public override void OnReset()
		{
			this.targetValue = false;
			this.targetVariable = false;
		}

		// Token: 0x04005E3E RID: 24126
		[Tooltip("The value to set the SharedBool to")]
		public SharedBool targetValue;

		// Token: 0x04005E3F RID: 24127
		[RequiredField]
		[Tooltip("The SharedBool to set")]
		public SharedBool targetVariable;
	}
}
