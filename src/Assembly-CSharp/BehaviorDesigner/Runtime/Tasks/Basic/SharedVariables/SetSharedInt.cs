using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200151E RID: 5406
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedInt variable to the specified object. Returns Success.")]
	public class SetSharedInt : Action
	{
		// Token: 0x06008083 RID: 32899 RVA: 0x000576D3 File Offset: 0x000558D3
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008084 RID: 32900 RVA: 0x000576EC File Offset: 0x000558EC
		public override void OnReset()
		{
			this.targetValue = 0;
			this.targetVariable = 0;
		}

		// Token: 0x04006D49 RID: 27977
		[Tooltip("The value to set the SharedInt to")]
		public SharedInt targetValue;

		// Token: 0x04006D4A RID: 27978
		[RequiredField]
		[Tooltip("The SharedInt to set")]
		public SharedInt targetVariable;
	}
}
