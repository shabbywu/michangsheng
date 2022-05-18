using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200128A RID: 4746
	[CommandInfo("Narrative", "Set Language", "Set the active language for the scene. A Localization object with a localization file must be present in the scene.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetLanguage : Command
	{
		// Token: 0x06007311 RID: 29457 RVA: 0x002A9D70 File Offset: 0x002A7F70
		public override void OnEnter()
		{
			Localization localization = Object.FindObjectOfType<Localization>();
			if (localization != null)
			{
				localization.SetActiveLanguage(this._languageCode.Value, true);
				SetLanguage.mostRecentLanguage = this._languageCode.Value;
			}
			this.Continue();
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x0004E6E5 File Offset: 0x0004C8E5
		public override string GetSummary()
		{
			return this._languageCode.Value;
		}

		// Token: 0x06007313 RID: 29459 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007314 RID: 29460 RVA: 0x0004E6F2 File Offset: 0x0004C8F2
		public override bool HasReference(Variable variable)
		{
			return this._languageCode.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x0004E710 File Offset: 0x0004C910
		protected virtual void OnEnable()
		{
			if (this.languageCodeOLD != "")
			{
				this._languageCode.Value = this.languageCodeOLD;
				this.languageCodeOLD = "";
			}
		}

		// Token: 0x0400651A RID: 25882
		[Tooltip("Code of the language to set. e.g. ES, DE, JA")]
		[SerializeField]
		protected StringData _languageCode;

		// Token: 0x0400651B RID: 25883
		public static string mostRecentLanguage = "";

		// Token: 0x0400651C RID: 25884
		[HideInInspector]
		[FormerlySerializedAs("languageCode")]
		public string languageCodeOLD = "";
	}
}
