using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001519 RID: 5401
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
	public class SetSharedBool : Action
	{
		// Token: 0x06008074 RID: 32884 RVA: 0x000575E5 File Offset: 0x000557E5
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008075 RID: 32885 RVA: 0x000575FE File Offset: 0x000557FE
		public override void OnReset()
		{
			this.targetValue = false;
			this.targetVariable = false;
		}

		// Token: 0x04006D3E RID: 27966
		[Tooltip("The value to set the SharedBool to")]
		public SharedBool targetValue;

		// Token: 0x04006D3F RID: 27967
		[RequiredField]
		[Tooltip("The SharedBool to set")]
		public SharedBool targetVariable;
	}
}
