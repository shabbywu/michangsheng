using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001500 RID: 5376
	[TaskCategory("Basic/String")]
	[TaskDescription("Randomly selects a string from the array of strings.")]
	public class GetRandomString : Action
	{
		// Token: 0x06008025 RID: 32805 RVA: 0x000571EE File Offset: 0x000553EE
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.source[Random.Range(0, this.source.Length)].Value;
			return 2;
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x00057216 File Offset: 0x00055416
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x04006D01 RID: 27905
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x04006D02 RID: 27906
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
