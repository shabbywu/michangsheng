using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B9 RID: 4537
	[TaskCategory("YSSea")]
	[TaskDescription("检测势力声望")]
	public class CheckShiLiShengWang : Conditional
	{
		// Token: 0x06007785 RID: 30597 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007786 RID: 30598 RVA: 0x002B922F File Offset: 0x002B742F
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007787 RID: 30599 RVA: 0x002B9244 File Offset: 0x002B7444
		public override TaskStatus OnUpdate()
		{
			Avatar player = Tools.instance.getPlayer();
			if ((player.MenPaiHaoGanDu.HasField(string.Concat(this.avatar.MenPai)) ? ((int)player.MenPaiHaoGanDu[string.Concat(this.avatar.MenPai)].n) : 0) >= this.Value.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06007788 RID: 30600 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006313 RID: 25363
		private SeaAvatarObjBase avatar;

		// Token: 0x04006314 RID: 25364
		[Tooltip("声望的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
