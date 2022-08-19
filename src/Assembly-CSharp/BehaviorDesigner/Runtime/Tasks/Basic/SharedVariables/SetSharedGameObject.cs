using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001062 RID: 4194
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
	public class SetSharedGameObject : Action
	{
		// Token: 0x06007283 RID: 29315 RVA: 0x002AE1E4 File Offset: 0x002AC3E4
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null || this.valueCanBeNull.Value) ? this.targetValue.Value : this.gameObject);
			return 2;
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x002AE230 File Offset: 0x002AC430
		public override void OnReset()
		{
			this.valueCanBeNull = false;
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04005E44 RID: 24132
		[Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
		public SharedGameObject targetValue;

		// Token: 0x04005E45 RID: 24133
		[RequiredField]
		[Tooltip("The SharedGameObject to set")]
		public SharedGameObject targetVariable;

		// Token: 0x04005E46 RID: 24134
		[Tooltip("Can the target value be null?")]
		public SharedBool valueCanBeNull;
	}
}
