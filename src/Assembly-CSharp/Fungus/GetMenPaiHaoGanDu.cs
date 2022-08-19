using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F47 RID: 3911
	[CommandInfo("YSNew/Get", "GetMenPaiHaoGanDu", "获取门派好感度存放到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetMenPaiHaoGanDu : Command
	{
		// Token: 0x06006E5D RID: 28253 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E5E RID: 28254 RVA: 0x002A4B64 File Offset: 0x002A2D64
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			Flowchart flowchart = this.GetFlowchart();
			int value = player.MenPaiHaoGanDu.HasField(string.Concat(this.MenPaiID)) ? ((int)player.MenPaiHaoGanDu[string.Concat(this.MenPaiID)].n) : 0;
			flowchart.SetIntegerVariable("TempValue", value);
			this.Continue();
		}

		// Token: 0x06006E5F RID: 28255 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B9A RID: 23450
		[Tooltip("要获取的门派的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MenPaiID;
	}
}
