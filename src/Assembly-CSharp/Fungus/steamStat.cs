using System;
using Steamworks;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA4 RID: 4004
	[CommandInfo("YSTools", "steamStat", "设置steam变量的值", 0)]
	[AddComponentMenu("")]
	public class steamStat : Command
	{
		// Token: 0x06006FB9 RID: 28601 RVA: 0x002A7947 File Offset: 0x002A5B47
		public override void OnEnter()
		{
			SteamChengJiu.ints.SetAchievement(this.StatName);
			SteamUserStats.StoreStats();
			this.Continue();
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006FBB RID: 28603 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C34 RID: 23604
		[Tooltip("steam状态名称")]
		[SerializeField]
		protected string StatName = "";
	}
}
