using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001520 RID: 5408
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedObjectList variable to the specified object. Returns Success.")]
	public class SetSharedObjectList : Action
	{
		// Token: 0x06008089 RID: 32905 RVA: 0x0005772F File Offset: 0x0005592F
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x00057748 File Offset: 0x00055948
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04006D4D RID: 27981
		[Tooltip("The value to set the SharedObjectList to.")]
		public SharedObjectList targetValue;

		// Token: 0x04006D4E RID: 27982
		[RequiredField]
		[Tooltip("The SharedObjectList to set")]
		public SharedObjectList targetVariable;
	}
}
