using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143F RID: 5183
	[CommandInfo("YSTools", "LevelUp", "直接提升一个等级，并把经验值设为0", 0)]
	[AddComponentMenu("")]
	public class LevelUp : Command
	{
		// Token: 0x06007D47 RID: 32071 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x00054B6A File Offset: 0x00052D6A
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.exp = 0UL;
			player.levelUp();
			this.Continue();
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AD7 RID: 27351
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "直接提升一个等级，不用配置什么东西";
	}
}
