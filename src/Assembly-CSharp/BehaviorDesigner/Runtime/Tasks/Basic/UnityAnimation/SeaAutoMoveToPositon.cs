using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BE RID: 4542
	[TaskCategory("YS")]
	[TaskDescription("无禁之海自动移动到目标点")]
	public class SeaAutoMoveToPositon : Action
	{
		// Token: 0x0600779E RID: 30622 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x002B9348 File Offset: 0x002B7548
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.nowPositon = (base.Owner.GetVariable("nowPositon") as SharedInt);
			this.endPositon = (base.Owner.GetVariable("endPositon") as SharedInt);
		}

		// Token: 0x060077A0 RID: 30624 RVA: 0x002B93A8 File Offset: 0x002B75A8
		public override TaskStatus OnUpdate()
		{
			int nowMapIndex = this.avatar.NowMapIndex;
			MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[nowMapIndex];
			EndlessSeaMag.Inst.GetRoadXian(nowMapIndex, this.endPositon.Value);
			if (this.nowPositon.Value == this.endPositon.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060077A1 RID: 30625 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x0400631D RID: 25373
		private Avatar avatar;

		// Token: 0x0400631E RID: 25374
		private SharedInt nowPositon;

		// Token: 0x0400631F RID: 25375
		private SharedInt endPositon;
	}
}
