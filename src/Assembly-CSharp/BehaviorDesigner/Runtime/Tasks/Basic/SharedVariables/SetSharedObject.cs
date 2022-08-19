using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001065 RID: 4197
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedObject variable to the specified object. Returns Success.")]
	public class SetSharedObject : Action
	{
		// Token: 0x0600728C RID: 29324 RVA: 0x002AE2A8 File Offset: 0x002AC4A8
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x002AE2C1 File Offset: 0x002AC4C1
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04005E4B RID: 24139
		[Tooltip("The value to set the SharedObject to")]
		public SharedObject targetValue;

		// Token: 0x04005E4C RID: 24140
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedObject targetVariable;
	}
}
