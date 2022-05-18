using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F8 RID: 5624
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns the component of Type type if the game object has one attached, null if it doesn't. Returns Success.")]
	public class GetComponent : Action
	{
		// Token: 0x0600837C RID: 33660 RVA: 0x0005A6DA File Offset: 0x000588DA
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(this.type.Value);
			return 2;
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x0005A709 File Offset: 0x00058909
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.type.Value = "";
			this.storeValue.Value = null;
		}

		// Token: 0x0400702E RID: 28718
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400702F RID: 28719
		[Tooltip("The type of component")]
		public SharedString type;

		// Token: 0x04007030 RID: 28720
		[Tooltip("The component")]
		[RequiredField]
		public SharedObject storeValue;
	}
}
