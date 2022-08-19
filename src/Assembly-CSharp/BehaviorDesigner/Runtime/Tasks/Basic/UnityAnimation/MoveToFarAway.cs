using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BA RID: 4538
	[TaskCategory("YSSea")]
	[TaskDescription("逃离玩家")]
	public class MoveToFarAway : Action
	{
		// Token: 0x0600778A RID: 30602 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x002B92B7 File Offset: 0x002B74B7
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x002B92CA File Offset: 0x002B74CA
		public override TaskStatus OnUpdate()
		{
			this.avatar.moveAwayFromPositon();
			return 2;
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006315 RID: 25365
		private SeaAvatarObjBase avatar;

		// Token: 0x04006316 RID: 25366
		private SharedInt tempWeith;
	}
}
