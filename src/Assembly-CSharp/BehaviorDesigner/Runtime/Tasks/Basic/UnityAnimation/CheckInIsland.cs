using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B6 RID: 4534
	[TaskCategory("YSSea")]
	[TaskDescription("检测NPC是否在岛上")]
	public class CheckInIsland : Conditional
	{
		// Token: 0x06007776 RID: 30582 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007777 RID: 30583 RVA: 0x002B9189 File Offset: 0x002B7389
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x002B919C File Offset: 0x002B739C
		public override TaskStatus OnUpdate()
		{
			if (((MapSeaCompent)AllMapManage.instance.mapIndex[this.avatar.NowMapIndex]).NodeHasIsLand())
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x0400630E RID: 25358
		private SeaAvatarObjBase avatar;
	}
}
