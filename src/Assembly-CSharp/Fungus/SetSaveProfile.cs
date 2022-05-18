using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200128E RID: 4750
	[CommandInfo("Variable", "Set Save Profile", "Sets the active profile that the Save Variable and Load Variable commands will use. This is useful to crete multiple player save games. Once set, the profile applies across all Flowcharts and will also persist across scene loads.", 0)]
	[AddComponentMenu("")]
	public class SetSaveProfile : Command
	{
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06007326 RID: 29478 RVA: 0x0004E852 File Offset: 0x0004CA52
		public static string SaveProfile
		{
			get
			{
				return SetSaveProfile.saveProfile;
			}
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x0004E859 File Offset: 0x0004CA59
		public override void OnEnter()
		{
			SetSaveProfile.saveProfile = this.saveProfileName;
			this.Continue();
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x0004E86C File Offset: 0x0004CA6C
		public override string GetSummary()
		{
			return this.saveProfileName;
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006524 RID: 25892
		[Tooltip("Name of save profile to make active.")]
		[SerializeField]
		protected string saveProfileName = "";

		// Token: 0x04006525 RID: 25893
		private static string saveProfile = "";
	}
}
