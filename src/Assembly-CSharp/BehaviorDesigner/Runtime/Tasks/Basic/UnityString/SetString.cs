using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001504 RID: 5380
	[TaskCategory("Basic/String")]
	[TaskDescription("Sets the variable string to the value string.")]
	public class SetString : Action
	{
		// Token: 0x06008031 RID: 32817 RVA: 0x000572DD File Offset: 0x000554DD
		public override TaskStatus OnUpdate()
		{
			this.variable.Value = this.value.Value;
			return 2;
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x000572F6 File Offset: 0x000554F6
		public override void OnReset()
		{
			this.variable = "";
			this.value = "";
		}

		// Token: 0x04006D0C RID: 27916
		[Tooltip("The target string")]
		[RequiredField]
		public SharedString variable;

		// Token: 0x04006D0D RID: 27917
		[Tooltip("The value string")]
		public SharedString value;
	}
}
