using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015FD RID: 5629
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Sets the GameObject tag. Returns Success.")]
	public class SetTag : Action
	{
		// Token: 0x0600838B RID: 33675 RVA: 0x0005A832 File Offset: 0x00058A32
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).tag = this.tag.Value;
			return 2;
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x0005A856 File Offset: 0x00058A56
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
		}

		// Token: 0x0400703C RID: 28732
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400703D RID: 28733
		[Tooltip("The GameObject tag")]
		public SharedString tag;
	}
}
