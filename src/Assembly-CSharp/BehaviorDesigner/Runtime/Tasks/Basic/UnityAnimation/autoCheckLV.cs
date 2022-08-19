using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B5 RID: 4533
	[TaskCategory("YSSea")]
	[TaskDescription("自动检测玩家等级是否大于NPC等级")]
	public class autoCheckLV : Conditional
	{
		// Token: 0x06007771 RID: 30577 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x002B9155 File Offset: 0x002B7355
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x002B9168 File Offset: 0x002B7368
		public override TaskStatus OnUpdate()
		{
			if ((int)Tools.instance.getPlayer().level > this.avatar.LV)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x0400630D RID: 25357
		private SeaAvatarObjBase avatar;
	}
}
