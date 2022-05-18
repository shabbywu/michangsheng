using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E3 RID: 5603
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the value of the specified axis and stores it in a float.")]
	public class GetAxis : Action
	{
		// Token: 0x0600833D RID: 33597 RVA: 0x002CE8E4 File Offset: 0x002CCAE4
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

		// Token: 0x0600833E RID: 33598 RVA: 0x0005A363 File Offset: 0x00058563
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x0400700C RID: 28684
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x0400700D RID: 28685
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x0400700E RID: 28686
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
