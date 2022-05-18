using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001502 RID: 5378
	[TaskCategory("Basic/String")]
	[TaskDescription("Returns success if the string is null or empty")]
	public class IsNullOrEmpty : Conditional
	{
		// Token: 0x0600802B RID: 32811 RVA: 0x00057280 File Offset: 0x00055480
		public override TaskStatus OnUpdate()
		{
			if (!string.IsNullOrEmpty(this.targetString.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x00057297 File Offset: 0x00055497
		public override void OnReset()
		{
			this.targetString = "";
		}

		// Token: 0x04006D07 RID: 27911
		[Tooltip("The target string")]
		public SharedString targetString;
	}
}
