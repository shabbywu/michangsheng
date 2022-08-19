using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001107 RID: 4359
	[TaskCategory("Basic/Math")]
	[TaskDescription("Lerp the float by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x060074DA RID: 29914 RVA: 0x002B35C2 File Offset: 0x002B17C2
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.Lerp(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x002B35F8 File Offset: 0x002B17F8
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04006085 RID: 24709
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04006086 RID: 24710
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04006087 RID: 24711
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04006088 RID: 24712
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
