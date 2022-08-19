using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105A RID: 4186
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransform : Conditional
	{
		// Token: 0x0600726B RID: 29291 RVA: 0x002ADEF0 File Offset: 0x002AC0F0
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

		// Token: 0x0600726C RID: 29292 RVA: 0x002ADF6D File Offset: 0x002AC16D
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E34 RID: 24116
		[Tooltip("The first variable to compare")]
		public SharedTransform variable;

		// Token: 0x04005E35 RID: 24117
		[Tooltip("The variable to compare to")]
		public SharedTransform compareTo;
	}
}
