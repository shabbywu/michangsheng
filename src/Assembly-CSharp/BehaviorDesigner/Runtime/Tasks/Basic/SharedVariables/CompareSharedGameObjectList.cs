using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001053 RID: 4179
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObjectList : Conditional
	{
		// Token: 0x06007256 RID: 29270 RVA: 0x002ADB94 File Offset: 0x002ABD94
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

		// Token: 0x06007257 RID: 29271 RVA: 0x002ADC44 File Offset: 0x002ABE44
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E26 RID: 24102
		[Tooltip("The first variable to compare")]
		public SharedGameObjectList variable;

		// Token: 0x04005E27 RID: 24103
		[Tooltip("The variable to compare to")]
		public SharedGameObjectList compareTo;
	}
}
