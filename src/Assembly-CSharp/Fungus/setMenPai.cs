using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001413 RID: 5139
	[CommandInfo("YSNew/Set", "setMenPai", "设置门派id", 0)]
	[AddComponentMenu("")]
	public class setMenPai : Command
	{
		// Token: 0x06007CA5 RID: 31909 RVA: 0x002C5340 File Offset: 0x002C3540
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.joinMenPai(this.MenPaiID);
			player.taskMag.SetChuanWenBlack(5);
			player.taskMag.SetChuanWenBlack(6);
			player.taskMag.SetChuanWenBlack(7);
			player.taskMag.SetChuanWenBlack(29);
			player.taskMag.SetChuanWenBlack(30);
			this.Continue();
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A93 RID: 27283
		[Tooltip("设置门派的ID")]
		[SerializeField]
		protected int MenPaiID;
	}
}
