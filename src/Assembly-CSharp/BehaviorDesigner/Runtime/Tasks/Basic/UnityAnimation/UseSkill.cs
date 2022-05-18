using System;
using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001683 RID: 5763
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class UseSkill : Action
	{
		// Token: 0x060085B8 RID: 34232 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x0005CB92 File Offset: 0x0005AD92
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x002D13B0 File Offset: 0x002CF5B0
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

		// Token: 0x060085BB RID: 34235 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007257 RID: 29271
		private Avatar avatar;

		// Token: 0x04007258 RID: 29272
		private Behavior selfBehavior;

		// Token: 0x04007259 RID: 29273
		private SharedInt tempWeith;
	}
}
