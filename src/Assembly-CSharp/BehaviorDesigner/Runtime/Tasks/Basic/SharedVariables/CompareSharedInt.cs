using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001054 RID: 4180
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedInt : Conditional
	{
		// Token: 0x06007259 RID: 29273 RVA: 0x002ADC54 File Offset: 0x002ABE54
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x002ADC84 File Offset: 0x002ABE84
		public override void OnReset()
		{
			this.variable = 0;
			this.compareTo = 0;
		}

		// Token: 0x04005E28 RID: 24104
		[Tooltip("The first variable to compare")]
		public SharedInt variable;

		// Token: 0x04005E29 RID: 24105
		[Tooltip("The variable to compare to")]
		public SharedInt compareTo;
	}
}
