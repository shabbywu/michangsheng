using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F48 RID: 3912
	[CommandInfo("YSNew/Get", "GetMoney", "获取金钱保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetMoney : Command
	{
		// Token: 0x06006E62 RID: 28258 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x002A4BCC File Offset: 0x002A2DCC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.GetFlowchart().SetIntegerVariable("TempValue", (int)player.money);
			this.Continue();
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B9B RID: 23451
		[Tooltip("全局变量的ID")]
		[SerializeField]
		protected int StaticValueID;
	}
}
