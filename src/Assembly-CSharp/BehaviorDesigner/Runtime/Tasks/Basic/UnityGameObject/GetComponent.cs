using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001139 RID: 4409
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns the component of Type type if the game object has one attached, null if it doesn't. Returns Success.")]
	public class GetComponent : Action
	{
		// Token: 0x06007582 RID: 30082 RVA: 0x002B4796 File Offset: 0x002B2996
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(this.type.Value);
			return 2;
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x002B47C5 File Offset: 0x002B29C5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.type.Value = "";
			this.storeValue.Value = null;
		}

		// Token: 0x0400610B RID: 24843
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400610C RID: 24844
		[Tooltip("The type of component")]
		public SharedString type;

		// Token: 0x0400610D RID: 24845
		[Tooltip("The component")]
		[RequiredField]
		public SharedObject storeValue;
	}
}
