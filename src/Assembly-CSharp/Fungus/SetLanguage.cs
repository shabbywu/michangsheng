using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E39 RID: 3641
	[CommandInfo("Narrative", "Set Language", "Set the active language for the scene. A Localization object with a localization file must be present in the scene.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SetLanguage : Command
	{
		// Token: 0x06006683 RID: 26243 RVA: 0x00286944 File Offset: 0x00284B44
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

		// Token: 0x06006684 RID: 26244 RVA: 0x00286988 File Offset: 0x00284B88
		public override string GetSummary()
		{
			return this._languageCode.Value;
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x00286995 File Offset: 0x00284B95
		public override bool HasReference(Variable variable)
		{
			return this._languageCode.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x002869B3 File Offset: 0x00284BB3
		protected virtual void OnEnable()
		{
			if (this.languageCodeOLD != "")
			{
				this._languageCode.Value = this.languageCodeOLD;
				this.languageCodeOLD = "";
			}
		}

		// Token: 0x040057D6 RID: 22486
		[Tooltip("Code of the language to set. e.g. ES, DE, JA")]
		[SerializeField]
		protected StringData _languageCode;

		// Token: 0x040057D7 RID: 22487
		public static string mostRecentLanguage = "";

		// Token: 0x040057D8 RID: 22488
		[HideInInspector]
		[FormerlySerializedAs("languageCode")]
		public string languageCodeOLD = "";
	}
}
