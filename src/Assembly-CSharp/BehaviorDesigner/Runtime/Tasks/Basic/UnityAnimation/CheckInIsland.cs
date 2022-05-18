using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001678 RID: 5752
	[TaskCategory("YSSea")]
	[TaskDescription("检测NPC是否在岛上")]
	public class CheckInIsland : Conditional
	{
		// Token: 0x06008581 RID: 34177 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008582 RID: 34178 RVA: 0x0005CA0E File Offset: 0x0005AC0E
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06008583 RID: 34179 RVA: 0x0005CA21 File Offset: 0x0005AC21
		public override TaskStatus OnUpdate()
		{
			if (((MapSeaCompent)AllMapManage.instance.mapIndex[this.avatar.NowMapIndex]).NodeHasIsLand())
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06008584 RID: 34180 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400723D RID: 29245
		private SeaAvatarObjBase avatar;
	}
}
