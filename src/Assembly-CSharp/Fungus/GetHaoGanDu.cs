using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F5 RID: 5109
	[CommandInfo("YSNew/Get", "GetHaoGanDu", "获取好感度保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDu : Command
	{
		// Token: 0x06007C28 RID: 31784 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x002C48E8 File Offset: 0x002C2AE8
		public override void OnEnter()
		{
			int favor = NPCEx.GetFavor(this.AvatarID);
			Flowchart flowchart = this.GetFlowchart();
			this.setHasVariable("TempValue", favor, flowchart);
			this.Continue();
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A60 RID: 27232
		[Tooltip("需要获取好感度的武将ID")]
		[SerializeField]
		protected int AvatarID = 1;
	}
}
