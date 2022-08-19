using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001048 RID: 4168
	[TaskCategory("Basic/String")]
	[TaskDescription("Returns success if the string is null or empty")]
	public class IsNullOrEmpty : Conditional
	{
		// Token: 0x06007231 RID: 29233 RVA: 0x002AD6FF File Offset: 0x002AB8FF
		public override TaskStatus OnUpdate()
		{
			if (!string.IsNullOrEmpty(this.targetString.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007232 RID: 29234 RVA: 0x002AD716 File Offset: 0x002AB916
		public override void OnReset()
		{
			this.targetString = "";
		}

		// Token: 0x04005E07 RID: 24071
		[Tooltip("The target string")]
		public SharedString targetString;
	}
}
