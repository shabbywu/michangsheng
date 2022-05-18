using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E4 RID: 5604
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the raw value of the specified axis and stores it in a float.")]
	public class GetAxisRaw : Action
	{
		// Token: 0x06008340 RID: 33600 RVA: 0x002CE92C File Offset: 0x002CCB2C
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

		// Token: 0x06008341 RID: 33601 RVA: 0x0005A395 File Offset: 0x00058595
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x0400700F RID: 28687
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x04007010 RID: 28688
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x04007011 RID: 28689
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
