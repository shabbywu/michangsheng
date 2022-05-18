using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167D RID: 5757
	[TaskCategory("YSSea")]
	[TaskDescription("移动到最近的岛屿")]
	public class MoveToNearIsLand : Action
	{
		// Token: 0x0600859A RID: 34202 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x0600859B RID: 34203 RVA: 0x0005CAE8 File Offset: 0x0005ACE8
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600859C RID: 34204 RVA: 0x0005CAFB File Offset: 0x0005ACFB
		public override TaskStatus OnUpdate()
		{
			this.avatar.moveToNearlIsland();
			return 2;
		}

		// Token: 0x0600859D RID: 34205 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007246 RID: 29254
		private SeaAvatarObjBase avatar;

		// Token: 0x04007247 RID: 29255
		private SharedInt tempWeith;
	}
}
