using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C5 RID: 5573
	[TaskCategory("Basic/Math")]
	[TaskDescription("Is the int a positive value?")]
	public class IsIntPositive : Conditional
	{
		// Token: 0x060082D1 RID: 33489 RVA: 0x00059BF7 File Offset: 0x00057DF7
		public override TaskStatus OnUpdate()
		{
			if (this.intVariable.Value <= 0)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060082D2 RID: 33490 RVA: 0x00059C0A File Offset: 0x00057E0A
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04006FA7 RID: 28583
		[Tooltip("The int to check if positive")]
		public SharedInt intVariable;
	}
}
