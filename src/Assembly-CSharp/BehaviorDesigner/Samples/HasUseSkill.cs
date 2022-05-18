using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x0200146C RID: 5228
	[TaskCategory("YS")]
	[TaskDescription("检测是否拥有最优技能")]
	public class HasUseSkill : Conditional
	{
		// Token: 0x06007DF1 RID: 32241 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x0005526D File Offset: 0x0005346D
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x002C8248 File Offset: 0x002C6448
		public override TaskStatus OnUpdate()
		{
			if ((base.Owner.GetVariable("optimalSkillWeight") as SharedInt).Value == 20)
			{
				int listSum = RoundManager.instance.getListSum(this.avatar.crystal);
				if ((ulong)this.avatar.NowCard < (ulong)((long)listSum))
				{
					return 2;
				}
				return 1;
			}
			else
			{
				if (this.currentSkillID.Value > 0)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x04006B63 RID: 27491
		[Tooltip("当前技能id")]
		public SharedInt currentSkillID;

		// Token: 0x04006B64 RID: 27492
		private Avatar avatar;
	}
}
