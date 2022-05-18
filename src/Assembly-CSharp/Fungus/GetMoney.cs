using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FE RID: 5118
	[CommandInfo("YSNew/Get", "GetMoney", "获取金钱保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetMoney : Command
	{
		// Token: 0x06007C4D RID: 31821 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x002C4B44 File Offset: 0x002C2D44
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.GetFlowchart().SetIntegerVariable("TempValue", (int)player.money);
			this.Continue();
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A6D RID: 27245
		[Tooltip("全局变量的ID")]
		[SerializeField]
		protected int StaticValueID;
	}
}
