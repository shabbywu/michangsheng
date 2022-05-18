using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001200 RID: 4608
	[CommandInfo("Scripting", "Debug Log", "Writes a log message to the debug console.", 0)]
	[AddComponentMenu("")]
	public class DebugLog : Command
	{
		// Token: 0x060070C8 RID: 28872 RVA: 0x002A3388 File Offset: 0x002A1588
		public override void OnEnter()
		{
			string text = this.GetFlowchart().SubstituteVariables(this.logMessage.Value);
			switch (this.logType)
			{
			case DebugLogType.Info:
				Debug.Log(text);
				break;
			case DebugLogType.Warning:
				Debug.LogWarning(text);
				break;
			case DebugLogType.Error:
				Debug.LogError(text);
				break;
			}
			this.Continue();
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x0004C948 File Offset: 0x0004AB48
		public override string GetSummary()
		{
			return this.logMessage.GetDescription();
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x0004C955 File Offset: 0x0004AB55
		public override bool HasReference(Variable variable)
		{
			return this.logMessage.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400633D RID: 25405
		[Tooltip("Display type of debug log info")]
		[SerializeField]
		protected DebugLogType logType;

		// Token: 0x0400633E RID: 25406
		[Tooltip("Text to write to the debug log. Supports variable substitution, e.g. {$Myvar}")]
		[SerializeField]
		protected StringDataMulti logMessage;
	}
}
