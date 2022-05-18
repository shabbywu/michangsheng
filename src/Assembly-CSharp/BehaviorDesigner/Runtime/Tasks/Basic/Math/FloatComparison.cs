using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015BA RID: 5562
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs comparison between two floats: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class FloatComparison : Conditional
	{
		// Token: 0x060082BC RID: 33468 RVA: 0x002CDE6C File Offset: 0x002CC06C
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatComparison.Operation.LessThan:
				if (this.float1.Value >= this.float2.Value)
				{
					return 1;
				}
				return 2;
			case FloatComparison.Operation.LessThanOrEqualTo:
				if (this.float1.Value > this.float2.Value)
				{
					return 1;
				}
				return 2;
			case FloatComparison.Operation.EqualTo:
				if (this.float1.Value != this.float2.Value)
				{
					return 1;
				}
				return 2;
			case FloatComparison.Operation.NotEqualTo:
				if (this.float1.Value == this.float2.Value)
				{
					return 1;
				}
				return 2;
			case FloatComparison.Operation.GreaterThanOrEqualTo:
				if (this.float1.Value < this.float2.Value)
				{
					return 1;
				}
				return 2;
			case FloatComparison.Operation.GreaterThan:
				if (this.float1.Value <= this.float2.Value)
				{
					return 1;
				}
				return 2;
			default:
				return 1;
			}
		}

		// Token: 0x060082BD RID: 33469 RVA: 0x00059ABE File Offset: 0x00057CBE
		public override void OnReset()
		{
			this.operation = FloatComparison.Operation.LessThan;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
		}

		// Token: 0x04006F76 RID: 28534
		[Tooltip("The operation to perform")]
		public FloatComparison.Operation operation;

		// Token: 0x04006F77 RID: 28535
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x04006F78 RID: 28536
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x020015BB RID: 5563
		public enum Operation
		{
			// Token: 0x04006F7A RID: 28538
			LessThan,
			// Token: 0x04006F7B RID: 28539
			LessThanOrEqualTo,
			// Token: 0x04006F7C RID: 28540
			EqualTo,
			// Token: 0x04006F7D RID: 28541
			NotEqualTo,
			// Token: 0x04006F7E RID: 28542
			GreaterThanOrEqualTo,
			// Token: 0x04006F7F RID: 28543
			GreaterThan
		}
	}
}
