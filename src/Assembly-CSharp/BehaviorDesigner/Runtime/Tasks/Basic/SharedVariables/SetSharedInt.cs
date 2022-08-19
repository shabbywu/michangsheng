using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001064 RID: 4196
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedInt variable to the specified object. Returns Success.")]
	public class SetSharedInt : Action
	{
		// Token: 0x06007289 RID: 29321 RVA: 0x002AE275 File Offset: 0x002AC475
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x002AE28E File Offset: 0x002AC48E
		public override void OnReset()
		{
			this.targetValue = 0;
			this.targetVariable = 0;
		}

		// Token: 0x04005E49 RID: 24137
		[Tooltip("The value to set the SharedInt to")]
		public SharedInt targetValue;

		// Token: 0x04005E4A RID: 24138
		[RequiredField]
		[Tooltip("The SharedInt to set")]
		public SharedInt targetVariable;
	}
}
