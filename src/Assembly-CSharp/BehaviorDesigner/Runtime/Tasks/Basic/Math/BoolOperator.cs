using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015B6 RID: 5558
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two bools: AND, OR, NAND, or XOR.")]
	public class BoolOperator : Action
	{
		// Token: 0x060082B3 RID: 33459 RVA: 0x002CDD6C File Offset: 0x002CBF6C
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

		// Token: 0x060082B4 RID: 33460 RVA: 0x00059A2D File Offset: 0x00057C2D
		public override void OnReset()
		{
			this.operation = BoolOperator.Operation.AND;
			this.bool1.Value = false;
			this.bool2.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04006F69 RID: 28521
		[Tooltip("The operation to perform")]
		public BoolOperator.Operation operation;

		// Token: 0x04006F6A RID: 28522
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x04006F6B RID: 28523
		[Tooltip("The second bool")]
		public SharedBool bool2;

		// Token: 0x04006F6C RID: 28524
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;

		// Token: 0x020015B7 RID: 5559
		public enum Operation
		{
			// Token: 0x04006F6E RID: 28526
			AND,
			// Token: 0x04006F6F RID: 28527
			OR,
			// Token: 0x04006F70 RID: 28528
			NAND,
			// Token: 0x04006F71 RID: 28529
			XOR
		}
	}
}
