using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140C RID: 5132
	[CommandInfo("YSNew/Get", "GetXinJin", "获取玩家心境保存到TempValue中，数字不用填写", 0)]
	[AddComponentMenu("")]
	public class GetXinJin : Command
	{
		// Token: 0x06007C8B RID: 31883 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x002C51D4 File Offset: 0x002C33D4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.GetFlowchart().SetIntegerVariable("TempValue", player.xinjin);
			this.Continue();
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A89 RID: 27273
		[Tooltip("全局变量的ID")]
		[SerializeField]
		protected int StaticValueID;
	}
}
