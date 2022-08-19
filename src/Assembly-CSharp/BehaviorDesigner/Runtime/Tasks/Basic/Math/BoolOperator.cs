using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FC RID: 4348
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two bools: AND, OR, NAND, or XOR.")]
	public class BoolOperator : Action
	{
		// Token: 0x060074B9 RID: 29881 RVA: 0x002B2E58 File Offset: 0x002B1058
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case BoolOperator.Operation.AND:
				this.storeResult.Value = (this.bool1.Value && this.bool2.Value);
				break;
			case BoolOperator.Operation.OR:
				this.storeResult.Value = (this.bool1.Value || this.bool2.Value);
				break;
			case BoolOperator.Operation.NAND:
				this.storeResult.Value = (!this.bool1.Value || !this.bool2.Value);
				break;
			case BoolOperator.Operation.XOR:
				this.storeResult.Value = (this.bool1.Value ^ this.bool2.Value);
				break;
			}
			return 2;
		}

		// Token: 0x060074BA RID: 29882 RVA: 0x002B2F25 File Offset: 0x002B1125
		public override void OnReset()
		{
			this.operation = BoolOperator.Operation.AND;
			this.bool1.Value = false;
			this.bool2.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04006069 RID: 24681
		[Tooltip("The operation to perform")]
		public BoolOperator.Operation operation;

		// Token: 0x0400606A RID: 24682
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x0400606B RID: 24683
		[Tooltip("The second bool")]
		public SharedBool bool2;

		// Token: 0x0400606C RID: 24684
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;

		// Token: 0x02001733 RID: 5939
		public enum Operation
		{
			// Token: 0x04007558 RID: 30040
			AND,
			// Token: 0x04007559 RID: 30041
			OR,
			// Token: 0x0400755A RID: 30042
			NAND,
			// Token: 0x0400755B RID: 30043
			XOR
		}
	}
}
