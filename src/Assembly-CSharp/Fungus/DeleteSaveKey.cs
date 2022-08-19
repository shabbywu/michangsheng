using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC7 RID: 3527
	[CommandInfo("Variable", "Delete Save Key", "Deletes a saved value from permanent storage.", 0)]
	[AddComponentMenu("")]
	public class DeleteSaveKey : Command
	{
		// Token: 0x0600644E RID: 25678 RVA: 0x0027E36C File Offset: 0x0027C56C
		public override void OnEnter()
		{
			if (this.key == "")
			{
				this.Continue();
				return;
			}
			Flowchart flowchart = this.GetFlowchart();
			PlayerPrefs.DeleteKey(SetSaveProfile.SaveProfile + "_" + flowchart.SubstituteVariables(this.key));
			this.Continue();
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x0027E3BF File Offset: 0x0027C5BF
		public override string GetSummary()
		{
			if (this.key.Length == 0)
			{
				return "Error: No stored value key selected";
			}
			return this.key;
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005640 RID: 22080
		[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}")]
		[SerializeField]
		protected string key = "";
	}
}
