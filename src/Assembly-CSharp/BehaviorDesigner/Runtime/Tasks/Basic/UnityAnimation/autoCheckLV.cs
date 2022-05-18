using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001677 RID: 5751
	[TaskCategory("YSSea")]
	[TaskDescription("自动检测玩家等级是否大于NPC等级")]
	public class autoCheckLV : Conditional
	{
		// Token: 0x0600857C RID: 34172 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x0600857D RID: 34173 RVA: 0x0005C9DA File Offset: 0x0005ABDA
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600857E RID: 34174 RVA: 0x0005C9ED File Offset: 0x0005ABED
		public override TaskStatus OnUpdate()
		{
			if ((int)Tools.instance.getPlayer().level > this.avatar.LV)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600857F RID: 34175 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400723C RID: 29244
		private SeaAvatarObjBase avatar;
	}
}
