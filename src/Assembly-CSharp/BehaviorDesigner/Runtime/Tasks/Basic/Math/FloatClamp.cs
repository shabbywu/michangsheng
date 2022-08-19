using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FE RID: 4350
	[TaskCategory("Basic/Math")]
	[TaskDescription("Clamps the float between two values.")]
	public class FloatClamp : Action
	{
		// Token: 0x060074BF RID: 29887 RVA: 0x002B2F82 File Offset: 0x002B1182
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Clamp(this.floatVariable.Value, this.minValue.Value, this.maxValue.Value);
			return 2;
		}

		// Token: 0x060074C0 RID: 29888 RVA: 0x002B2FB8 File Offset: 0x002B11B8
		public override void OnReset()
		{
			this.floatVariable = (this.minValue = (this.maxValue = 0f));
		}

		// Token: 0x0400606E RID: 24686
		[Tooltip("The float to clamp")]
		public SharedFloat floatVariable;

		// Token: 0x0400606F RID: 24687
		[Tooltip("The maximum value of the float")]
		public SharedFloat minValue;

		// Token: 0x04006070 RID: 24688
		[Tooltip("The maximum value of the float")]
		public SharedFloat maxValue;
	}
}
