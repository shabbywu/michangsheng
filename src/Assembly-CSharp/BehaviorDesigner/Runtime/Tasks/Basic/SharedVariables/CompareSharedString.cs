using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001059 RID: 4185
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedString : Conditional
	{
		// Token: 0x06007268 RID: 29288 RVA: 0x002ADEA9 File Offset: 0x002AC0A9
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x002ADECB File Offset: 0x002AC0CB
		public override void OnReset()
		{
			this.variable = "";
			this.compareTo = "";
		}

		// Token: 0x04005E32 RID: 24114
		[Tooltip("The first variable to compare")]
		public SharedString variable;

		// Token: 0x04005E33 RID: 24115
		[Tooltip("The variable to compare to")]
		public SharedString compareTo;
	}
}
