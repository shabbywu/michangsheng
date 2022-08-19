using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001106 RID: 4358
	[TaskCategory("Basic/Math")]
	[TaskDescription("Is the int a positive value?")]
	public class IsIntPositive : Conditional
	{
		// Token: 0x060074D7 RID: 29911 RVA: 0x002B35A1 File Offset: 0x002B17A1
		public override TaskStatus OnUpdate()
		{
			if (this.intVariable.Value <= 0)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060074D8 RID: 29912 RVA: 0x002B35B4 File Offset: 0x002B17B4
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04006084 RID: 24708
		[Tooltip("The int to check if positive")]
		public SharedInt intVariable;
	}
}
