using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5D RID: 3933
	[CommandInfo("YSNew/Set", "setMenPai", "设置门派id", 0)]
	[AddComponentMenu("")]
	public class setMenPai : Command
	{
		// Token: 0x06006EB5 RID: 28341 RVA: 0x002A54B8 File Offset: 0x002A36B8
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

		// Token: 0x06006EB6 RID: 28342 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BC1 RID: 23489
		[Tooltip("设置门派的ID")]
		[SerializeField]
		protected int MenPaiID;
	}
}
