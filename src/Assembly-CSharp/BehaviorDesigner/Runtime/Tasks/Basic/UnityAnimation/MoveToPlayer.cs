using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BC RID: 4540
	[TaskCategory("YSSea")]
	[TaskDescription("移动到玩家坐标点")]
	public class MoveToPlayer : Action
	{
		// Token: 0x06007794 RID: 30612 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x002B92F9 File Offset: 0x002B74F9
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x002B930C File Offset: 0x002B750C
		public override TaskStatus OnUpdate()
		{
			this.avatar.MonstarMoveToPlayer();
			return 2;
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006319 RID: 25369
		private SeaAvatarObjBase avatar;

		// Token: 0x0400631A RID: 25370
		private SharedInt tempWeith;
	}
}
