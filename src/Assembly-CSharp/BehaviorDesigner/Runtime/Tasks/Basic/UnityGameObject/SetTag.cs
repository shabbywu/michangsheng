using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x0200113E RID: 4414
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Sets the GameObject tag. Returns Success.")]
	public class SetTag : Action
	{
		// Token: 0x06007591 RID: 30097 RVA: 0x002B4962 File Offset: 0x002B2B62
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).tag = this.tag.Value;
			return 2;
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x002B4986 File Offset: 0x002B2B86
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x04006119 RID: 24857
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400611A RID: 24858
		[Tooltip("The GameObject tag")]
		public SharedString tag;
	}
}
