using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FD RID: 5117
	[CommandInfo("YSNew/Get", "GetMenPaiHaoGanDu", "获取门派好感度存放到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetMenPaiHaoGanDu : Command
	{
		// Token: 0x06007C48 RID: 31816 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x002C4ADC File Offset: 0x002C2CDC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			Flowchart flowchart = this.GetFlowchart();
			int value = player.MenPaiHaoGanDu.HasField(string.Concat(this.MenPaiID)) ? ((int)player.MenPaiHaoGanDu[string.Concat(this.MenPaiID)].n) : 0;
			flowchart.SetIntegerVariable("TempValue", value);
			this.Continue();
		}

		// Token: 0x06007C4A RID: 31818 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A6C RID: 27244
		[Tooltip("要获取的门派的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MenPaiID;
	}
}
