using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200104F RID: 4175
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedBool : Conditional
	{
		// Token: 0x0600724A RID: 29258 RVA: 0x002ADA10 File Offset: 0x002ABC10
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600724B RID: 29259 RVA: 0x002ADA40 File Offset: 0x002ABC40
		public override void OnReset()
		{
			this.variable = false;
			this.compareTo = false;
		}

		// Token: 0x04005E1E RID: 24094
		[Tooltip("The first variable to compare")]
		public SharedBool variable;

		// Token: 0x04005E1F RID: 24095
		[Tooltip("The variable to compare to")]
		public SharedBool compareTo;
	}
}
