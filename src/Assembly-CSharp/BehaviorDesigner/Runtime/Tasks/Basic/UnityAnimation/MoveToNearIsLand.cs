using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BB RID: 4539
	[TaskCategory("YSSea")]
	[TaskDescription("移动到最近的岛屿")]
	public class MoveToNearIsLand : Action
	{
		// Token: 0x0600778F RID: 30607 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x002B92D8 File Offset: 0x002B74D8
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x002B92EB File Offset: 0x002B74EB
		public override TaskStatus OnUpdate()
		{
			this.avatar.moveToNearlIsland();
			return 2;
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006317 RID: 25367
		private SeaAvatarObjBase avatar;

		// Token: 0x04006318 RID: 25368
		private SharedInt tempWeith;
	}
}
