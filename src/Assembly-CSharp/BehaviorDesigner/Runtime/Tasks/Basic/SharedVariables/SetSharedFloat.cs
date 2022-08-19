using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001061 RID: 4193
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedFloat variable to the specified object. Returns Success.")]
	public class SetSharedFloat : Action
	{
		// Token: 0x06007280 RID: 29312 RVA: 0x002AE1A8 File Offset: 0x002AC3A8
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x002AE1C1 File Offset: 0x002AC3C1
		public override void OnReset()
		{
			this.targetValue = 0f;
			this.targetVariable = 0f;
		}

		// Token: 0x04005E42 RID: 24130
		[Tooltip("The value to set the SharedFloat to")]
		public SharedFloat targetValue;

		// Token: 0x04005E43 RID: 24131
		[RequiredField]
		[Tooltip("The SharedFloat to set")]
		public SharedFloat targetVariable;
	}
}
