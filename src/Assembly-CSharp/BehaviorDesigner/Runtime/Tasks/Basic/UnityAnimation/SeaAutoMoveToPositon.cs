using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001680 RID: 5760
	[TaskCategory("YS")]
	[TaskDescription("无禁之海自动移动到目标点")]
	public class SeaAutoMoveToPositon : Action
	{
		// Token: 0x060085A9 RID: 34217 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x002D11CC File Offset: 0x002CF3CC
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.nowPositon = (base.Owner.GetVariable("nowPositon") as SharedInt);
			this.endPositon = (base.Owner.GetVariable("endPositon") as SharedInt);
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x002D122C File Offset: 0x002CF42C
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

		// Token: 0x060085AC RID: 34220 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400724C RID: 29260
		private Avatar avatar;

		// Token: 0x0400724D RID: 29261
		private SharedInt nowPositon;

		// Token: 0x0400724E RID: 29262
		private SharedInt endPositon;
	}
}
