using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C4 RID: 5572
	[TaskCategory("Basic/Math")]
	[TaskDescription("Is the float a positive value?")]
	public class IsFloatPositive : Conditional
	{
		// Token: 0x060082CE RID: 33486 RVA: 0x00059BCE File Offset: 0x00057DCE
		public override TaskStatus OnUpdate()
		{
			if (this.floatVariable.Value <= 0f)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060082CF RID: 33487 RVA: 0x00059BE5 File Offset: 0x00057DE5
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x04006FA6 RID: 28582
		[Tooltip("The float to check if positive")]
		public SharedFloat floatVariable;
	}
}
