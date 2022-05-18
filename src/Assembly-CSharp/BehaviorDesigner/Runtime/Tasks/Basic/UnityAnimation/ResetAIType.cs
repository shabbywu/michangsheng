using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167F RID: 5759
	[TaskCategory("YSSea")]
	[TaskDescription("重置NPC AI类型")]
	public class ResetAIType : Action
	{
		// Token: 0x060085A4 RID: 34212 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x0005CB2A File Offset: 0x0005AD2A
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x0005CB3D File Offset: 0x0005AD3D
		public override TaskStatus OnUpdate()
		{
			this.avatar.ResetBehavirTree(this.AIType.Value);
			return 2;
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400724A RID: 29258
		private SeaAvatarObjBase avatar;

		// Token: 0x0400724B RID: 29259
		public SharedInt AIType;
	}
}
