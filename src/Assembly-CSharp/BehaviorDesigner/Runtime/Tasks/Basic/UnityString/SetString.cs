using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x0200104A RID: 4170
	[TaskCategory("Basic/String")]
	[TaskDescription("Sets the variable string to the value string.")]
	public class SetString : Action
	{
		// Token: 0x06007237 RID: 29239 RVA: 0x002AD7A9 File Offset: 0x002AB9A9
		public override TaskStatus OnUpdate()
		{
			this.variable.Value = this.value.Value;
			return 2;
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x002AD7C2 File Offset: 0x002AB9C2
		public override void OnReset()
		{
			this.variable = "";
			this.value = "";
		}

		// Token: 0x04005E0C RID: 24076
		[Tooltip("The target string")]
		[RequiredField]
		public SharedString variable;

		// Token: 0x04005E0D RID: 24077
		[Tooltip("The value string")]
		public SharedString value;
	}
}
