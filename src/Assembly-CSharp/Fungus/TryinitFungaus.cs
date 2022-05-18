using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001457 RID: 5207
	[CommandInfo("YSTools", "TryinitFungaus", "初始化Fungaus", 0)]
	[AddComponentMenu("")]
	public class TryinitFungaus : Command, INoCommand
	{
		// Token: 0x06007D9D RID: 32157 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x002C6E18 File Offset: 0x002C5018
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			Flowchart flowchart = this.GetFlowchart();
			this.setHasVariable("ShenShi", player.shengShi, flowchart);
			this.setHasVariable("JinJie", (int)player.level, flowchart);
			this.setHasVariable("DunSu", player.dunSu, flowchart);
			this.setHasVariable("ZiZhi", player.ZiZhi, flowchart);
			this.setHasVariable("WuXin", (int)player.wuXin, flowchart);
			this.setHasVariable("ShaQi", (int)player.shaQi, flowchart);
			this.setHasVariable("MenPai", (int)player.menPai, flowchart);
			this.setHasVariable("ChengHao", player.chengHao, flowchart);
			this.setHasVariable("Sex", player.Sex, flowchart);
			this.Continue();
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006B1F RID: 27423
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "初始化";
	}
}
