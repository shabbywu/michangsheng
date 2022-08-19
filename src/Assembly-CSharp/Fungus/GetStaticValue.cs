using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F53 RID: 3923
	[CommandInfo("YSNew/Get", "GetStaticValue", "获取全局变量保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetStaticValue : Command
	{
		// Token: 0x06006E92 RID: 28306 RVA: 0x002A51A4 File Offset: 0x002A33A4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			Flowchart flowchart = this.GetFlowchart();
			switch (this.StaticValueID)
			{
			case 1:
				flowchart.SetIntegerVariable("TempValue", player.HP);
				break;
			case 2:
				flowchart.SetIntegerVariable("TempValue", (int)player.exp);
				break;
			case 3:
				flowchart.SetIntegerVariable("TempValue", (int)player.shouYuan);
				break;
			default:
			{
				int value = GlobalValue.Get(this.StaticValueID, base.GetCommandSourceDesc());
				flowchart.SetIntegerVariable("TempValue", value);
				break;
			}
			}
			this.Continue();
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BB3 RID: 23475
		[Tooltip("全局变量的ID")]
		[SerializeField]
		public int StaticValueID;
	}
}
