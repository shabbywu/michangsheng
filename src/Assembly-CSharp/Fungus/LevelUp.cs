using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F8B RID: 3979
	[CommandInfo("YSTools", "LevelUp", "直接提升一个等级，并把经验值设为0", 0)]
	[AddComponentMenu("")]
	public class LevelUp : Command
	{
		// Token: 0x06006F5D RID: 28509 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x002A6CB6 File Offset: 0x002A4EB6
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.exp = 0UL;
			player.levelUp();
			this.Continue();
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C08 RID: 23560
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "直接提升一个等级，不用配置什么东西";
	}
}
