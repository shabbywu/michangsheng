using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001046 RID: 4166
	[TaskCategory("Basic/String")]
	[TaskDescription("Randomly selects a string from the array of strings.")]
	public class GetRandomString : Action
	{
		// Token: 0x0600722B RID: 29227 RVA: 0x002AD5F8 File Offset: 0x002AB7F8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.source[Random.Range(0, this.source.Length)].Value;
			return 2;
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x002AD620 File Offset: 0x002AB820
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x04005E01 RID: 24065
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x04005E02 RID: 24066
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
