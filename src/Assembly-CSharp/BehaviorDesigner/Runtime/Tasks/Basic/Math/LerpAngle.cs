using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001108 RID: 4360
	[TaskCategory("Basic/Math")]
	[TaskDescription("Lerp the angle by an amount.")]
	public class LerpAngle : Action
	{
		// Token: 0x060074DD RID: 29917 RVA: 0x002B3630 File Offset: 0x002B1830
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.LerpAngle(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x002B3664 File Offset: 0x002B1864
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04006089 RID: 24713
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x0400608A RID: 24714
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x0400608B RID: 24715
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x0400608C RID: 24716
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
