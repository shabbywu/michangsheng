using System;
using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011C1 RID: 4545
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class UseSkill : Action
	{
		// Token: 0x060077AD RID: 30637 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x002B9568 File Offset: 0x002B7768
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x002B9598 File Offset: 0x002B7798
		public override TaskStatus OnUpdate()
		{
			SharedInt sharedInt = base.Owner.GetVariable("optimalSkillID") as SharedInt;
			this.selfBehavior.GetVariable("optimalSkillWeight");
			if (sharedInt.Value == 18011 || (sharedInt.Value >= 9001 && sharedInt.Value <= 9999) || (sharedInt.Value >= 13001 && sharedInt.Value <= 13999))
			{
				MessageData data = new MessageData(sharedInt.Value);
				MessageMag.Instance.Send(FightFaBaoShow.NPCUseWeaponMsgKey, data);
			}
			this.avatar.spell.spellSkill(sharedInt.Value, "");
			return 2;
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006328 RID: 25384
		private Avatar avatar;

		// Token: 0x04006329 RID: 25385
		private Behavior selfBehavior;

		// Token: 0x0400632A RID: 25386
		private SharedInt tempWeith;
	}
}
