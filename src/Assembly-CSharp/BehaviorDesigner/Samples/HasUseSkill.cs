using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x02000FB4 RID: 4020
	[TaskCategory("YS")]
	[TaskDescription("检测是否拥有最优技能")]
	public class HasUseSkill : Conditional
	{
		// Token: 0x06006FF7 RID: 28663 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x002A8B11 File Offset: 0x002A6D11
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x002A8B30 File Offset: 0x002A6D30
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

		// Token: 0x04005C6B RID: 23659
		[Tooltip("当前技能id")]
		public SharedInt currentSkillID;

		// Token: 0x04005C6C RID: 23660
		private Avatar avatar;
	}
}
