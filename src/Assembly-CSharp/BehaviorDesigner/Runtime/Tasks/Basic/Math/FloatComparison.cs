using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FF RID: 4351
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs comparison between two floats: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class FloatComparison : Conditional
	{
		// Token: 0x060074C2 RID: 29890 RVA: 0x002B2FE8 File Offset: 0x002B11E8
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

		// Token: 0x060074C3 RID: 29891 RVA: 0x002B30C8 File Offset: 0x002B12C8
		public override void OnReset()
		{
			this.operation = FloatComparison.Operation.LessThan;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
		}

		// Token: 0x04006071 RID: 24689
		[Tooltip("The operation to perform")]
		public FloatComparison.Operation operation;

		// Token: 0x04006072 RID: 24690
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x04006073 RID: 24691
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x02001734 RID: 5940
		public enum Operation
		{
			// Token: 0x0400755D RID: 30045
			LessThan,
			// Token: 0x0400755E RID: 30046
			LessThanOrEqualTo,
			// Token: 0x0400755F RID: 30047
			EqualTo,
			// Token: 0x04007560 RID: 30048
			NotEqualTo,
			// Token: 0x04007561 RID: 30049
			GreaterThanOrEqualTo,
			// Token: 0x04007562 RID: 30050
			GreaterThan
		}
	}
}
