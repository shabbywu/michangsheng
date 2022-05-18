using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200151C RID: 5404
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
	public class SetSharedGameObject : Action
	{
		// Token: 0x0600807D RID: 32893 RVA: 0x002CB4D8 File Offset: 0x002C96D8
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null || this.valueCanBeNull.Value) ? this.targetValue.Value : this.gameObject);
			return 2;
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x0005768E File Offset: 0x0005588E
		public override void OnReset()
		{
			this.valueCanBeNull = false;
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04006D44 RID: 27972
		[Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
		public SharedGameObject targetValue;

		// Token: 0x04006D45 RID: 27973
		[RequiredField]
		[Tooltip("The SharedGameObject to set")]
		public SharedGameObject targetVariable;

		// Token: 0x04006D46 RID: 27974
		[Tooltip("Can the target value be null?")]
		public SharedBool valueCanBeNull;
	}
}
