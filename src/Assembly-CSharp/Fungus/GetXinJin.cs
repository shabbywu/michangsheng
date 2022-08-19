using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F56 RID: 3926
	[CommandInfo("YSNew/Get", "GetXinJin", "获取玩家心境保存到TempValue中", 0)]
	[AddComponentMenu("")]
	public class GetXinJin : Command
	{
		// Token: 0x06006E9D RID: 28317 RVA: 0x002A52D4 File Offset: 0x002A34D4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.GetFlowchart().SetIntegerVariable("TempValue", player.xinjin);
			this.Continue();
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}
	}
}
