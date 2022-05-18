using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001409 RID: 5129
	[CommandInfo("YSNew/Get", "GetStaticValue", "获取全局变量保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetStaticValue : Command
	{
		// Token: 0x06007C7D RID: 31869 RVA: 0x002C5078 File Offset: 0x002C3278
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

		// Token: 0x06007C7E RID: 31870 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A84 RID: 27268
		[Tooltip("全局变量的ID")]
		[SerializeField]
		public int StaticValueID;
	}
}
