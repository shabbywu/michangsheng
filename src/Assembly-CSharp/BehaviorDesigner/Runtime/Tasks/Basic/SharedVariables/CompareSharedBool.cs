using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001509 RID: 5385
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedBool : Conditional
	{
		// Token: 0x06008044 RID: 32836 RVA: 0x002CAF60 File Offset: 0x002C9160
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x00057441 File Offset: 0x00055641
		public override void OnReset()
		{
			this.variable = false;
			this.compareTo = false;
		}

		// Token: 0x04006D1E RID: 27934
		[Tooltip("The first variable to compare")]
		public SharedBool variable;

		// Token: 0x04006D1F RID: 27935
		[Tooltip("The variable to compare to")]
		public SharedBool compareTo;
	}
}
