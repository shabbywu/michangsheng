using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167B RID: 5755
	[TaskCategory("YSSea")]
	[TaskDescription("检测势力声望")]
	public class CheckShiLiShengWang : Conditional
	{
		// Token: 0x06008590 RID: 34192 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x0005CAB4 File Offset: 0x0005ACB4
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x002D1158 File Offset: 0x002CF358
		public override TaskStatus OnUpdate()
		{
			Avatar player = Tools.instance.getPlayer();
			if ((player.MenPaiHaoGanDu.HasField(string.Concat(this.avatar.MenPai)) ? ((int)player.MenPaiHaoGanDu[string.Concat(this.avatar.MenPai)].n) : 0) >= this.Value.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007242 RID: 29250
		private SeaAvatarObjBase avatar;

		// Token: 0x04007243 RID: 29251
		[Tooltip("声望的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
