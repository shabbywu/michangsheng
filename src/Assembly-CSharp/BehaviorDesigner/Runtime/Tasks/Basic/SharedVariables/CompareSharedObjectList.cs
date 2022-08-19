using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001056 RID: 4182
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObjectList : Conditional
	{
		// Token: 0x0600725F RID: 29279 RVA: 0x002ADD30 File Offset: 0x002ABF30
		public override TaskStatus OnUpdate()
		{
			if (this.variable.Value == null && this.compareTo.Value != null)
			{
				return 1;
			}
			if (this.variable.Value == null && this.compareTo.Value == null)
			{
				return 2;
			}
			if (this.variable.Value.Count != this.compareTo.Value.Count)
			{
				return 1;
			}
			for (int i = 0; i < this.variable.Value.Count; i++)
			{
				if (this.variable.Value[i] != this.compareTo.Value[i])
				{
					return 1;
				}
			}
			return 2;
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x002ADDE0 File Offset: 0x002ABFE0
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E2C RID: 24108
		[Tooltip("The first variable to compare")]
		public SharedObjectList variable;

		// Token: 0x04005E2D RID: 24109
		[Tooltip("The variable to compare to")]
		public SharedObjectList compareTo;
	}
}
