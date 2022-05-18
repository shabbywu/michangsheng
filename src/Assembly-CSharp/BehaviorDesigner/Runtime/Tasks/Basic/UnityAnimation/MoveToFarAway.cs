using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167C RID: 5756
	[TaskCategory("YSSea")]
	[TaskDescription("逃离玩家")]
	public class MoveToFarAway : Action
	{
		// Token: 0x06008595 RID: 34197 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008596 RID: 34198 RVA: 0x0005CAC7 File Offset: 0x0005ACC7
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06008597 RID: 34199 RVA: 0x0005CADA File Offset: 0x0005ACDA
		public override TaskStatus OnUpdate()
		{
			this.avatar.moveAwayFromPositon();
			return 2;
		}

		// Token: 0x06008598 RID: 34200 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007244 RID: 29252
		private SeaAvatarObjBase avatar;

		// Token: 0x04007245 RID: 29253
		private SharedInt tempWeith;
	}
}
