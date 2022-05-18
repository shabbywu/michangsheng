using System;
using Steamworks;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001456 RID: 5206
	[CommandInfo("YSTools", "steamStat", "设置steam变量的值", 0)]
	[AddComponentMenu("")]
	public class steamStat : Command
	{
		// Token: 0x06007D99 RID: 32153 RVA: 0x00054F06 File Offset: 0x00053106
		public override void OnEnter()
		{
			SteamChengJiu.ints.SetAchievement(this.StatName);
			SteamUserStats.StoreStats();
			this.Continue();
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006B1E RID: 27422
		[Tooltip("steam状态名称")]
		[SerializeField]
		protected string StatName = "";
	}
}
