using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013EC RID: 5100
	[CommandInfo("YSNew/Add", "AddTime", "增加时间", 0)]
	[AddComponentMenu("")]
	public class AddTime : Command
	{
		// Token: 0x06007C06 RID: 31750 RVA: 0x000545A1 File Offset: 0x000527A1
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			NpcJieSuanManager.inst.IsNoJieSuan = this.IsNoJieSuan;
			player.AddTime(this.Day, this.Month, this.Year);
			this.Continue();
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A50 RID: 27216
		[Tooltip("年")]
		[SerializeField]
		public int Year;

		// Token: 0x04006A51 RID: 27217
		[Tooltip("月")]
		[SerializeField]
		public int Month;

		// Token: 0x04006A52 RID: 27218
		[Tooltip("日")]
		[SerializeField]
		public int Day;

		// Token: 0x04006A53 RID: 27219
		public bool IsNoJieSuan;
	}
}
