using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA5 RID: 4005
	[CommandInfo("YSTools", "TryinitFungaus", "初始化Fungaus", 0)]
	[AddComponentMenu("")]
	public class TryinitFungaus : Command, INoCommand
	{
		// Token: 0x06006FBD RID: 28605 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x002A7978 File Offset: 0x002A5B78
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

		// Token: 0x06006FBF RID: 28607 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C35 RID: 23605
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "初始化";
	}
}
