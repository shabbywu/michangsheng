using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001105 RID: 4357
	[TaskCategory("Basic/Math")]
	[TaskDescription("Is the float a positive value?")]
	public class IsFloatPositive : Conditional
	{
		// Token: 0x060074D4 RID: 29908 RVA: 0x002B3578 File Offset: 0x002B1778
		public override TaskStatus OnUpdate()
		{
			if (this.floatVariable.Value <= 0f)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060074D5 RID: 29909 RVA: 0x002B358F File Offset: 0x002B178F
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x04006083 RID: 24707
		[Tooltip("The float to check if positive")]
		public SharedFloat floatVariable;
	}
}
