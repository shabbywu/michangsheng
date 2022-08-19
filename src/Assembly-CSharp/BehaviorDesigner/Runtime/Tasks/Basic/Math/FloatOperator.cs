using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001100 RID: 4352
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two floats: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class FloatOperator : Action
	{
		// Token: 0x060074C5 RID: 29893 RVA: 0x002B30F4 File Offset: 0x002B12F4
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

		// Token: 0x060074C6 RID: 29894 RVA: 0x002B323B File Offset: 0x002B143B
		public override void OnReset()
		{
			this.operation = FloatOperator.Operation.Add;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006074 RID: 24692
		[Tooltip("The operation to perform")]
		public FloatOperator.Operation operation;

		// Token: 0x04006075 RID: 24693
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x04006076 RID: 24694
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x04006077 RID: 24695
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;

		// Token: 0x02001735 RID: 5941
		public enum Operation
		{
			// Token: 0x04007564 RID: 30052
			Add,
			// Token: 0x04007565 RID: 30053
			Subtract,
			// Token: 0x04007566 RID: 30054
			Multiply,
			// Token: 0x04007567 RID: 30055
			Divide,
			// Token: 0x04007568 RID: 30056
			Min,
			// Token: 0x04007569 RID: 30057
			Max,
			// Token: 0x0400756A RID: 30058
			Modulo
		}
	}
}
