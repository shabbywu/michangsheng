using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001125 RID: 4389
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the raw value of the specified axis and stores it in a float.")]
	public class GetAxisRaw : Action
	{
		// Token: 0x06007546 RID: 30022 RVA: 0x002B4384 File Offset: 0x002B2584
		public override TaskStatus OnUpdate()
		{
			float num = Input.GetAxis(this.axisName.Value);
			if (!this.multiplier.IsNone)
			{
				num *= this.multiplier.Value;
			}
			this.storeResult.Value = num;
			return 2;
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x002B43CA File Offset: 0x002B25CA
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x040060EC RID: 24812
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x040060ED RID: 24813
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x040060EE RID: 24814
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
