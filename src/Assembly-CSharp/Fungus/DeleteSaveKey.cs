using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001201 RID: 4609
	[CommandInfo("Variable", "Delete Save Key", "Deletes a saved value from permanent storage.", 0)]
	[AddComponentMenu("")]
	public class DeleteSaveKey : Command
	{
		// Token: 0x060070CD RID: 28877 RVA: 0x002A33E4 File Offset: 0x002A15E4
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

		// Token: 0x060070CE RID: 28878 RVA: 0x0004C973 File Offset: 0x0004AB73
		public override string GetSummary()
		{
			if (this.key.Length == 0)
			{
				return "Error: No stored value key selected";
			}
			return this.key;
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0400633F RID: 25407
		[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}")]
		[SerializeField]
		protected string key = "";
	}
}
