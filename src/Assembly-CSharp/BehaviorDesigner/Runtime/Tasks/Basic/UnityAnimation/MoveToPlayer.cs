using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167E RID: 5758
	[TaskCategory("YSSea")]
	[TaskDescription("移动到玩家坐标点")]
	public class MoveToPlayer : Action
	{
		// Token: 0x0600859F RID: 34207 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085A0 RID: 34208 RVA: 0x0005CB09 File Offset: 0x0005AD09
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x0005CB1C File Offset: 0x0005AD1C
		public override TaskStatus OnUpdate()
		{
			this.avatar.MonstarMoveToPlayer();
			return 2;
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007248 RID: 29256
		private SeaAvatarObjBase avatar;

		// Token: 0x04007249 RID: 29257
		private SharedInt tempWeith;
	}
}
