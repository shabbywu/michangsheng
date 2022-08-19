using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001052 RID: 4178
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObject : Conditional
	{
		// Token: 0x06007253 RID: 29267 RVA: 0x002ADB04 File Offset: 0x002ABD04
		public override TaskStatus OnUpdate()
		{
			if (this.variable.Value == null && this.compareTo.Value != null)
			{
				return 1;
			}
			if (this.variable.Value == null && this.compareTo.Value == null)
			{
				return 2;
			}
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x002ADB81 File Offset: 0x002ABD81
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04005E24 RID: 24100
		[Tooltip("The first variable to compare")]
		public SharedGameObject variable;

		// Token: 0x04005E25 RID: 24101
		[Tooltip("The variable to compare to")]
		public SharedGameObject compareTo;
	}
}
