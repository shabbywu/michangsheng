using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F36 RID: 3894
	[CommandInfo("YSNew/Add", "AddTime", "增加时间", 0)]
	[AddComponentMenu("")]
	public class AddTime : Command
	{
		// Token: 0x06006E1B RID: 28187 RVA: 0x002A4553 File Offset: 0x002A2753
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			NpcJieSuanManager.inst.IsNoJieSuan = this.IsNoJieSuan;
			player.AddTime(this.Day, this.Month, this.Year);
			this.Continue();
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B7E RID: 23422
		[Tooltip("年")]
		[SerializeField]
		public int Year;

		// Token: 0x04005B7F RID: 23423
		[Tooltip("月")]
		[SerializeField]
		public int Month;

		// Token: 0x04005B80 RID: 23424
		[Tooltip("日")]
		[SerializeField]
		public int Day;

		// Token: 0x04005B81 RID: 23425
		public bool IsNoJieSuan;
	}
}
