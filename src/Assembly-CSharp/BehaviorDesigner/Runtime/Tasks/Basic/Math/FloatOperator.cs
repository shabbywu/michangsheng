using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015BC RID: 5564
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two floats: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class FloatOperator : Action
	{
		// Token: 0x060082BF RID: 33471 RVA: 0x002CDF4C File Offset: 0x002CC14C
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatOperator.Operation.Add:
				this.storeResult.Value = this.float1.Value + this.float2.Value;
				break;
			case FloatOperator.Operation.Subtract:
				this.storeResult.Value = this.float1.Value - this.float2.Value;
				break;
			case FloatOperator.Operation.Multiply:
				this.storeResult.Value = this.float1.Value * this.float2.Value;
				break;
			case FloatOperator.Operation.Divide:
				this.storeResult.Value = this.float1.Value / this.float2.Value;
				break;
			case FloatOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Modulo:
				this.storeResult.Value = this.float1.Value % this.float2.Value;
				break;
			}
			return 2;
		}

		// Token: 0x060082C0 RID: 33472 RVA: 0x00059AE7 File Offset: 0x00057CE7
		public override void OnReset()
		{
			this.operation = FloatOperator.Operation.Add;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006F80 RID: 28544
		[Tooltip("The operation to perform")]
		public FloatOperator.Operation operation;

		// Token: 0x04006F81 RID: 28545
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x04006F82 RID: 28546
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x04006F83 RID: 28547
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;

		// Token: 0x020015BD RID: 5565
		public enum Operation
		{
			// Token: 0x04006F85 RID: 28549
			Add,
			// Token: 0x04006F86 RID: 28550
			Subtract,
			// Token: 0x04006F87 RID: 28551
			Multiply,
			// Token: 0x04006F88 RID: 28552
			Divide,
			// Token: 0x04006F89 RID: 28553
			Min,
			// Token: 0x04006F8A RID: 28554
			Max,
			// Token: 0x04006F8B RID: 28555
			Modulo
		}
	}
}
