using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001066 RID: 4198
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedObjectList variable to the specified object. Returns Success.")]
	public class SetSharedObjectList : Action
	{
		// Token: 0x0600728F RID: 29327 RVA: 0x002AE2D1 File Offset: 0x002AC4D1
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06007290 RID: 29328 RVA: 0x002AE2EA File Offset: 0x002AC4EA
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04005E4D RID: 24141
		[Tooltip("The value to set the SharedObjectList to.")]
		public SharedObjectList targetValue;

		// Token: 0x04005E4E RID: 24142
		[RequiredField]
		[Tooltip("The SharedObjectList to set")]
		public SharedObjectList targetVariable;
	}
}
