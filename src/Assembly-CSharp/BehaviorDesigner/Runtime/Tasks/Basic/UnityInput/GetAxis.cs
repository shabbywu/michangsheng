using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001124 RID: 4388
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the value of the specified axis and stores it in a float.")]
	public class GetAxis : Action
	{
		// Token: 0x06007543 RID: 30019 RVA: 0x002B430C File Offset: 0x002B250C
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

		// Token: 0x06007544 RID: 30020 RVA: 0x002B4352 File Offset: 0x002B2552
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x040060E9 RID: 24809
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x040060EA RID: 24810
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x040060EB RID: 24811
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
