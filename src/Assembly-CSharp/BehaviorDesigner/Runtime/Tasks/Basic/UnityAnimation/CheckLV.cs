using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001679 RID: 5753
	[TaskCategory("YSSea")]
	[TaskDescription("检测等级")]
	public class CheckLV : Conditional
	{
		// Token: 0x06008586 RID: 34182 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008587 RID: 34183 RVA: 0x0005CA4C File Offset: 0x0005AC4C
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x0005CA5F File Offset: 0x0005AC5F
		public override TaskStatus OnUpdate()
		{
			if ((int)Tools.instance.getPlayer().level >= this.Value.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06008589 RID: 34185 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400723E RID: 29246
		private SeaAvatarObjBase avatar;

		// Token: 0x0400723F RID: 29247
		[Tooltip("等级的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
