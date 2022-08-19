using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001055 RID: 4181
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObject : Conditional
	{
		// Token: 0x0600725C RID: 29276 RVA: 0x002ADCA0 File Offset: 0x002ABEA0
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
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x002ADD1D File Offset: 0x002ABF1D
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E2A RID: 24106
		[Tooltip("The first variable to compare")]
		public SharedObject variable;

		// Token: 0x04005E2B RID: 24107
		[Tooltip("The variable to compare to")]
		public SharedObject compareTo;
	}
}
