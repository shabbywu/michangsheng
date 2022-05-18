using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200151B RID: 5403
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedFloat variable to the specified object. Returns Success.")]
	public class SetSharedFloat : Action
	{
		// Token: 0x0600807A RID: 32890 RVA: 0x00057653 File Offset: 0x00055853
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x0005766C File Offset: 0x0005586C
		public override void OnReset()
		{
			this.targetValue = 0f;
			this.targetVariable = 0f;
		}

		// Token: 0x04006D42 RID: 27970
		[Tooltip("The value to set the SharedFloat to")]
		public SharedFloat targetValue;

		// Token: 0x04006D43 RID: 27971
		[RequiredField]
		[Tooltip("The SharedFloat to set")]
		public SharedFloat targetVariable;
	}
}
