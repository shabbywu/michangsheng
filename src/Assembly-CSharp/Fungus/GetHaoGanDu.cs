using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F3F RID: 3903
	[CommandInfo("YSNew/Get", "GetHaoGanDu", "获取好感度保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDu : Command
	{
		// Token: 0x06006E3D RID: 28221 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E3E RID: 28222 RVA: 0x002A48D8 File Offset: 0x002A2AD8
		public override void OnEnter()
		{
			int favor = NPCEx.GetFavor(this.AvatarID);
			Flowchart flowchart = this.GetFlowchart();
			this.setHasVariable("TempValue", favor, flowchart);
			this.Continue();
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B8E RID: 23438
		[Tooltip("需要获取好感度的武将ID")]
		[SerializeField]
		protected int AvatarID = 1;
	}
}
