using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E3D RID: 3645
	[CommandInfo("Variable", "Set Save Profile", "Sets the active profile that the Save Variable and Load Variable commands will use. This is useful to crete multiple player save games. Once set, the profile applies across all Flowcharts and will also persist across scene loads.", 0)]
	[AddComponentMenu("")]
	public class SetSaveProfile : Command
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x00286B77 File Offset: 0x00284D77
		public static string SaveProfile
		{
			get
			{
				return SetSaveProfile.saveProfile;
			}
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x00286B7E File Offset: 0x00284D7E
		public override void OnEnter()
		{
			SetSaveProfile.saveProfile = this.saveProfileName;
			this.Continue();
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x00286B91 File Offset: 0x00284D91
		public override string GetSummary()
		{
			return this.saveProfileName;
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040057E0 RID: 22496
		[Tooltip("Name of save profile to make active.")]
		[SerializeField]
		protected string saveProfileName = "";

		// Token: 0x040057E1 RID: 22497
		private static string saveProfile = "";
	}
}
