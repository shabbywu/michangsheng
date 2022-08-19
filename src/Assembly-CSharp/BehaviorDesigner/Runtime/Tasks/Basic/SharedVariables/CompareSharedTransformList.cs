using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105B RID: 4187
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransformList : Conditional
	{
		// Token: 0x0600726E RID: 29294 RVA: 0x002ADF80 File Offset: 0x002AC180
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

		// Token: 0x0600726F RID: 29295 RVA: 0x002AE030 File Offset: 0x002AC230
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E36 RID: 24118
		[Tooltip("The first variable to compare")]
		public SharedTransformList variable;

		// Token: 0x04005E37 RID: 24119
		[Tooltip("The variable to compare to")]
		public SharedTransformList compareTo;
	}
}
